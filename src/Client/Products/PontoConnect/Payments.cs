using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <summary>
    /// <para>This is an object representing a payment. When you want to initiate payment from one of your user's accounts, you have to create one to start the authorization flow.</para>
    /// <para>If you provide a redirect URI when creating the payment, you will receive a redirect link to send your customer to to start the authorization flow. Note that for live payments, your user must have already requested and been granted payment service for their organization to use this flow.</para>
    /// <para>Otherwise, the user can sign the payment in the Ponto Dashboard.</para>
    /// <para>When authorizing payment initiation in the sandbox, you should use the pre-filled credentials and 123456 as the digipass response.</para>
    /// </summary>
    public class Payments : ResourceWithParentClient<PaymentResponse, object, object, PaymentLinks>, IPayments
    {
        private const string ParentEntityName = "accounts";
        private const string EntityName = "payments";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Payments(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<PaymentResponse> Create(Token token, Guid accountId, PaymentRequest payment, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (payment is null)
                throw new ArgumentNullException(nameof(payment));

            var payload = new JsonApi.Data<PaymentRequest, object, object, object>
            {
                Type = "payment",
                Attributes = payment
            };

            return InternalCreate(token, new[] { accountId }, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PaymentResponse> Get(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, new[] { accountId }, id, cancellationToken);
        /// <inheritdoc />

        public Task Delete(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken) =>
            InternalDelete(token, new[] { accountId }, id, cancellationToken);

        /// <inheritdoc />
        protected override PaymentResponse Map(JsonApi.Data<PaymentResponse, object, object, PaymentLinks> data)
        {
            var result = base.Map(data);

            result.RedirectUri = data.Links?.RedirectString;

            return result;
        }
    }

    /// <summary>
    /// <para>This is an object representing a payment. When you want to initiate payment from one of your user's accounts, you have to create one to start the authorization flow.</para>
    /// <para>If you provide a redirect URI when creating the payment, you will receive a redirect link to send your customer to to start the authorization flow. Note that for live payments, your user must have already requested and been granted payment service for their organization to use this flow.</para>
    /// <para>Otherwise, the user can sign the payment in the Ponto Dashboard.</para>
    /// <para>When authorizing payment initiation in the sandbox, you should use the pre-filled credentials and 123456 as the digipass response.</para>
    /// </summary>
    public interface IPayments
    {
        /// <summary>
        /// Create Payment
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Account ID</param>
        /// <param name="payment">An object representing a payment</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created payment resource</returns>
        Task<PaymentResponse> Create(Token token, Guid accountId, PaymentRequest payment, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Payment
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Account ID</param>
        /// <param name="id">Payment ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The specified payment resource</returns>
        Task<PaymentResponse> Get(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Delete Payment
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Account ID</param>
        /// <param name="id">Payment ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        Task Delete(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken = null);
    }
}
