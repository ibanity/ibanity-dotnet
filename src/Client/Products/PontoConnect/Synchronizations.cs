using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <summary>
    /// This is an object representing a resource synchronization. This object will give you the details of the synchronization, including its resource, type, and status.
    /// </summary>
    public class Synchronizations : ResourceClient<Synchronization, object, object, object>, ISynchronizations
    {
        private const string EntityName = "synchronizations";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Synchronizations(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        public Task<Synchronization> Create(Token token, SynchronizationRequest synchronization, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (synchronization is null)
                throw new ArgumentNullException(nameof(synchronization));

            var payload = new JsonApi.Data<SynchronizationRequest, object, object, object>();
            payload.Type = "synchronization";
            payload.Attributes = synchronization;

            return InternalCreate(token, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Synchronization> Get(Token token, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, id, cancellationToken);
    }

    /// <summary>
    /// This is an object representing a resource synchronization. This object will give you the details of the synchronization, including its resource, type, and status.
    /// </summary>
    public interface ISynchronizations
    {
        /// <summary>
        /// Create Synchronization
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="synchronization">Details of the synchronization, including its resource, type, and status</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A synchronization resource</returns>
        Task<Synchronization> Create(Token token, SynchronizationRequest synchronization, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Synchronization
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="id">ID of the synchronization</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A synchronization resource</returns>
        Task<Synchronization> Get(Token token, Guid id, CancellationToken? cancellationToken = null);
    }
}
