using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="IPaymentInitiationRequestAuthorizations" />
    public class PaymentInitiationRequestAuthorizations : BasePaymentInitiationRequestAuthorizations<PaymentInitiationRequestAuthorizationRelationships>, IPaymentInitiationRequestAuthorizations
    {
        private const string ParentEntityName = "payment-initiation-requests";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PaymentInitiationRequestAuthorizations(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, ParentEntityName)
        { }

        /// <inheritdoc />
        protected override string GetStatus(PaymentInitiationRequestAuthorizationRelationships relationships) =>
            relationships?.PaymentInitiationRequest?.Data?.Attributes?.Status;
    }

    /// <summary>
    /// <p>This object represent the authorization resource. When you perform an Authorization flow using TPP managed authorization flow, you need to create an authorization resource to complete the flow.</p>
    /// <p>The attribute queryParameters contains the query parameters returned by the financial institution when the customer is redirected to your configured redirect uri.</p>
    /// </summary>
    public interface IPaymentInitiationRequestAuthorizations
    {
        /// <summary>
        /// Create bulk payment initiation request authorization
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="paymentInitiationRequestId">Payment initiation request ID</param>
        /// <param name="requestAuthorization">Details of the request authorization</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created Account Information Access Request Authorization resource</returns>
        Task<PaymentAuthorizationResponse> Create(CustomerAccessToken token, Guid financialInstitutionId, Guid paymentInitiationRequestId, RequestAuthorization requestAuthorization, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
    }
}
