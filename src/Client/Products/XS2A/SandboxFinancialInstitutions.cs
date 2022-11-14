using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="ISandboxFinancialInstitutions" />
    public class SandboxFinancialInstitutions : ResourceClient<FinancialInstitution, object, object, object, CustomerAccessToken>, ISandboxFinancialInstitutions
    {
        private const string EntityName = "financial-institutions";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public SandboxFinancialInstitutions(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        public Task<FinancialInstitution> Create(SandboxFinancialInstitution sandboxFinancialInstitution, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (sandboxFinancialInstitution is null)
                throw new ArgumentNullException(nameof(sandboxFinancialInstitution));

            var payload = new JsonApi.Data<SandboxFinancialInstitution, object, object, object>
            {
                Type = "financialInstitution",
                Attributes = sandboxFinancialInstitution
            };

            return InternalCreate(null, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task Delete(Guid id, CancellationToken? cancellationToken = null) =>
            InternalDelete(null, id, cancellationToken);

        /// <inheritdoc />
        public Task<FinancialInstitution> Update(Guid id, SandboxFinancialInstitution sandboxFinancialInstitution, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (sandboxFinancialInstitution is null)
                throw new ArgumentNullException(nameof(sandboxFinancialInstitution));

            var payload = new JsonApi.Data<SandboxFinancialInstitution, object, object, object>
            {
                Type = "financialInstitution",
                Attributes = sandboxFinancialInstitution
            };

            return InternalUpdate(null, id, payload, idempotencyKey, cancellationToken);
        }
    }

    /// <summary>
    /// This is an object representing a financial institution, providing its basic details - including whether it is a sandbox object or not.
    /// </summary>
    /// <remarks>You can manage fake financial institutions in the sandbox using the create, update, and delete methods. Obviously, these endpoints will not work for real, live financial institutions.</remarks>
    public interface ISandboxFinancialInstitutions
    {
        /// <summary>
        /// Create sandbox financial institution
        /// </summary>
        /// <param name="sandboxFinancialInstitution">Details of the sandbox financial institution</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created sandbox financial institution resource</returns>
        Task<FinancialInstitution> Create(SandboxFinancialInstitution sandboxFinancialInstitution, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Update sandbox financial institution
        /// </summary>
        /// <param name="id">Sandbox financial institution ID</param>
        /// <param name="sandboxFinancialInstitution">Details of the sandbox financial institution</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created sandbox financial institution resource</returns>
        Task<FinancialInstitution> Update(Guid id, SandboxFinancialInstitution sandboxFinancialInstitution, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Delete sandbox financial institution
        /// </summary>
        /// <param name="id">Sandbox financial institution ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        Task Delete(Guid id, CancellationToken? cancellationToken = null);
    }
}
