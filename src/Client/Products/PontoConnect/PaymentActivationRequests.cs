using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <summary>
    /// <para>This is an object representing a payment activation request. If your customer has not yet requested payment activation for their organization (as indicated by the user info endpoint), you can redirect them to Ponto to submit a request for payment activation.</para>
    /// <para>When creating the payment activation request, you will receive the redirect link in the response. Your customer should be redirected to this url to begin the process. At the end of the flow, they will be returned to the redirect uri that you defined.</para>
    /// <para>When using this endpoint in the sandbox, the redirection flow will work but the user will not be prompted to request payment activation as this is enabled by default in the sandbox.</para>
    /// </summary>
    public class PaymentActivationRequests : ResourceClient<PaymentActivationRequest, object, object, PaymentActivationRequestLinks>, IPaymentActivationRequests
    {
        private const string EntityName = "payment-activation-requests";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PaymentActivationRequests(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        public Task<PaymentActivationRequest> Request(Token token, Uri redirect, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (redirect is null)
                throw new ArgumentNullException(nameof(redirect));

            var payload = new JsonApi.Data<PaymentActivationRequest, object, object, object>
            {
                Type = "paymentActivationRequest",
                Attributes = new PaymentActivationRequest { Redirect = redirect }
            };

            return InternalCreate(token, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PaymentActivationRequest> Request(Token token, string redirectUri, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(redirectUri))
                throw new ArgumentException($"'{nameof(redirectUri)}' cannot be null or whitespace.", nameof(redirectUri));

            return Request(
                token,
                new Uri(redirectUri),
                idempotencyKey,
                cancellationToken);
        }

        /// <inheritdoc />
        protected override PaymentActivationRequest Map(JsonApi.Data<PaymentActivationRequest, object, object, PaymentActivationRequestLinks> data)
        {
            if (data.Attributes == null)
                data.Attributes = new PaymentActivationRequest();

            var result = base.Map(data);

            result.Redirect = data.Links?.Redirect;

            return result;
        }
    }

    /// <summary>
    /// <para>This is an object representing a payment activation request. If your customer has not yet requested payment activation for their organization (as indicated by the user info endpoint), you can redirect them to Ponto to submit a request for payment activation.</para>
    /// <para>When creating the payment activation request, you will receive the redirect link in the response. Your customer should be redirected to this url to begin the process. At the end of the flow, they will be returned to the redirect uri that you defined.</para>
    /// <para>When using this endpoint in the sandbox, the redirection flow will work but the user will not be prompted to request payment activation as this is enabled by default in the sandbox.</para>
    /// </summary>
    public interface IPaymentActivationRequests
    {
        /// <summary>
        /// Request Payment Activation
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="redirect">URI that your user will be redirected to at the end of the authorization flow</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created payment activation request resource</returns>
        Task<PaymentActivationRequest> Request(Token token, Uri redirect, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Request Payment Activation
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="redirectUri">URI that your user will be redirected to at the end of the authorization flow</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created payment activation request resource</returns>
        Task<PaymentActivationRequest> Request(Token token, string redirectUri, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
    }
}
