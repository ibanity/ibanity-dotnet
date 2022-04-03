using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Models;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class FinancialInstitutions : ResourceClient<FinancialInstitution, object, object>, IFinancialInstitutions
    {
        private const string EntityName = "financial-institutions";

        public FinancialInstitutions(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        public Task<PaginatedCollection<FinancialInstitution>> ListForOrganization(Token token, IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                filters,
                pageSize,
                cancellationToken);

        public Task<PaginatedCollection<FinancialInstitution>> ListForOrganization(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                continuationToken ?? throw new ArgumentNullException(nameof(continuationToken)),
                cancellationToken);

        public Task<PaginatedCollection<FinancialInstitution>> List(ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalList(
                null,
                continuationToken ?? throw new ArgumentNullException(nameof(continuationToken)),
                cancellationToken);

        public Task<PaginatedCollection<FinancialInstitution>> List(IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken) =>
            InternalList(null, filters, pageSize, cancellationToken);

        public Task<FinancialInstitution> GetForOrganization(Token token, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, id, cancellationToken);

        public Task<FinancialInstitution> Get(Guid id, CancellationToken? cancellationToken) =>
            GetForOrganization(null, id, cancellationToken);
    }

    public interface IFinancialInstitutions
    {
        Task<PaginatedCollection<FinancialInstitution>> ListForOrganization(Token token, IEnumerable<Filter> filters = null, int? pageSize = null, CancellationToken? cancellationToken = null);
        Task<PaginatedCollection<FinancialInstitution>> ListForOrganization(Token token, ContinuationToken continuationToken = null, CancellationToken? cancellationToken = null);
        Task<FinancialInstitution> GetForOrganization(Token token, Guid id, CancellationToken? cancellationToken = null);
        Task<PaginatedCollection<FinancialInstitution>> List(IEnumerable<Filter> filters = null, int? pageSize = null, CancellationToken? cancellationToken = null);
        Task<PaginatedCollection<FinancialInstitution>> List(ContinuationToken continuationToken, CancellationToken? cancellationToken = null);
        Task<FinancialInstitution> Get(Guid id, CancellationToken? cancellationToken = null);
    }
}
