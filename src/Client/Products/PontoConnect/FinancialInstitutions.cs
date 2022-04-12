using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Models;
using Ibanity.Apis.Client.Products.PontoConnect.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <summary>
    /// This is an object representing a financial institution, providing its basic details - ID and name. Only the financial institutions corresponding to authorized accounts will be available on the API.
    /// </summary>
    public class FinancialInstitutions : ResourceClient<FinancialInstitution, object, object, object>, IFinancialInstitutions
    {
        private const string EntityName = "financial-institutions";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public FinancialInstitutions(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        public Task<PaginatedCollection<FinancialInstitution>> ListForOrganization(Token token, IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                filters,
                pageSize,
                cancellationToken);

        /// <inheritdoc />
        public Task<PaginatedCollection<FinancialInstitution>> ListForOrganization(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                continuationToken ?? throw new ArgumentNullException(nameof(continuationToken)),
                cancellationToken);

        /// <inheritdoc />
        public Task<PaginatedCollection<FinancialInstitution>> List(ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalList(
                null,
                continuationToken ?? throw new ArgumentNullException(nameof(continuationToken)),
                cancellationToken);

        /// <inheritdoc />
        public Task<PaginatedCollection<FinancialInstitution>> List(IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken) =>
            InternalList(null, filters, pageSize, cancellationToken);

        /// <inheritdoc />
        public Task<FinancialInstitution> GetForOrganization(Token token, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, id, cancellationToken);

        /// <inheritdoc />
        public Task<FinancialInstitution> Get(Guid id, CancellationToken? cancellationToken) =>
            GetForOrganization(null, id, cancellationToken);
    }

    /// <summary>
    /// This is an object representing a financial institution, providing its basic details - ID and name. Only the financial institutions corresponding to authorized accounts will be available on the API.
    /// </summary>
    public interface IFinancialInstitutions
    {
        /// <summary>
        /// List Organization Financial Institutions
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="filters">Attributes to be filtered from the results</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a list of financial institution resources</returns>
        Task<PaginatedCollection<FinancialInstitution>> ListForOrganization(Token token, IEnumerable<Filter> filters = null, int? pageSize = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Organization Financial Institutions
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="continuationToken">Token referencing the page to request</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a list of financial institution resources</returns>
        Task<PaginatedCollection<FinancialInstitution>> ListForOrganization(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="id">Financial institution ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a financial institution resource</returns>
        Task<FinancialInstitution> GetForOrganization(Token token, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="filters">Attributes to be filtered from the results</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a list of financial institution resources</returns>
        Task<PaginatedCollection<FinancialInstitution>> List(IEnumerable<Filter> filters = null, int? pageSize = null, CancellationToken? cancellationToken = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="continuationToken">Token referencing the page to request</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a list of financial institution resources</returns>
        Task<PaginatedCollection<FinancialInstitution>> List(ContinuationToken continuationToken, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Financial Institution
        /// </summary>
        /// <param name="id">Financial institution ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a financial institution resource</returns>
        Task<FinancialInstitution> Get(Guid id, CancellationToken? cancellationToken = null);
    }
}
