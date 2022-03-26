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
        private readonly ITokenProvider _tokenService;
        private readonly string _urlPrefix;

        public FinancialInstitutions(IApiClient apiClient, ITokenProvider tokenService, string urlPrefix)
        {
            _apiClient = apiClient;
            _tokenService = tokenService;
            _urlPrefix = urlPrefix;
        }

        public async Task<PaginatedCollection<FinancialInstitution>> List(IEnumerable<Filter> filters, ContinuationToken continuationToken, CancellationToken? cancellationToken)
        {
            var queryParameters = string.Join("&", (filters ?? Enumerable.Empty<Filter>()).Select(f => f.ToString()));

            if (!string.IsNullOrWhiteSpace(queryParameters))
                queryParameters = "?" + queryParameters; // we need a proper builder here

            var page = await _apiClient.Get<JsonApi.Collection<FinancialInstitution>>(
                continuationToken == null
                    ? $"{_urlPrefix}/{EntityName}{queryParameters}"
                    : continuationToken.NextUrl,
                cancellationToken ?? CancellationToken.None);

            var result = new PaginatedCollection<FinancialInstitution>(page.Data.Select(Map));

            result.ContinuationToken = page.Links.Next == null
                ? null
                : new ContinuationToken(page.Links.Next);

            return result;
        }

        public async Task<FinancialInstitution> Get(Guid id, CancellationToken? cancellationToken) =>
            Map((await _apiClient.Get<JsonApi.Resource<FinancialInstitution>>(
                $"{_urlPrefix}/{EntityName}/{id}",
                cancellationToken ?? CancellationToken.None)).Data);

        private T Map<T>(Data<T> data) where T : Identified<Guid>
        {
            var result = data.Attributes;
            result.Id = Guid.Parse(data.Id);
            return result;
        }
    }

    public interface IFinancialInstitutions
    {
        Task<PaginatedCollection<FinancialInstitution>> List(IEnumerable<Filter> filters = null, ContinuationToken continuationToken = null, CancellationToken? cancellationToken = null);
        Task<FinancialInstitution> Get(Guid id, CancellationToken? cancellationToken = null);
    }
}
