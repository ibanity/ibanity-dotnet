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

        /// <inheritdoc />
        public Task<BulkPaymentInitiationRequestResponse> Create(CustomerAccessToken token, Guid financialInstitutionId, BulkPaymentInitiationRequest paymentInitiationRequest, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (paymentInitiationRequest is null)
                throw new ArgumentNullException(nameof(paymentInitiationRequest));

            var payload = new JsonApi.Data<BulkPaymentInitiationRequest, object, object, object>
            {
                Type = "paymentInitiationRequest",
                Attributes = paymentInitiationRequest
            };

            return InternalCreate(token, new[] { financialInstitutionId }, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<BulkPaymentInitiationRequestResponse> Get(CustomerAccessToken token, Guid financialInstitutionId, Guid id, CancellationToken? cancellationToken = null) =>
            InternalGet(token, new[] { financialInstitutionId }, id, cancellationToken);
    }

    /// <summary>
    /// This is an object representing a bulk payment initiation request. When you want to request the initiation of payments on behalf of one of your customers, you can create one to start the authorization flow.
    /// </summary>
    public interface IBulkPaymentInitiationRequests
    {
        /// <summary>
        /// Create Bulk Payment Initiation Request
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="paymentInitiationRequest">Details of the periodic payment initiation request</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created payment initiation request resource</returns>
        Task<BulkPaymentInitiationRequestResponse> Create(CustomerAccessToken token, Guid financialInstitutionId, BulkPaymentInitiationRequest paymentInitiationRequest, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Bulk Payment Initiation Request
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="id">Bulk Payment Initiation Request ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The specified payment initiation request resource</returns>
        Task<BulkPaymentInitiationRequestResponse> Get(CustomerAccessToken token, Guid financialInstitutionId, Guid id, CancellationToken? cancellationToken = null);
    }
}
