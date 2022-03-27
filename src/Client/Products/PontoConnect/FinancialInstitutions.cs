using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Models;
using Ibanity.Apis.Client.Products.PontoConnect.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class FinancialInstitutions : IFinancialInstitutions
    {
        private const string EntityName = "financial-institutions";

        private readonly IApiClient _apiClient;
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly string _urlPrefix;

        public FinancialInstitutions(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix)
        {
            if (string.IsNullOrEmpty(urlPrefix))
                throw new ArgumentException($"'{nameof(urlPrefix)}' cannot be null or empty.", nameof(urlPrefix));

            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _accessTokenProvider = accessTokenProvider ?? throw new ArgumentNullException(nameof(accessTokenProvider));
            _urlPrefix = urlPrefix;
        }

        public async Task<PaginatedCollection<FinancialInstitution>> ListForOrganization(Token token, IEnumerable<Filter> filters, ContinuationToken continuationToken, CancellationToken? cancellationToken)
        {
            var queryParameters = string.Join("&", (filters ?? Enumerable.Empty<Filter>()).Select(f => f.ToString()));

            if (!string.IsNullOrWhiteSpace(queryParameters))
                queryParameters = "?" + queryParameters; // we need a proper builder here

            var page = await _apiClient.Get<JsonApi.Collection<FinancialInstitution>>(
                continuationToken == null
                    ? $"{_urlPrefix}/{EntityName}{queryParameters}"
                    : continuationToken.NextUrl,
                await GetAccessToken(token),
                cancellationToken ?? CancellationToken.None);

            var result = new PaginatedCollection<FinancialInstitution>(page.Data.Select(Map));

            result.ContinuationToken = page.Links.Next == null
                ? null
                : new ContinuationToken(page.Links.Next);

            return result;
        }

        public async Task<FinancialInstitution> GetForOrganization(Token token, Guid id, CancellationToken? cancellationToken) =>
            Map((await _apiClient.Get<JsonApi.Resource<FinancialInstitution>>(
                $"{_urlPrefix}/{EntityName}/{id}",
                await GetAccessToken(token),
                cancellationToken ?? CancellationToken.None)).Data);

        public Task<PaginatedCollection<FinancialInstitution>> List(IEnumerable<Filter> filters, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            ListForOrganization(null, filters, continuationToken, cancellationToken);

        public Task<FinancialInstitution> Get(Guid id, CancellationToken? cancellationToken) =>
            GetForOrganization(null, id, cancellationToken);

        private T Map<T>(Data<T> data) where T : Identified<Guid>
        {
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            var result = data.Attributes;
            result.Id = Guid.Parse(data.Id);
            return result;
        }

        private async Task<string> GetAccessToken(Token token) => token == null
            ? null
            : (await _accessTokenProvider.RefreshToken(token)).AccessToken;
    }

    public interface IFinancialInstitutions
    {
        Task<PaginatedCollection<FinancialInstitution>> ListForOrganization(Token token, IEnumerable<Filter> filters = null, ContinuationToken continuationToken = null, CancellationToken? cancellationToken = null);
        Task<FinancialInstitution> GetForOrganization(Token token, Guid id, CancellationToken? cancellationToken = null);
        Task<PaginatedCollection<FinancialInstitution>> List(IEnumerable<Filter> filters = null, ContinuationToken continuationToken = null, CancellationToken? cancellationToken = null);
        Task<FinancialInstitution> Get(Guid id, CancellationToken? cancellationToken = null);
    }
}
