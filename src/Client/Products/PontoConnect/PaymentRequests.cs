using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <summary>
    /// <para>This is an object representing a payment request. When you want to initiate payment request from one of your user's accounts, you have to create one to start the authorization flow.</para>
    /// <para>If you provide a redirect URI when creating the payment request, you will receive a redirect link to send your customer to to start the authorization flow. Note that for live payment requests, your user must have already requested and been granted payment request service for their organization to use this flow.</para>
    /// <para>Otherwise, the user can sign the payment request in the Ponto Dashboard.</para>
    /// <para>When authorizing payment request initiation in the sandbox, you should use the pre-filled credentials and 123456 as the digipass response.</para>
    /// </summary>
    public class PaymentRequests : ResourceWithParentClient<PaymentRequestResponse, object, object, PaymentRequestLinks, Token>, IPaymentRequests
    {
        private const string ParentEntityName = "accounts";
        private const string EntityName = "payment-requests";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PaymentRequests(IApiClient apiClient, IAccessTokenProvider<Token> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<PaymentRequestResponse> Create(Token token, Guid accountId, PaymentRequestRequestInitiation paymentRequest, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (paymentRequest is null)
                throw new ArgumentNullException(nameof(paymentRequest));

            var payload = new JsonApi.Data<PaymentRequestRequestInitiation, object, object, object>
            {
                Type = "paymentRequest",
                Attributes = paymentRequest
            };

            return InternalCreate(token, new[] { accountId }, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PaymentRequestResponse> Get(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, new[] { accountId }, id, cancellationToken);
        /// <inheritdoc />

        public Task Delete(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken) =>
            InternalDelete(token, new[] { accountId }, id, cancellationToken);

        /// <inheritdoc />
        protected override PaymentRequestResponse Map(JsonApi.Data<PaymentRequestResponse, object, object, PaymentRequestLinks> data)
        {
            var result = base.Map(data);

            result.SigningUri = data.Links?.RedirectString;
            result.RedirectUri = data.Links?.RedirectString;

            return result;
        }
    }

    /// <summary>
    /// <para>This is an object representing a payment request. When you want to initiate payment request from one of your user's accounts, you have to create one to start the authorization flow.</para>
    /// <para>If you provide a redirect URI when creating the payment request, you will receive a redirect link to send your customer to to start the authorization flow. Note that for live payments, your user must have already requested and been granted payment request service for their organization to use this flow.</para>
    /// <para>Otherwise, the user can sign the payment request in the Ponto Dashboard.</para>
    /// <para>When authorizing payment request initiation in the sandbox, you should use the pre-filled credentials and 123456 as the digipass response.</para>
    /// </summary>
    public interface IPaymentRequests
    {
        /// <summary>
        /// Create PaymentRequest
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Account ID</param>
        /// <param name="paymentRequest">An object representing a payment request</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created payment request resource</returns>
        Task<PaymentRequestResponse> Create(Token token, Guid accountId, PaymentRequestRequestInitiation paymentRequest, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get PaymentRequest
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Account ID</param>
        /// <param name="id">PaymentRequest ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The specified payment request resource</returns>
        Task<PaymentRequestResponse> Get(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Delete PaymentRequest
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Account ID</param>
        /// <param name="id">PaymentRequest ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        Task Delete(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken = null);
    }
}
