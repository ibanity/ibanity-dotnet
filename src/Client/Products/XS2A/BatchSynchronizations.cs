using Ibanity.Apis.Client.Http;
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
    }

    /// <summary>
    /// This is an object representing a resource batch-synchronization. This object will give you the details of the batch-synchronization.
    /// </summary>
    public interface IBatchSynchronizations
    {
    }
}
