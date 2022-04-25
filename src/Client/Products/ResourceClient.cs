using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products
{
    /// <summary>
    /// Base class to manage an API resource.
    /// </summary>
    /// <typeparam name="TAttributes">Resource attribute type</typeparam>
    /// <typeparam name="TMeta">Resource meta type</typeparam>
    /// <typeparam name="TRelationships">Resource relationships type</typeparam>
    /// <typeparam name="TLinks">Resource links type</typeparam>
    public abstract class BaseResourceClient<TAttributes, TMeta, TRelationships, TLinks> where TAttributes : IIdentified<Guid>
    {
        private readonly IApiClient _apiClient;
        private readonly IAccessTokenProvider _accessTokenProvider;

        /// <summary>
        /// Beginning of URIs, composed by Ibanity API endpoint, followed by product name.
        /// </summary>
        protected string UrlPrefix { get; }

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public BaseResourceClient(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix)
        {
            if (string.IsNullOrWhiteSpace(urlPrefix))
                throw new ArgumentException($"'{nameof(urlPrefix)}' cannot be null or whitespace.", nameof(urlPrefix));

            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _accessTokenProvider = accessTokenProvider ?? throw new ArgumentNullException(nameof(accessTokenProvider));
            UrlPrefix = urlPrefix;
        }

        /// <summary>
        /// Get all resources.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="path">Resource collection path</param>
        /// <param name="filters">Attributes to be filtered from the results</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>First page of items</returns>
        protected Task<PaginatedCollection<TAttributes>> InternalList(Token token, string path, IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken)
        {
            var parameters = (filters ?? Enumerable.Empty<Filter>()).Select(f => f.ToString()).ToList();

            if (pageSize.HasValue)
                parameters.Add($"page[limit]={pageSize.Value}");

            // we need a proper builder here
            var queryParameters = parameters.Any()
                ? "?" + string.Join("&", parameters)
                : string.Empty;

            return InternalList(
                token,
                $"{path}{queryParameters}",
                cancellationToken);
        }

        /// <summary>
        /// Get all resources.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="continuationToken">Token referencing the page to request</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Requested page of items</returns>
        protected Task<PaginatedCollection<TAttributes>> InternalList(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                (continuationToken ?? throw new ArgumentNullException(nameof(continuationToken))).NextUrl,
                cancellationToken);

        /// <summary>
        /// Get all resources.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="path">Resource collection path</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>First page of items</returns>
        protected async Task<PaginatedCollection<TAttributes>> InternalList(Token token, string path, CancellationToken? cancellationToken)
        {
            var page = await _apiClient.Get<JsonApi.Collection<TAttributes, TMeta, TRelationships, TLinks>>(
                path,
                await GetAccessToken(token),
                cancellationToken ?? CancellationToken.None);

            var result = new PaginatedCollection<TAttributes>(page.Data.Select(Map));

            result.ContinuationToken = page.Links.Next == null
                ? null
                : new ContinuationToken(page.Links.Next);

            return result;
        }

        /// <summary>
        /// Get a single resource.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="path">Path part preceding the ID</param>
        /// <param name="id">Unique identifier of the resource</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Requested resource</returns>
        protected async Task<TAttributes> InternalGet(Token token, string path, Guid id, CancellationToken? cancellationToken) =>
            Map((await _apiClient.Get<JsonApi.Resource<TAttributes, TMeta, TRelationships, TLinks>>(
                $"{path}/{id}",
                await GetAccessToken(token),
                cancellationToken ?? CancellationToken.None)).Data);

        /// <summary>
        /// Delete a single resource.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="path">Path part preceding the ID</param>
        /// <param name="id">Unique identifier of the resource</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        protected async Task InternalDelete(Token token, string path, Guid id, CancellationToken? cancellationToken) =>
            await _apiClient.Delete(
                $"{path}/{id}",
                await GetAccessToken(token),
                cancellationToken ?? CancellationToken.None);

        /// <summary>
        /// Create a new resource.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="path">Resource collection path</param>
        /// <param name="payload">Resource data</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created resource</returns>
        protected async Task<TAttributes> InternalCreate<T>(Token token, string path, JsonApi.Data<T, object, object, object> payload, Guid? idempotencyKey, CancellationToken? cancellationToken) =>
            Map((await _apiClient.Post<JsonApi.Resource<T, object, object, object>, JsonApi.Resource<TAttributes, TMeta, TRelationships, TLinks>>(
                $"{path}",
                await GetAccessToken(token),
                new JsonApi.Resource<T, object, object, object> { Data = payload },
                idempotencyKey ?? Guid.NewGuid(),
                cancellationToken ?? CancellationToken.None)).Data);

        /// <summary>
        /// Create a new resource.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="path">Resource collection path</param>
        /// <param name="id">Unique identifier of the resource</param>
        /// <param name="payload">Resource data</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The updated resource</returns>
        protected async Task<TAttributes> InternalUpdate<T>(Token token, string path, Guid id, JsonApi.Data<T, object, object, object> payload, Guid? idempotencyKey, CancellationToken? cancellationToken) =>
            Map((await _apiClient.Patch<JsonApi.Resource<T, object, object, object>, JsonApi.Resource<TAttributes, TMeta, TRelationships, TLinks>>(
                $"{path}/{id}",
                await GetAccessToken(token),
                new JsonApi.Resource<T, object, object, object> { Data = payload },
                idempotencyKey ?? Guid.NewGuid(),
                cancellationToken ?? CancellationToken.None)).Data);

        /// <summary>
        /// Map received JSON:API data to a single-level object.
        /// </summary>
        /// <param name="data">Data received from the server</param>
        /// <returns>A single-level object ready to be used by the client application</returns>
        protected virtual TAttributes Map(JsonApi.Data<TAttributes, TMeta, TRelationships, TLinks> data)
        {
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            var result = data.Attributes;
            result.Id = Guid.Parse(data.Id);
            return result;
        }

        /// <summary>
        /// Refresh the token if necessary and returns the bearer token.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <returns>Bearer token</returns>
        protected async Task<string> GetAccessToken(Token token) => token == null
            ? null
            : (await _accessTokenProvider.RefreshToken(token)).AccessToken;
    }

    /// <summary>
    /// Base class to manage an API resource with a single ID.
    /// </summary>
    /// <typeparam name="TAttributes">Resource attribute type</typeparam>
    /// <typeparam name="TMeta">Resource meta type</typeparam>
    /// <typeparam name="TRelationships">Resource relationships type</typeparam>
    /// <typeparam name="TLinks">Resource links type</typeparam>
    public abstract class ResourceClient<TAttributes, TMeta, TRelationships, TLinks> :
        BaseResourceClient<TAttributes, TMeta, TRelationships, TLinks> where TAttributes : IIdentified<Guid>
    {
        private readonly string _entityName;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        /// <param name="entityName">Name of the resource</param>
        public ResourceClient(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix, string entityName) :
            base(apiClient, accessTokenProvider, urlPrefix)
        {
            if (string.IsNullOrWhiteSpace(entityName))
                throw new ArgumentException($"'{nameof(entityName)}' cannot be null or whitespace.", nameof(entityName));

            _entityName = entityName;
        }

        /// <summary>
        /// Get all resources.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="filters">Attributes to be filtered from the results</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>First page of items</returns>
        protected Task<PaginatedCollection<TAttributes>> InternalList(Token token, IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken) =>
            InternalList(token, GetPath(), filters, pageSize, cancellationToken);

        /// <summary>
        /// Get a single resource.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="id">Unique identifier of the resource</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Requested resource</returns>
        protected Task<TAttributes> InternalGet(Token token, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, GetPath(), id, cancellationToken);

        /// <summary>
        /// Delete a single resource.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="id">Unique identifier of the resource</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        protected Task InternalDelete(Token token, Guid id, CancellationToken? cancellationToken) =>
            InternalDelete(token, GetPath(), id, cancellationToken);

        /// <summary>
        /// Create a new resource.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="payload">Resource data</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created resource</returns>
        protected Task<TAttributes> InternalCreate<T>(Token token, JsonApi.Data<T, object, object, object> payload, Guid? idempotencyKey, CancellationToken? cancellationToken) =>
            InternalCreate(token, GetPath(), payload, idempotencyKey, cancellationToken);

        private string GetPath() =>
            $"{UrlPrefix}/{_entityName}";
    }

    /// <summary>
    /// Base class to manage an API resource with a multiple IDs.
    /// </summary>
    /// <typeparam name="TAttributes">Resource attribute type</typeparam>
    /// <typeparam name="TMeta">Resource meta type</typeparam>
    /// <typeparam name="TRelationships">Resource relationships type</typeparam>
    /// <typeparam name="TLinks">Resource links type</typeparam>
    public abstract class ResourceWithParentClient<TAttributes, TMeta, TRelationships, TLinks> :
        BaseResourceClient<TAttributes, TMeta, TRelationships, TLinks> where TAttributes : IIdentified<Guid>
    {
        private readonly string[] _entityNames;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        /// <param name="entityNames">Names of the resources hierarchy</param>
        public ResourceWithParentClient(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix, string[] entityNames) :
            base(apiClient, accessTokenProvider, urlPrefix)
        {
            _entityNames = entityNames ?? throw new ArgumentNullException(nameof(entityNames));

            if (entityNames.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("Empty entity name", nameof(entityNames));

            if (entityNames.Length < 2)
                throw new ArgumentException($"Too few entity names (expected at least 2 but got {entityNames.Length})", nameof(entityNames));
        }

        /// <summary>
        /// Get all resources.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="parentIds">IDs of parent resources</param>
        /// <param name="filters">Attributes to be filtered from the results</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>First page of items</returns>
        protected Task<PaginatedCollection<TAttributes>> InternalList(Token token, Guid[] parentIds, IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken) =>
            InternalList(token, GetPath(parentIds), filters, pageSize, cancellationToken);

        /// <summary>
        /// Get a single resource.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="parentIds">IDs of parent resources</param>
        /// <param name="id">Unique identifier of the resource</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Requested resource</returns>
        protected Task<TAttributes> InternalGet(Token token, Guid[] parentIds, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, GetPath(parentIds), id, cancellationToken);

        /// <summary>
        /// Delete a single resource.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="parentIds">IDs of parent resources</param>
        /// <param name="id">Unique identifier of the resource</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        protected Task InternalDelete(Token token, Guid[] parentIds, Guid id, CancellationToken? cancellationToken) =>
            InternalDelete(token, GetPath(parentIds), id, cancellationToken);

        /// <summary>
        /// Create a new resource.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="parentIds">IDs of parent resources</param>
        /// <param name="payload">Resource data</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created resource</returns>
        protected Task<TAttributes> InternalCreate<T>(Token token, Guid[] parentIds, JsonApi.Data<T, object, object, object> payload, Guid? idempotencyKey, CancellationToken? cancellationToken) =>
            InternalCreate(token, GetPath(parentIds), payload, idempotencyKey, cancellationToken);

        /// <summary>
        /// Create a new resource.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="parentIds">IDs of parent resources</param>
        /// <param name="id">Unique identifier of the resource</param>
        /// <param name="payload">Resource data</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The updated resource</returns>
        protected Task<TAttributes> InternalUpdate<T>(Token token, Guid[] parentIds, Guid id, JsonApi.Data<T, object, object, object> payload, Guid? idempotencyKey, CancellationToken? cancellationToken) =>
            InternalUpdate(token, GetPath(parentIds), id, payload, idempotencyKey, cancellationToken);

        private string GetPath(Guid[] ids)
        {
            if (ids is null)
                throw new ArgumentNullException(nameof(ids));

            if (ids.Length != _entityNames.Length - 1)
                throw new ArgumentException($"Expected {_entityNames.Length - 1} IDs but got {ids.Length}", nameof(ids));

            return
                UrlPrefix + "/" +
                string.Join(string.Empty, _entityNames.Take(_entityNames.Length - 1).Zip(ids, (e, i) => $"{e}/{i}/")) +
                _entityNames.Last();
        }
    }
}
