using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <summary>
    /// <para>This is an object representing a bulk payment. When you want to initiate a bulk payment from one of your user's accounts, you have to create one to start the authorization flow.</para>
    /// <para>If you provide a redirect URI when creating the bulk payment, you will receive a redirect link to send your customer to to start the authorization flow. Note that for live bulk payments, your user must have already requested and been granted payment service for their organization to use this flow.</para>
    /// <para>Otherwise, the user can sign the bulk payment in the Ponto Dashboard.</para>
    /// <para>When authorizing bulk payment initiation in the sandbox, you should use the pre-filled credentials and 123456 as the digipass response.</para>
    /// </summary>
    public class BulkPayments : ResourceWithParentClient<BulkPaymentResponse, object, object, PaymentLinks>, IBulkPayments
    {
        private const string ParentEntityName = "accounts";
        private const string EntityName = "bulk-payments";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public BulkPayments(IApiClient apiClient, IAccessTokenProvider<Token> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<BulkPaymentResponse> Create(Token token, Guid accountId, BulkPaymentRequest payment, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (payment is null)
                throw new ArgumentNullException(nameof(payment));

            var payload = new JsonApi.Data<BulkPaymentRequest, object, object, object>
            {
                Type = "bulkPayment",
                Attributes = payment
            };

            return InternalCreate(token, new[] { accountId }, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<BulkPaymentResponse> Get(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, new[] { accountId }, id, cancellationToken);

        /// <inheritdoc />
        public Task Delete(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken) =>
            InternalDelete(token, new[] { accountId }, id, cancellationToken);

        /// <inheritdoc />
        protected override BulkPaymentResponse Map(JsonApi.Data<BulkPaymentResponse, object, object, PaymentLinks> data)
        {
            var result = base.Map(data);

            result.RedirectUri = data.Links?.RedirectString;

            return result;
        }
    }

    /// <summary>
    /// <para>This is an object representing a bulk payment. When you want to initiate a bulk payment from one of your user's accounts, you have to create one to start the authorization flow.</para>
    /// <para>If you provide a redirect URI when creating the bulk payment, you will receive a redirect link to send your customer to to start the authorization flow. Note that for live bulk payments, your user must have already requested and been granted payment service for their organization to use this flow.</para>
    /// <para>Otherwise, the user can sign the bulk payment in the Ponto Dashboard.</para>
    /// <para>When authorizing bulk payment initiation in the sandbox, you should use the pre-filled credentials and 123456 as the digipass response.</para>
    /// </summary>
    public interface IBulkPayments
    {
        /// <summary>
        /// Create Bulk Payment
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Account ID</param>
        /// <param name="payment">An object representing a bulk payment</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created bulk payment resource</returns>
        Task<BulkPaymentResponse> Create(Token token, Guid accountId, BulkPaymentRequest payment, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Bulk Payment
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Account ID</param>
        /// <param name="id">Bulk payment ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The specified bulk payment resource</returns>
        Task<BulkPaymentResponse> Get(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Delete Bulk Payment
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Account ID</param>
        /// <param name="id">Bulk payment ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        Task Delete(Token token, Guid accountId, Guid id, CancellationToken? cancellationToken = null);
    }
}
