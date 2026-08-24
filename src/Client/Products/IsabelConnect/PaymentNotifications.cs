using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.IsabelConnect
{
    /// <summary>
    /// This is an object representing a payment notification.
    /// </summary>
    public class PaymentNotifications : ResourceClient<PaymentNotification, object, PaymentNotificationRelationships, object, string, Token>, IPaymentNotifications
    {
        private const string EntityName = "bulk-payment-initiation-requests/notifications";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PaymentNotifications(IApiClient apiClient, IAccessTokenProvider<Token> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        protected override string ParseId(string id) => id;

        /// <inheritdoc />
        protected override PaymentNotification Map(JsonApi.Data<PaymentNotification, object, PaymentNotificationRelationships, object> data)
        {
            var result = base.Map(data);

            if (data.Relationships?.Payment?.Data != null)
                result.PaymentId = data.Relationships.Payment.Data.Id;

            return result;
        }

        /// <inheritdoc />
        public Task<IsabelCollection<PaymentNotification>> List(Token token, long? pageOffset = null, int? pageSize = null, CancellationToken? cancellationToken = null) =>
            InternalOffsetBasedList(
                token ?? throw new ArgumentNullException(nameof(token)),
                null,
                null,
                pageOffset,
                pageSize,
                cancellationToken);

        /// <inheritdoc />
        public Task Delete(Token token, string id, CancellationToken? cancellationToken = null) =>
            InternalDelete(
                token ?? throw new ArgumentNullException(nameof(token)),
                id,
                cancellationToken);
    }

    /// <summary>
    /// This is an object representing a payment notification.
    /// </summary>
    public interface IPaymentNotifications
    {
        /// <summary>
        /// List Payment Notifications
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="pageOffset">Defines the start position of the results by giving the number of records to be skipped</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of payment notification resources</returns>
        Task<IsabelCollection<PaymentNotification>> List(Token token, long? pageOffset = null, int? pageSize = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Acknowledge Payment Notification
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="id">Payment Notification ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        Task Delete(Token token, string id, CancellationToken? cancellationToken = null);
    }
}
