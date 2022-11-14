using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="ISandboxFinancialInstitutionUsers" />
    public class SandboxFinancialInstitutionUsers : ResourceClient<SandboxFinancialInstitutionUserResponse, object, object, object, CustomerAccessToken>, ISandboxFinancialInstitutionUsers
    {
        private const string EntityName = "financial-institution-users";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public SandboxFinancialInstitutionUsers(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        public Task<SandboxFinancialInstitutionUserResponse> Create(SandboxFinancialInstitutionUser sandboxFinancialInstitutionUser, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (sandboxFinancialInstitutionUser is null)
                throw new ArgumentNullException(nameof(sandboxFinancialInstitutionUser));

            var payload = new JsonApi.Data<SandboxFinancialInstitutionUser, object, object, object>
            {
                Type = "financialInstitutionUser",
                Attributes = sandboxFinancialInstitutionUser
            };

            return InternalCreate(null, payload, idempotencyKey, cancellationToken);
        }
    }

    /// <summary>
    /// <p>This is an object representing a financial institution user. It is a fake financial institution customer you can create for test purposes.</p>
    /// <p>From this object, you can follow the links to its related financial institution accounts and transactions.</p>
    /// </summary>
    public interface ISandboxFinancialInstitutionUsers
    {
        /// <summary>
        /// Create sandbox financial institution user
        /// </summary>
        /// <param name="sandboxFinancialInstitutionUser">Details of the sandbox financial institution user</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created sandbox financial institution user resource</returns>
        Task<SandboxFinancialInstitutionUserResponse> Create(SandboxFinancialInstitutionUser sandboxFinancialInstitutionUser, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
    }
}
