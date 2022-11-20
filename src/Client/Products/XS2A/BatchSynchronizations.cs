using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="IBatchSynchronizations" />
    public class BatchSynchronizations : ResourceClient<BatchSynchronizationResponse, object, object, object, CustomerAccessToken>, IBatchSynchronizations
    {
        private const string EntityName = "batch-synchronizations";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public BatchSynchronizations(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        public Task<BatchSynchronizationResponse> Create(BatchSynchronization batchSynchronization, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (batchSynchronization is null)
                throw new ArgumentNullException(nameof(batchSynchronization));

            var payload = new JsonApi.Data<BatchSynchronization, object, object, object>
            {
                Type = "batchSynchronization",
                Attributes = batchSynchronization
            };

            return InternalCreate(null, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        protected override BatchSynchronizationResponse Map(Data<BatchSynchronizationResponse, object, object, object> data)
        {
            if (data.Attributes == null)
                data.Attributes = new BatchSynchronizationResponse();

            return base.Map(data);
        }
    }

    /// <summary>
    /// This is an object representing a resource batch-synchronization. This object will give you the details of the batch-synchronization.
    /// </summary>
    public interface IBatchSynchronizations
    {
        /// <summary>
        /// Create Batch Synchronization
        /// </summary>
        /// <param name="batchSynchronization">Details of the batch-synchronization</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A batch synchronization resource</returns>
        Task<BatchSynchronizationResponse> Create(BatchSynchronization batchSynchronization, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
    }
}
