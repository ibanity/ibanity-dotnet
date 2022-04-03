using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public abstract class BaseResourceClient<TAttributes, TMeta, TRelationships, TLinks> where TAttributes : IIdentified<Guid>
    {
        protected readonly IApiClient _apiClient;
        private readonly IAccessTokenProvider _accessTokenProvider;
        protected readonly string _urlPrefix;

        public BaseResourceClient(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix)
        {
            if (string.IsNullOrWhiteSpace(urlPrefix))
                throw new ArgumentException($"'{nameof(urlPrefix)}' cannot be null or whitespace.", nameof(urlPrefix));

            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _accessTokenProvider = accessTokenProvider ?? throw new ArgumentNullException(nameof(accessTokenProvider));
            _urlPrefix = urlPrefix;
        }

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

        protected Task<PaginatedCollection<TAttributes>> InternalList(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                (continuationToken ?? throw new ArgumentNullException(nameof(continuationToken))).NextUrl,
                cancellationToken);

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

        protected async Task<TAttributes> InternalGet(Token token, string path, Guid id, CancellationToken? cancellationToken) =>
            Map((await _apiClient.Get<JsonApi.Resource<TAttributes, TMeta, TRelationships, TLinks>>(
                $"{path}/{id}",
                await GetAccessToken(token),
                cancellationToken ?? CancellationToken.None)).Data);

        protected async Task<TAttributes> InternalCreate<T>(Token token, string path, T payload, CancellationToken? cancellationToken) =>
            Map((await _apiClient.Post<T, JsonApi.Resource<TAttributes, TMeta, TRelationships, TLinks>>(
                $"{path}",
                await GetAccessToken(token),
                payload,
                cancellationToken ?? CancellationToken.None)).Data);

        protected virtual TAttributes Map(JsonApi.Data<TAttributes, TMeta, TRelationships, TLinks> data)
        {
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            var result = data.Attributes;
            result.Id = Guid.Parse(data.Id);
            return result;
        }

        protected async Task<string> GetAccessToken(Token token) => token == null
            ? null
            : (await _accessTokenProvider.RefreshToken(token)).AccessToken;
    }

    public abstract class ResourceClient<TAttributes, TMeta, TRelationships, TLinks> :
        BaseResourceClient<TAttributes, TMeta, TRelationships, TLinks> where TAttributes : IIdentified<Guid>
    {
        private readonly string _entityName;

        public ResourceClient(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix, string entityName) :
            base(apiClient, accessTokenProvider, urlPrefix)
        {
            if (string.IsNullOrWhiteSpace(entityName))
                throw new ArgumentException($"'{nameof(entityName)}' cannot be null or whitespace.", nameof(entityName));

            _entityName = entityName;
        }

        protected Task<PaginatedCollection<TAttributes>> InternalList(Token token, IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken) =>
            InternalList(token, $"{_urlPrefix}/{_entityName}", filters, pageSize, cancellationToken);

        protected Task<TAttributes> InternalGet(Token token, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, $"{_urlPrefix}/{_entityName}", id, cancellationToken);

        protected async Task InternalDelete(Token token, Guid id, CancellationToken? cancellationToken) =>
            await _apiClient.Delete(
                $"{_urlPrefix}/{_entityName}/{id}",
                await GetAccessToken(token),
                cancellationToken ?? CancellationToken.None);
    }

    public abstract class ResourceWithParentClient<TAttributes, TMeta, TRelationships, TLinks> :
        BaseResourceClient<TAttributes, TMeta, TRelationships, TLinks> where TAttributes : IIdentified<Guid>
    {
        private readonly string _parentEntityName;
        private readonly string _entityName;

        public ResourceWithParentClient(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix, string parentEntityName, string entityName) :
            base(apiClient, accessTokenProvider, urlPrefix)
        {
            if (string.IsNullOrWhiteSpace(parentEntityName))
                throw new ArgumentException($"'{nameof(parentEntityName)}' cannot be null or whitespace.", nameof(parentEntityName));

            if (string.IsNullOrWhiteSpace(entityName))
                throw new ArgumentException($"'{nameof(entityName)}' cannot be null or whitespace.", nameof(entityName));

            _parentEntityName = parentEntityName;
            _entityName = entityName;
        }

        protected Task<PaginatedCollection<TAttributes>> InternalList(Token token, Guid parentId, IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken) =>
            InternalList(token, $"{_urlPrefix}/{_parentEntityName}/{parentId}/{_entityName}", filters, pageSize, cancellationToken);

        protected Task<TAttributes> InternalGet(Token token, Guid parentId, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, $"{_urlPrefix}/{_parentEntityName}/{parentId}/{_entityName}", id, cancellationToken);

        protected Task<TAttributes> InternalCreate<T>(Token token, Guid parentId, T payload, CancellationToken? cancellationToken) =>
            InternalCreate(token, $"{_urlPrefix}/{_parentEntityName}/{parentId}/{_entityName}", payload, cancellationToken);
    }
}