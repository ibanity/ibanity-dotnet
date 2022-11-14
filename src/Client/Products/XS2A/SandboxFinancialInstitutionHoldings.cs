using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="ISandboxFinancialInstitutionHoldings" />
    public class SandboxFinancialInstitutionHoldings : ResourceWithParentClient<SandboxFinancialInstitutionHoldingResponse, object, object, object, CustomerAccessToken>, ISandboxFinancialInstitutionHoldings
    {
        private const string GrandGrandParentEntityName = "financial-institutions";
        private const string GrandParentEntityName = "financial-institution-users";
        private const string ParentEntityName = "financial-institution-accounts";
        private const string EntityName = "financial-institution-holdings";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public SandboxFinancialInstitutionHoldings(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { GrandGrandParentEntityName, GrandParentEntityName, ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<IbanityCollection<SandboxFinancialInstitutionHoldingResponse>> List(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null) =>
            InternalCursorBasedList(null, new[] { financialInstitutionId, financialInstitutionUserId, financialInstitutionAccountId }, null, pageSize, pageBefore, pageAfter, cancellationToken);

        /// <inheritdoc />
        public Task<SandboxFinancialInstitutionHoldingResponse> Get(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, Guid id, CancellationToken? cancellationToken = null) =>
            InternalGet(null, new[] { financialInstitutionId, financialInstitutionUserId, financialInstitutionAccountId }, id, cancellationToken);

        /// <inheritdoc />
        public Task<SandboxFinancialInstitutionHoldingResponse> Create(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, SandboxFinancialInstitutionHolding sandboxFinancialInstitutionHolding, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (sandboxFinancialInstitutionHolding is null)
                throw new ArgumentNullException(nameof(sandboxFinancialInstitutionHolding));

            var payload = new JsonApi.Data<SandboxFinancialInstitutionHolding, object, object, object>
            {
                Type = "financialInstitutionHolding",
                Attributes = sandboxFinancialInstitutionHolding
            };

            return InternalCreate(null, new[] { financialInstitutionId, financialInstitutionUserId, financialInstitutionAccountId }, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<SandboxFinancialInstitutionHoldingResponse> Update(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, Guid id, SandboxFinancialInstitutionHolding sandboxFinancialInstitutionHolding, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (sandboxFinancialInstitutionHolding is null)
                throw new ArgumentNullException(nameof(sandboxFinancialInstitutionHolding));

            var payload = new JsonApi.Data<SandboxFinancialInstitutionHolding, object, object, object>
            {
                Type = "financialInstitutionTransaction",
                Attributes = sandboxFinancialInstitutionHolding
            };

            return InternalUpdate(null, new[] { financialInstitutionId, financialInstitutionUserId, financialInstitutionAccountId }, id, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task Delete(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, Guid id, CancellationToken? cancellationToken = null) =>
            InternalDelete(null, new[] { financialInstitutionId, financialInstitutionUserId, financialInstitutionAccountId }, id, cancellationToken);
    }

    /// <summary>
    /// <p>This is an object representing a financial institution holding, a fake holding on a fake securities account you can create for test purposes.</p>
    /// <p>In addition to the regular holding API calls, there are sandbox-specific endpoints available to manage your sandbox data.</p>
    /// <p>From this object, you can follow the link to its related financial institution account</p>
    /// </summary>
    public interface ISandboxFinancialInstitutionHoldings
    {
        /// <summary>
        /// List sandbox financial institution holdings
        /// </summary>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="financialInstitutionUserId">Financial institution user ID</param>
        /// <param name="financialInstitutionAccountId">Financial institution account ID</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="pageBefore">Cursor that specifies the first resource of the next page</param>
        /// <param name="pageAfter">Cursor that specifies the last resource of the previous page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of financial institution resources</returns>
        Task<IbanityCollection<SandboxFinancialInstitutionHoldingResponse>> List(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get sandbox financial institution holding
        /// </summary>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="financialInstitutionUserId">Financial institution user ID</param>
        /// <param name="financialInstitutionAccountId">Financial institution account ID</param>
        /// <param name="id">Sandbox financial institution holding ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of financial institution resources</returns>
        Task<SandboxFinancialInstitutionHoldingResponse> Get(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Create sandbox financial institution holding
        /// </summary>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="financialInstitutionUserId">Financial institution user ID</param>
        /// <param name="financialInstitutionAccountId">Financial institution account ID</param>
        /// <param name="sandboxFinancialInstitutionHolding">Details of the sandbox financial institution holding</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created sandbox financial institution holding resource</returns>
        Task<SandboxFinancialInstitutionHoldingResponse> Create(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, SandboxFinancialInstitutionHolding sandboxFinancialInstitutionHolding, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Delete sandbox financial institution holding
        /// </summary>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="financialInstitutionUserId">Financial institution user ID</param>
        /// <param name="financialInstitutionAccountId">Financial institution account ID</param>
        /// <param name="id">Sandbox financial institution holding ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        Task Delete(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid financialInstitutionAccountId, Guid id, CancellationToken? cancellationToken = null);
    }
}
