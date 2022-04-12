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
    /// <para>This is an object representing a financial institution transaction, a fake transaction on a fake account you can create for test purposes.</para>
    /// <para>Once the account corresponding to the financial institution account has been synchronized, your custom financial institution transactions will be visible in the transactions list.</para>
    /// </summary>
    public class SandboxTransactions : ResourceWithParentClient<SandboxTransaction, object, object, object>, ISandboxTransactions
    {
        private const string TopLevelParentEntityName = "sandbox/financial-institutions";
        private const string ParentEntityName = "financial-institution-accounts";
        private const string EntityName = "financial-institution-transactions";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public SandboxTransactions(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { TopLevelParentEntityName, ParentEntityName, EntityName })
        {
        }

        /// <inheritdoc />
        public Task<PaginatedCollection<SandboxTransaction>> List(Token token, Guid financialInstitutionId, Guid accountId, IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                new[] { financialInstitutionId, accountId },
                filters,
                pageSize,
                cancellationToken);

        /// <inheritdoc />
        public Task<PaginatedCollection<SandboxTransaction>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                continuationToken ?? throw new ArgumentNullException(nameof(continuationToken)),
                cancellationToken);

        /// <inheritdoc />
        public Task<SandboxTransaction> Get(Token token, Guid financialInstitutionId, Guid accountId, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, new[] { financialInstitutionId, accountId }, id, cancellationToken);

        /// <inheritdoc />
        public Task<SandboxTransaction> Create(Token token, Guid financialInstitutionId, Guid accountId, Transaction transaction, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (transaction is null)
                throw new ArgumentNullException(nameof(transaction));

            var payload = new JsonApi.Data<Transaction, object, object, object>();
            payload.Type = "financialInstitutionTransaction";
            payload.Attributes = transaction;

            return InternalCreate(token, new[] { financialInstitutionId, accountId }, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<SandboxTransaction> Update(Token token, Guid financialInstitutionId, Guid accountId, Guid id, Transaction transaction, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (transaction is null)
                throw new ArgumentNullException(nameof(transaction));

            var payload = new JsonApi.Data<Transaction, object, object, object>();
            payload.Type = "financialInstitutionTransaction";
            payload.Attributes = transaction;

            return InternalUpdate(token, new[] { financialInstitutionId, accountId }, id, payload, idempotencyKey, cancellationToken);
        }
    }

    /// <summary>
    /// <para>This is an object representing a financial institution transaction, a fake transaction on a fake account you can create for test purposes.</para>
    /// <para>Once the account corresponding to the financial institution account has been synchronized, your custom financial institution transactions will be visible in the transactions list.</para>
    /// </summary>
    public interface ISandboxTransactions
    {
        /// <summary>
        /// List Financial Institution Transactions
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="accountId">Account ID</param>
        /// <param name="filters">Attributes to be filtered from the results</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a list of financial institution transaction resources</returns>
        Task<PaginatedCollection<SandboxTransaction>> List(Token token, Guid financialInstitutionId, Guid accountId, IEnumerable<Filter> filters = null, int? pageSize = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Financial Institution Transactions
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="continuationToken">Token referencing the page to request</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a list of financial institution transaction resources</returns>
        Task<PaginatedCollection<SandboxTransaction>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Financial Institution Transaction
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="accountId">Account ID</param>
        /// <param name="id">Financial institution transaction ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns the specified financial institution transaction resource</returns>
        Task<SandboxTransaction> Get(Token token, Guid financialInstitutionId, Guid accountId, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Create Financial Institution Transaction
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="accountId">Account ID</param>
        /// <param name="transaction">An object representing a financial institution transaction</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns the created financial institution transaction resource</returns>
        Task<SandboxTransaction> Create(Token token, Guid financialInstitutionId, Guid accountId, Transaction transaction, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Update Financial Institution Transaction
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="accountId">Account ID</param>
        /// <param name="id">Financial institution transaction ID</param>
        /// <param name="transaction">An object representing a financial institution transaction</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns the updated financial institution transaction resource</returns>
        Task<SandboxTransaction> Update(Token token, Guid financialInstitutionId, Guid accountId, Guid id, Transaction transaction, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
    }
}
