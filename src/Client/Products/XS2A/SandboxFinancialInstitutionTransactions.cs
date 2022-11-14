using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="ISandboxFinancialInstitutionTransactions" />
    public class SandboxFinancialInstitutionTransactions : ResourceWithParentClient<SandboxFinancialInstitutionTransactionResponse, object, object, object, CustomerAccessToken>, ISandboxFinancialInstitutionTransactions
    {
        private const string GrandGrandParentEntityName = "financial-institutions";
        private const string GrandParentEntityName = "financial-institution-users";
        private const string ParentEntityName = "financial-institution-accounts";
        private const string EntityName = "financial-institution-transactions";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public SandboxFinancialInstitutionTransactions(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { GrandGrandParentEntityName, GrandParentEntityName, ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<IbanityCollection<SandboxFinancialInstitutionTransactionResponse>> List(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null) =>
            InternalCursorBasedList(null, new[] { financialInstitutionId, financialInstitutionUserId, financialInstitutionAccountId }, null, pageSize, pageBefore, pageAfter, cancellationToken);

        /// <inheritdoc />
        public Task<SandboxFinancialInstitutionTransactionResponse> Get(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, Guid id, CancellationToken? cancellationToken = null) =>
            InternalGet(null, new[] { financialInstitutionId, financialInstitutionUserId, financialInstitutionAccountId }, id, cancellationToken);

        /// <inheritdoc />
        public Task<SandboxFinancialInstitutionTransactionResponse> Create(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, SandboxFinancialInstitutionTransaction sandboxFinancialInstitutionTransaction, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (sandboxFinancialInstitutionTransaction is null)
                throw new ArgumentNullException(nameof(sandboxFinancialInstitutionTransaction));

            var payload = new JsonApi.Data<SandboxFinancialInstitutionTransaction, object, object, object>
            {
                Type = "financialInstitutionTransaction",
                Attributes = sandboxFinancialInstitutionTransaction
            };

            return InternalCreate(null, new[] { financialInstitutionId, financialInstitutionUserId, financialInstitutionAccountId }, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<SandboxFinancialInstitutionTransactionResponse> Update(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, Guid id, SandboxFinancialInstitutionTransaction sandboxFinancialInstitutionTransaction, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (sandboxFinancialInstitutionTransaction is null)
                throw new ArgumentNullException(nameof(sandboxFinancialInstitutionTransaction));

            var payload = new JsonApi.Data<SandboxFinancialInstitutionTransaction, object, object, object>
            {
                Type = "financialInstitutionTransaction",
                Attributes = sandboxFinancialInstitutionTransaction
            };

            return InternalUpdate(null, new[] { financialInstitutionId, financialInstitutionUserId, financialInstitutionAccountId }, id, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task Delete(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, Guid id, CancellationToken? cancellationToken = null) =>
            InternalDelete(null, new[] { financialInstitutionId, financialInstitutionUserId, financialInstitutionAccountId }, id, cancellationToken);
    }

    /// <summary>
    /// <p>This is an object representing a financial institution transaction, a fake transaction on a fake account you can create for test purposes.</p>
    /// <p>In addition to the regular transaction API calls, there are sandbox-specific endpoints available to manage your sandbox data.</p>
    /// <p>From this object, you can follow the link to its related financial institution account</p>
    /// </summary>
    public interface ISandboxFinancialInstitutionTransactions
    {
        /// <summary>
        /// List sandbox financial institution transactions
        /// </summary>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="financialInstitutionUserId">Financial institution user ID</param>
        /// <param name="financialInstitutionAccountId">Financial institution account ID</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="pageBefore">Cursor that specifies the first resource of the next page</param>
        /// <param name="pageAfter">Cursor that specifies the last resource of the previous page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of financial institution resources</returns>
        Task<IbanityCollection<SandboxFinancialInstitutionTransactionResponse>> List(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get sandbox financial institution transaction
        /// </summary>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="financialInstitutionUserId">Financial institution user ID</param>
        /// <param name="financialInstitutionAccountId">Financial institution account ID</param>
        /// <param name="id">Sandbox financial institution transaction ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of financial institution resources</returns>
        Task<SandboxFinancialInstitutionTransactionResponse> Get(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Create sandbox financial institution transaction
        /// </summary>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="financialInstitutionUserId">Financial institution user ID</param>
        /// <param name="financialInstitutionAccountId">Financial institution account ID</param>
        /// <param name="sandboxFinancialInstitutionTransaction">Details of the sandbox financial institution transaction</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created sandbox financial institution transaction resource</returns>
        Task<SandboxFinancialInstitutionTransactionResponse> Create(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, SandboxFinancialInstitutionTransaction sandboxFinancialInstitutionTransaction, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Update sandbox financial institution transaction
        /// </summary>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="financialInstitutionUserId">Financial institution user ID</param>
        /// <param name="financialInstitutionAccountId">Financial institution account ID</param>
        /// <param name="id">Financial institution transaction ID</param>
        /// <param name="sandboxFinancialInstitutionTransaction">Details of the sandbox financial institution transaction</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created sandbox financial institution transaction resource</returns>
        Task<SandboxFinancialInstitutionTransactionResponse> Update(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, Guid id, SandboxFinancialInstitutionTransaction sandboxFinancialInstitutionTransaction, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Delete sandbox financial institution transaction
        /// </summary>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="financialInstitutionUserId">Financial institution user ID</param>
        /// <param name="financialInstitutionAccountId">Financial institution account ID</param>
        /// <param name="id">Sandbox financial institution transaction ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        Task Delete(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, Guid id, CancellationToken? cancellationToken = null);
    }
}
