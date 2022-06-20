using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <summary>
    /// <para>This is an object representing an account transaction. This object will give you the details of the financial transaction, including its amount and description.</para>
    /// <para>From this object, you can link back to its account.</para>
    /// </summary>
    public class Transactions : ResourceWithParentClient<TransactionResponse, object, TransactionRelationships, object>, ITransactions
    {
        private const string ParentEntityName = "accounts";
        private const string EntityName = "transactions";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Transactions(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<IbanityCollection<TransactionResponse>> List(Token token, Guid accountId, int? pageSize, Guid? pageBefore, Guid? pageAfter, CancellationToken? cancellationToken) =>
            InternalCursorBasedList(
                token ?? throw new ArgumentNullException(nameof(token)),
                new[] { accountId },
                null,
                pageSize,
                pageBefore,
                pageAfter,
                cancellationToken);

        /// <inheritdoc />
        public Task<IbanityCollection<TransactionResponse>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalCursorBasedList(
                token ?? throw new ArgumentNullException(nameof(token)),
                continuationToken ?? throw new ArgumentNullException(nameof(continuationToken)),
                cancellationToken);

        /// <inheritdoc />
        public Task<IbanityCollection<TransactionResponse>> ListUpdatedForSynchronization(Token token, Guid synchronizationId, int? pageSize, Guid? pageBefore, Guid? pageAfter, CancellationToken? cancellationToken) =>
            InternalCursorBasedList(
                token ?? throw new ArgumentNullException(nameof(token)),
                $"{UrlPrefix}/synchronizations/{synchronizationId}/updated-transactions",
                null,
                pageSize,
                pageBefore,
                pageAfter,
                cancellationToken);

        /// <inheritdoc />
        public Task<IbanityCollection<TransactionResponse>> ListUpdatedForSynchronization(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalCursorBasedList(
                token ?? throw new ArgumentNullException(nameof(token)),
                continuationToken ?? throw new ArgumentNullException(nameof(continuationToken)),
                cancellationToken);

        /// <inheritdoc />
        public Task<TransactionResponse> Get(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, new[] { accountId }, id, cancellationToken);

        /// <inheritdoc />
        protected override TransactionResponse Map(JsonApi.Data<TransactionResponse, object, TransactionRelationships, object> data)
        {
            var result = base.Map(data);

            result.AccountId = Guid.Parse(data.Relationships.Account.Data.Id);

            return result;
        }
    }

    /// <summary>
    /// <para>This is an object representing an account transaction. This object will give you the details of the financial transaction, including its amount and description.</para>
    /// <para>From this object, you can link back to its account.</para>
    /// </summary>
    public interface ITransactions
    {
        /// <summary>
        /// List Transactions
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Bank account ID</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="pageBefore">Cursor that specifies the first resource of the next page</param>
        /// <param name="pageAfter">Cursor that specifies the last resource of the previous page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of transaction resources</returns>
        Task<IbanityCollection<TransactionResponse>> List(Token token, Guid accountId, int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Transactions
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="continuationToken">Token referencing the page to request</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of transaction resources</returns>
        Task<IbanityCollection<TransactionResponse>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Transactions updated during the synchronization
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="synchronizationId">Synchronization ID</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="pageBefore">Cursor that specifies the first resource of the next page</param>
        /// <param name="pageAfter">Cursor that specifies the last resource of the previous page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of transaction resources updated during the synchronization</returns>
        Task<IbanityCollection<TransactionResponse>> ListUpdatedForSynchronization(Token token, Guid synchronizationId, int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Transactions updated during the synchronization
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="continuationToken">Token referencing the page to request</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of transaction resources updated during the synchronization</returns>
        Task<IbanityCollection<TransactionResponse>> ListUpdatedForSynchronization(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Transaction
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Bank account ID</param>
        /// <param name="id">Transaction ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The specified transaction resource</returns>
        Task<TransactionResponse> Get(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken = null);
    }
}
