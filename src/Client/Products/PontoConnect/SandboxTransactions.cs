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
        public Task<IbanityCollection<SandboxTransaction>> List(Token token, Guid financialInstitutionId, Guid accountId, int? pageSize, Guid? pageBefore, Guid? pageAfter, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                new[] { financialInstitutionId, accountId },
                null,
                pageSize,
                pageBefore,
                pageAfter,
                cancellationToken);

        /// <inheritdoc />
        public Task<IbanityCollection<SandboxTransaction>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
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

            var payload = new JsonApi.Data<Transaction, object, object, object>
            {
                Type = "financialInstitutionTransaction",
                Attributes = transaction
            };

            return InternalCreate(token, new[] { financialInstitutionId, accountId }, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<SandboxTransaction> Update(Token token, Guid financialInstitutionId, Guid accountId, Guid id, Transaction transaction, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (transaction is null)
                throw new ArgumentNullException(nameof(transaction));

            var payload = new JsonApi.Data<Transaction, object, object, object>
            {
                Type = "financialInstitutionTransaction",
                Attributes = transaction
            };

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
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="pageBefore">Cursor that specifies the first resource of the next page</param>
        /// <param name="pageAfter">Cursor that specifies the last resource of the previous page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of financial institution transaction resources</returns>
        Task<IbanityCollection<SandboxTransaction>> List(Token token, Guid financialInstitutionId, Guid accountId, int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Financial Institution Transactions
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="continuationToken">Token referencing the page to request</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of financial institution transaction resources</returns>
        Task<IbanityCollection<SandboxTransaction>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Financial Institution Transaction
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="accountId">Account ID</param>
        /// <param name="id">Financial institution transaction ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The specified financial institution transaction resource</returns>
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
        /// <returns>The created financial institution transaction resource</returns>
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
        /// <returns>The updated financial institution transaction resource</returns>
        Task<SandboxTransaction> Update(Token token, Guid financialInstitutionId, Guid accountId, Guid id, Transaction transaction, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
    }
}
