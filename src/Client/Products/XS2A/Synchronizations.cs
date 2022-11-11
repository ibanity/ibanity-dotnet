using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="ISynchronizations" />
    public class Synchronizations : ResourceClient<SynchronizationResponse, object, object, object, CustomerAccessToken>, ISynchronizations
    {
        private const string EntityName = "customer/synchronizations";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Synchronizations(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }
    }

    /// <summary>
    /// <p>This is an object representing a resource synchronization. This object will give you the details of the synchronization, including its resource, type, and status.</p>
    /// <p>The synchronization API endpoints are customer specific and therefore can only be accessed by providing the corresponding customer access token.</p>
    /// </summary>
    public interface ISynchronizations
    {
    }
}
