using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;

namespace Ibanity.Apis.Client.Products.IsabelConnect
{
    /// <summary>
    /// <para>This is an object representing a bulk payment initiation request. When you want to request the initiation of payments on behalf of one of your customers, you can create one to start the authorization flow.</para>
    /// <para>When creating the request, you should provide the payment information by uploading a PAIN xml file. <see href="https://documentation.ibanity.com/isabel-connect/products#bulk-payment-initiation">Learn more about the supported formats in Isabel Connect</see>.</para>
    /// </summary>
    public class BulkPaymentInitiationRequests : ResourceClient<BulkPaymentInitiationRequest, object, object, object, string>, IBulkPaymentInitiationRequests
    {
        private const string EntityName = "bulk-payment-initiation-requests";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public BulkPaymentInitiationRequests(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        protected override string ParseId(string id) => id;
    }

    /// <summary>
    /// <para>This is an object representing a bulk payment initiation request. When you want to request the initiation of payments on behalf of one of your customers, you can create one to start the authorization flow.</para>
    /// <para>When creating the request, you should provide the payment information by uploading a PAIN xml file. <see href="https://documentation.ibanity.com/isabel-connect/products#bulk-payment-initiation">Learn more about the supported formats in Isabel Connect</see>.</para>
    /// </summary>
    public interface IBulkPaymentInitiationRequests
    {
    }
}
