using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="ITransactionDeleteRequests" />
    public class TransactionDeleteRequests : ResourceClient<TransactionDeleteRequestResponse, object, object, object, CustomerAccessToken>, ITransactionDeleteRequests
    {
        private const string EntityName = "transaction-delete-requests";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public TransactionDeleteRequests(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        public Task<TransactionDeleteRequestResponse> CreateForApplication(TransactionDeleteRequest transactionDeleteRequest, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (transactionDeleteRequest is null)
                throw new ArgumentNullException(nameof(transactionDeleteRequest));

            var payload = new JsonApi.Data<TransactionDeleteRequest, object, object, object>
            {
                Type = "transactionDeleteRequest",
                Attributes = transactionDeleteRequest
            };

            return InternalCreate(null, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<TransactionDeleteRequestResponse> CreateForCustomer(CustomerAccessToken token, TransactionDeleteRequest transactionDeleteRequest, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (transactionDeleteRequest is null)
                throw new ArgumentNullException(nameof(transactionDeleteRequest));

            var payload = new JsonApi.Data<TransactionDeleteRequest, object, object, object>
            {
                Type = "transactionDeleteRequest",
                Attributes = transactionDeleteRequest
            };

            return InternalCreate(token, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<TransactionDeleteRequestResponse> CreateForAccount(CustomerAccessToken token, TransactionDeleteRequest transactionDeleteRequest, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (transactionDeleteRequest is null)
                throw new ArgumentNullException(nameof(transactionDeleteRequest));

            var payload = new JsonApi.Data<TransactionDeleteRequest, object, object, object>
            {
                Type = "transactionDeleteRequest",
                Attributes = transactionDeleteRequest
            };

            return InternalCreate(token, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        protected override TransactionDeleteRequestResponse Map(Data<TransactionDeleteRequestResponse, object, object, object> data)
        {
            if (data.Attributes == null)
                data.Attributes = new TransactionDeleteRequestResponse();

            return base.Map(data);
        }
    }

    /// <summary>
    /// This is an object representing a resource transaction-delete-request. This object will give you the details of the transaction-delete-request.
    /// </summary>
    public interface ITransactionDeleteRequests
    {
        /// <summary>
        /// Create Transaction Delete Request For Application
        /// </summary>
        /// <param name="transactionDeleteRequest">Details of the transaction-delete-request</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A transaction delete request resource</returns>
        Task<TransactionDeleteRequestResponse> CreateForApplication(TransactionDeleteRequest transactionDeleteRequest, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Create Transaction Delete Request For Customer
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="transactionDeleteRequest">Details of the transaction-delete-request</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A transaction delete request resource</returns>
        Task<TransactionDeleteRequestResponse> CreateForCustomer(CustomerAccessToken token, TransactionDeleteRequest transactionDeleteRequest, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Create Transaction Delete Request For Account
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="transactionDeleteRequest">Details of the transaction-delete-request</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A transaction delete request resource</returns>
        Task<TransactionDeleteRequestResponse> CreateForAccount(CustomerAccessToken token, TransactionDeleteRequest transactionDeleteRequest, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
    }
}
