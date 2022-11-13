using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="IPeriodicPaymentInitiationRequests" />
    public class PeriodicPaymentInitiationRequests : ResourceWithParentClient<PeriodicPaymentInitiationRequestResponse, object, object, object, CustomerAccessToken>, IPeriodicPaymentInitiationRequests
    {
        private const string ParentEntityName = "customer/financial-institutions";
        private const string EntityName = "periodic-payment-initiation-requests";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PeriodicPaymentInitiationRequests(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<PeriodicPaymentInitiationRequestResponse> Create(CustomerAccessToken token, Guid financialInstitutionId, PeriodicPaymentInitiationRequest paymentInitiationRequest, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (paymentInitiationRequest is null)
                throw new ArgumentNullException(nameof(paymentInitiationRequest));

            var payload = new JsonApi.Data<PeriodicPaymentInitiationRequest, object, object, object>
            {
                Type = "paymentInitiationRequest",
                Attributes = paymentInitiationRequest
            };

            return InternalCreate(token, new[] { financialInstitutionId }, payload, idempotencyKey, cancellationToken);
        }
    }

    /// <summary>
    /// <p>This is an object representing a payment initiation request. When you want to initiate payment from one of your customers, you have to create one to start the authorization flow.</p>
    /// <p>When creating the payment initiation request, you will receive the redirect link in the response. Your customer should be redirected to this url to begin the authorization process. At the end of the flow, they will be returned to the redirect uri that you defined.</p>
    /// <p>If the payment initiation is not authorized (for example when the customer cancels the flow), an error query parameter will be added to the redirect uri with the value rejected.</p>
    /// <p>When authorizing payment initiation from a financial institution user (in the sandbox), you should use 123456 as the digipass response.</p>
    /// </summary>
    public interface IPeriodicPaymentInitiationRequests
    {
        /// <summary>
        /// Create Periodic Payment Initiation Request
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="paymentInitiationRequest">Details of the periodic payment initiation request</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created payment initiation request resource</returns>
        Task<PeriodicPaymentInitiationRequestResponse> Create(CustomerAccessToken token, Guid financialInstitutionId, PeriodicPaymentInitiationRequest paymentInitiationRequest, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
    }
}
