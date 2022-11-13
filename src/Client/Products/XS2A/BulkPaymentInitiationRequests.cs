using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="IBulkPaymentInitiationRequests" />
    public class BulkPaymentInitiationRequests : ResourceWithParentClient<BulkPaymentInitiationRequestResponse, object, object, object, CustomerAccessToken>, IBulkPaymentInitiationRequests
    {
        private const string ParentEntityName = "customer/financial-institutions";
        private const string EntityName = "bulk-payment-initiation-requests";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public BulkPaymentInitiationRequests(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }
    }

    /// <summary>
    /// This is an object representing a bulk payment initiation request. When you want to request the initiation of payments on behalf of one of your customers, you can create one to start the authorization flow.
    /// </summary>
    public interface IBulkPaymentInitiationRequests
    {
    }
}
