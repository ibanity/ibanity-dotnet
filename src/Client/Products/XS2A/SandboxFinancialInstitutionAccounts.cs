using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="ISandboxFinancialInstitutionAccounts" />
    public class SandboxFinancialInstitutionAccounts : ResourceWithParentClient<SandboxFinancialInstitutionAccountResponse, object, object, object, CustomerAccessToken>, ISandboxFinancialInstitutionAccounts
    {
        private const string GrandParentEntityName = "financial-institutions";
        private const string ParentEntityName = "financial-institution-users";
        private const string EntityName = "financial-institution-accounts";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public SandboxFinancialInstitutionAccounts(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { GrandParentEntityName, ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<IbanityCollection<SandboxFinancialInstitutionAccountResponse>> List(Guid financialInstitutionId, Guid financialInstitutionUserId, int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null) =>
            InternalCursorBasedList(null, new[] { financialInstitutionId, financialInstitutionUserId }, null, pageSize, pageBefore, pageAfter, cancellationToken);

        /// <inheritdoc />
        public Task<SandboxFinancialInstitutionAccountResponse> Get(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid id, CancellationToken? cancellationToken = null) =>
            InternalGet(null, new[] { financialInstitutionId, financialInstitutionUserId }, id, cancellationToken);

        /// <inheritdoc />
        public Task<SandboxFinancialInstitutionAccountResponse> Create(Guid financialInstitutionId, Guid financialInstitutionUserId, SandboxFinancialInstitutionAccount sandboxFinancialInstitutionAccount, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (sandboxFinancialInstitutionAccount is null)
                throw new ArgumentNullException(nameof(sandboxFinancialInstitutionAccount));

            var payload = new JsonApi.Data<SandboxFinancialInstitutionAccount, object, object, object>
            {
                Type = "financialInstitutionUser",
                Attributes = sandboxFinancialInstitutionAccount
            };

            return InternalCreate(null, new[] { financialInstitutionId, financialInstitutionUserId }, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task Delete(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid id, CancellationToken? cancellationToken = null) =>
            InternalDelete(null, new[] { financialInstitutionId, financialInstitutionUserId }, id, cancellationToken);
    }

    /// <summary>
    /// <p>This is an object representing a financial institution account, a fake account you create for test purposes.</p>
    /// <p>In addition to the regular account API calls, there are sandbox-specific endpoints available to manage your sandbox data.</p>
    /// <p>A financial institution account belongs to a financial institution user and financial institution and can have many associated financial institution transactions.</p>
    /// </summary>
    public interface ISandboxFinancialInstitutionAccounts
    {
        /// <summary>
        /// List sandbox financial institution accounts
        /// </summary>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="financialInstitutionUserId">Financial institution user ID</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="pageBefore">Cursor that specifies the first resource of the next page</param>
        /// <param name="pageAfter">Cursor that specifies the last resource of the previous page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of financial institution resources</returns>
        Task<IbanityCollection<SandboxFinancialInstitutionAccountResponse>> List(Guid financialInstitutionId, Guid financialInstitutionUserId, int? pageSize = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get sandbox financial institution account
        /// </summary>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="financialInstitutionUserId">Financial institution user ID</param>
        /// <param name="id">Sandbox financial institution account ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of financial institution resources</returns>
        Task<SandboxFinancialInstitutionAccountResponse> Get(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Create sandbox financial institution account
        /// </summary>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="financialInstitutionUserId">Financial institution user ID</param>
        /// <param name="sandboxFinancialInstitutionAccount">Details of the sandbox financial institution account</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created sandbox financial institution account resource</returns>
        Task<SandboxFinancialInstitutionAccountResponse> Create(Guid financialInstitutionId, Guid financialInstitutionUserId, SandboxFinancialInstitutionAccount sandboxFinancialInstitutionAccount, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Delete sandbox financial institution account
        /// </summary>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="financialInstitutionUserId">Financial institution user ID</param>
        /// <param name="id">Sandbox financial institution account ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        Task Delete(Guid financialInstitutionId, Guid financialInstitutionUserId, Guid id, CancellationToken? cancellationToken = null);
    }
}
