using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public abstract class ResourceClient<T> where T : Identified<Guid>
    {
        private readonly IApiClient _apiClient;
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly string _urlPrefix;
        private readonly string _entityName;

        public ResourceClient(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix, string entityName)
        {
            if (string.IsNullOrWhiteSpace(urlPrefix))
                throw new ArgumentException($"'{nameof(urlPrefix)}' cannot be null or empty.", nameof(urlPrefix));

            if (string.IsNullOrWhiteSpace(entityName))
                throw new ArgumentException($"'{nameof(entityName)}' cannot be null or whitespace.", nameof(entityName));

            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _accessTokenProvider = accessTokenProvider ?? throw new ArgumentNullException(nameof(accessTokenProvider));
            _urlPrefix = urlPrefix;
            _entityName = entityName;
        }

        protected Task<PaginatedCollection<T>> InternalList(Token token, IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken)
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
                $"{_urlPrefix}/{_entityName}{queryParameters}",
                cancellationToken);
        }

        protected Task<PaginatedCollection<T>> InternalList(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                (continuationToken ?? throw new ArgumentNullException(nameof(continuationToken))).NextUrl,
                cancellationToken);

        protected async Task<PaginatedCollection<T>> InternalList(Token token, string path, CancellationToken? cancellationToken)
        {
            var page = await _apiClient.Get<JsonApi.Collection<T>>(
                path,
                await GetAccessToken(token),
                cancellationToken ?? CancellationToken.None);

            var result = new PaginatedCollection<T>(page.Data.Select(Map));

            result.ContinuationToken = page.Links.Next == null
                ? null
                : new ContinuationToken(page.Links.Next);

            return result;
        }

        protected async Task<T> InternalGet(Token token, Guid id, CancellationToken? cancellationToken) =>
            Map((await _apiClient.Get<JsonApi.Resource<T>>(
                $"{_urlPrefix}/{_entityName}/{id}",
                await GetAccessToken(token),
                cancellationToken ?? CancellationToken.None)).Data);

        protected T Map(Data<T> data)
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
}
