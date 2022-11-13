using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <summary>
    /// Abstract service to manage payment authorizations
    /// </summary>
    /// <typeparam name="TRelationships">Relationships type</typeparam>
    public abstract class BasePaymentInitiationRequestAuthorizations<TRelationships> : ResourceWithParentClient<PaymentAuthorizationResponse, object, TRelationships, AuthorizationLinks, CustomerAccessToken>
    {
        private const string GrandParentEntityName = "customer/financial-institutions";
        private const string EntityName = "authorizations";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        /// <param name="parentEntityName"></param>
        protected BasePaymentInitiationRequestAuthorizations(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix, string parentEntityName) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { GrandParentEntityName, parentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<PaymentAuthorizationResponse> Create(CustomerAccessToken token, Guid financialInstitutionId, Guid paymentInitiationRequestId, RequestAuthorization requestAuthorization, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (requestAuthorization is null)
                throw new ArgumentNullException(nameof(requestAuthorization));

            var payload = new JsonApi.Data<RequestAuthorization, object, TRelationships, object>
            {
                Type = "authorization",
                Attributes = requestAuthorization
            };

            return InternalCreate(token, new[] { financialInstitutionId, paymentInitiationRequestId }, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        protected override PaymentAuthorizationResponse Map(Data<PaymentAuthorizationResponse, object, TRelationships, AuthorizationLinks> data)
        {
            var result = base.Map(data);

            result.NextRedirect = data.Links?.NextRedirect;
            result.PaymentStatus = GetStatus(data.Relationships);

            return result;
        }

        /// <summary>
        /// Get payment status from relationships
        /// </summary>
        /// <param name="relationships">Relationship received in response</param>
        /// <returns>Payment status</returns>
        protected abstract string GetStatus(TRelationships relationships);
    }
}
