using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;

namespace Ibanity.Apis.Client.Products.IsabelConnect
{
    /// <summary>
    /// Payment status for a bulk payment initiation request, returned as a pain.002 XML document.
    /// </summary>
    public class PaymentStatuses : IPaymentStatuses
    {
        private readonly IApiClient _apiClient;
        private readonly IAccessTokenProvider<Token> _accessTokenProvider;
        private readonly string _urlPrefix;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PaymentStatuses(IApiClient apiClient, IAccessTokenProvider<Token> accessTokenProvider, string urlPrefix)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _accessTokenProvider = accessTokenProvider ?? throw new ArgumentNullException(nameof(accessTokenProvider));
            _urlPrefix = urlPrefix;
        }

        /// <inheritdoc />
        public async Task Get(Token token, string bulkPaymentInitiationRequestId, Stream target, string notificationId = null, CancellationToken? cancellationToken = null)
        {
            var path = $"{_urlPrefix}/bulk-payment-initiation-requests/{bulkPaymentInitiationRequestId}/payment-status";

            if (!string.IsNullOrWhiteSpace(notificationId))
                path += $"?notificationId={notificationId}";

            await _apiClient.GetToStream(
                path,
                (await _accessTokenProvider.RefreshToken(token ?? throw new ArgumentNullException(nameof(token))).ConfigureAwait(false)).AccessToken,
                null,
                target,
                cancellationToken ?? CancellationToken.None).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Payment status for a bulk payment initiation request, returned as a pain.002 XML document.
    /// </summary>
    public interface IPaymentStatuses
    {
        /// <summary>
        /// Get Payment Status
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="bulkPaymentInitiationRequestId">Bulk Payment Initiation Request ID</param>
        /// <param name="target">Destination stream where the pain.002 XML will be written</param>
        /// <param name="notificationId">Optional notification ID to filter the payment status</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <remarks>Result will be written to the provided stream.</remarks>
        Task Get(Token token, string bulkPaymentInitiationRequestId, Stream target, string notificationId = null, CancellationToken? cancellationToken = null);
    }
}
