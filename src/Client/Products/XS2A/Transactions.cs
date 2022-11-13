using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="ITransactions" />
    public class Transactions : ResourceWithParentClient<Transaction, object, TransactionRelationships, object, CustomerAccessToken>, ITransactions
    {
        private const string GrandParentEntityName = "customer/financial-institutions";
        private const string ParentEntityName = "accounts";
        private const string EntityName = "transactions";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Transactions(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { GrandParentEntityName, ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<IbanityCollection<Transaction>> List(CustomerAccessToken token, Guid financialInstitutionId, Guid accountId, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null) =>
            InternalCursorBasedList(token, new[] { financialInstitutionId, accountId }, null, pageLimit, pageBefore, pageAfter, cancellationToken);

        /// <inheritdoc />
        public Task<IbanityCollection<Transaction>> ListUpdatedForSynchronization(CustomerAccessToken token, Guid synchronizationId, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null) =>
            InternalCursorBasedList(
                token,
                $"customer/synchronizations/{synchronizationId}/updated-transactions",
                null,
                pageLimit,
                pageBefore,
                pageAfter,
                cancellationToken);

        /// <inheritdoc />
        protected override Transaction Map(JsonApi.Data<Transaction, object, TransactionRelationships, object> data)
        {
            var result = base.Map(data);

            result.AccountId = Guid.Parse(data.Relationships.Account.Data.Id);

            return result;
        }
    }

    /// <summary>
    /// <p>This is an object representing an account transaction. This object will give you the details of the financial transaction, including its amount and description.</p>
    /// <p>From this object, you can link back to its account.</p>
    /// <p>The transaction API endpoints are customer specific and therefore can only be accessed by providing the corresponding customer access token.</p>
    /// </summary>
    public interface ITransactions
    {
        /// <summary>
        /// List Transactions
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="accountId">Bank account ID</param>
        /// <param name="pageLimit">Number of items by page</param>
        /// <param name="pageBefore">Cursor that specifies the first resource of the next page</param>
        /// <param name="pageAfter">Cursor that specifies the last resource of the previous page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of transaction resources</returns>
        Task<IbanityCollection<Transaction>> List(CustomerAccessToken token, Guid financialInstitutionId, Guid accountId, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Updated Transactions for Synchronization
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="synchronizationId">Synchronization ID</param>
        /// <param name="pageLimit">Number of items by page</param>
        /// <param name="pageBefore">Cursor that specifies the first resource of the next page</param>
        /// <param name="pageAfter">Cursor that specifies the last resource of the previous page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of transaction resources updated during the synchronization</returns>
        Task<IbanityCollection<Transaction>> ListUpdatedForSynchronization(CustomerAccessToken token, Guid synchronizationId, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);
    }
}
