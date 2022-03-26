using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
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

        public async Task<PaginatedCollection<FinancialInstitution>> List(ContinuationToken continuationToken, CancellationToken? cancellationToken)
        {
            var page = await _apiClient.Get<JsonApi.Collection<FinancialInstitution>>(
                continuationToken == null
                    ? $"{_urlPrefix}/{EntityName}"
                    : continuationToken.NextUrl,
                cancellationToken ?? CancellationToken.None);

            var result = new PaginatedCollection<FinancialInstitution>(page.Data.Select(Map));

            result.ContinuationToken = page.Links.Next == null
                ? null
                : new ContinuationToken(page.Links.Next);

            return result;
        }

        private T Map<T>(Data<T> data) where T : Identified<Guid>
        {
            var result = data.Attributes;
            result.Id = Guid.Parse(data.Id);
            return result;
        }
    }

    public interface IFinancialInstitutions
    {
        Task<PaginatedCollection<FinancialInstitution>> List(ContinuationToken continuationToken = null, CancellationToken? cancellationToken = null);
    }
}
