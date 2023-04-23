using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="IPendingTransactions" />
    public class PendingTransactions : ResourceWithParentClient<PendingTransaction, object, PendingTransactionRelationships, object, CustomerAccessToken>, IPendingTransactions
    {
        private const string GrandParentEntityName = "customer/financial-institutions";
        private const string ParentEntityName = "accounts";
        private const string EntityName = "pending-transactions";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PendingTransactions(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { GrandParentEntityName, ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<IbanityCollection<PendingTransaction>> List(CustomerAccessToken token, Guid financialInstitutionId, Guid accountId, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null) =>
            InternalCursorBasedList(token, new[] { financialInstitutionId, accountId }, null, pageLimit, pageBefore, pageAfter, cancellationToken);

        /// <inheritdoc />
        public Task<IbanityCollection<PendingTransaction>> ListUpdatedForSynchronization(CustomerAccessToken token, Guid synchronizationId, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null) =>
            InternalCursorBasedList(
                token ?? throw new ArgumentNullException(nameof(token)),
                $"{UrlPrefix}/customer/synchronizations/{synchronizationId}/updated-pending-transactions",
                null,
                pageLimit,
                pageBefore,
                pageAfter,
                cancellationToken);

        /// <inheritdoc />
        public Task<PendingTransaction> Get(CustomerAccessToken token, Guid financialInstitutionId, Guid accountId, Guid id, CancellationToken? cancellationToken = null) =>
            InternalGet(token, new[] { financialInstitutionId, accountId }, id, cancellationToken);

        /// <inheritdoc />
        protected override PendingTransaction Map(JsonApi.Data<PendingTransaction, object, PendingTransactionRelationships, object> data)
        {
            var result = base.Map(data);

            result.AccountId = Guid.Parse(data.Relationships.Account.Data.Id);

            return result;
        }
    }

    /// <summary>
    /// <p>This is an object representing an account pending transaction. This object will give you the details of the financial transaction, including its amount and description.</p>
    /// <p>From this object, you can link back to its account.</p>
    /// <p>The Pending transaction API endpoints are customer specific and therefore can only be accessed by providing the corresponding customer access token.</p>
    /// </summary>
    public interface IPendingTransactions
    {
        /// <summary>
        /// List Pending Transactions
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="accountId">Bank account ID</param>
        /// <param name="pageLimit">Number of items by page</param>
        /// <param name="pageBefore">Cursor that specifies the first resource of the next page</param>
        /// <param name="pageAfter">Cursor that specifies the last resource of the previous page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of pending transaction resources</returns>
        Task<IbanityCollection<PendingTransaction>> List(CustomerAccessToken token, Guid financialInstitutionId, Guid accountId, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Pending Transactions
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="synchronizationId">Synchronization ID</param>
        /// <param name="pageLimit">Number of items by page</param>
        /// <param name="pageBefore">Cursor that specifies the first resource of the next page</param>
        /// <param name="pageAfter">Cursor that specifies the last resource of the previous page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of pending transaction resources</returns>
        Task<IbanityCollection<PendingTransaction>> ListUpdatedForSynchronization(CustomerAccessToken token, Guid synchronizationId, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Pending Transaction
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="accountId">Bank account ID</param>
        /// <param name="id">Pending Transaction ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The specified pending transaction resource or 404 if not found</returns>
        Task<PendingTransaction> Get(CustomerAccessToken token, Guid financialInstitutionId, Guid accountId, Guid id, CancellationToken? cancellationToken = null);
    }
}
