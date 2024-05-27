using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Webhooks.Jwt;
using Ibanity.Apis.Client.Webhooks.Models;

namespace Ibanity.Apis.Client.Webhooks
{
    /// <inheritdoc />
    public class WebhooksService : IWebhooksService
    {
        private static readonly IReadOnlyDictionary<string, Type> Types = new Dictionary<string, Type>
        {
            { "pontoConnect.synchronization.succeededWithoutChange", typeof(Payload<Models.PontoConnect.NestedSynchronizationSucceededWithoutChange>) },
            { "pontoConnect.synchronization.failed", typeof(Payload<Models.PontoConnect.NestedSynchronizationFailed>) },
            { "pontoConnect.account.detailsUpdated", typeof(Payload<Models.PontoConnect.NestedAccountDetailsUpdated>) },
            { "pontoConnect.account.reauthorized", typeof(Payload<Models.PontoConnect.NestedAccountReauthorized>) },
            { "pontoConnect.account.transactionsCreated", typeof(Payload<Models.PontoConnect.NestedAccountTransactionsCreated>) },
            { "pontoConnect.account.transactionsUpdated", typeof(Payload<Models.PontoConnect.NestedAccountTransactionsUpdated>) },
            { "pontoConnect.account.pendingTransactionsCreated", typeof(Payload<Models.PontoConnect.NestedAccountPendingTransactionsCreated>) },
            { "pontoConnect.account.pendingTransactionsUpdated", typeof(Payload<Models.PontoConnect.NestedAccountPendingTransactionsUpdated>) },
            { "pontoConnect.integration.created", typeof(Payload<Models.PontoConnect.NestedIntegrationCreated>) },
            { "pontoConnect.integration.revoked", typeof(Payload<Models.PontoConnect.NestedIntegrationRevoked>) },
            { "pontoConnect.integration.accountAdded", typeof(Payload<Models.PontoConnect.NestedIntegrationAccountAdded>) },
            { "pontoConnect.integration.accountRevoked", typeof(Payload<Models.PontoConnect.NestedIntegrationAccountRevoked>) },
            { "pontoConnect.organization.blocked", typeof(Payload<Models.PontoConnect.NestedOrganizationBlocked>) },
            { "pontoConnect.organization.unblocked", typeof(Payload<Models.PontoConnect.NestedOrganizationUnblocked>) },
            { "pontoConnect.paymentRequest.closed", typeof(Payload<Models.PontoConnect.NestedPaymentRequestClosed>) },
            { "xs2a.synchronization.succeededWithoutChange", typeof(Payload<Models.XS2A.NestedSynchronizationSucceededWithoutChange>) },
            { "xs2a.synchronization.failed", typeof(Payload<Models.XS2A.NestedSynchronizationFailed>) },
            { "xs2a.account.detailsUpdated", typeof(Payload<Models.XS2A.NestedAccountDetailsUpdated>) },
            { "xs2a.account.transactionsCreated", typeof(Payload<Models.XS2A.NestedAccountTransactionsCreated>) },
            { "xs2a.account.transactionsUpdated", typeof(Payload<Models.XS2A.NestedAccountTransactionsUpdated>) },
            { "xs2a.account.transactionsDeleted", typeof(Payload<Models.XS2A.NestedAccountTransactionsDeleted>) },
            { "xs2a.bulkPaymentInitiationRequest.authorizationCompleted", typeof(Payload<Models.XS2A.NestedBulkPaymentInitiationRequestAuthorizationCompleted>) },
            { "xs2a.bulkPaymentInitiationRequest.statusUpdate", typeof(Payload<Models.XS2A.NestedBulkPaymentInitiationRequestStatusUpdated>) },
            { "xs2a.paymentInitiationRequest.authorizationCompleted", typeof(Payload<Models.XS2A.NestedPaymentInitiationRequestAuthorizationCompleted>) },
            { "xs2a.paymentInitiationRequest.statusUpdate", typeof(Payload<Models.XS2A.NestedPaymentInitiationRequestStatusUpdated>) },
            { "xs2a.periodicPaymentInitiationRequest.authorizationCompleted", typeof(Payload<Models.XS2A.NestedPeriodicPaymentInitiationRequestAuthorizationCompleted>) },
            { "xs2a.periodicPaymentInitiationRequest.statusUpdate", typeof(Payload<Models.XS2A.NestedPeriodicPaymentInitiationRequestStatusUpdated>) }
        };

        private readonly ISerializer<string> _serializer;
        private readonly IVerifier _jwtVerifier;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="serializer">To-string serializer</param>
        /// <param name="jwksService">Get public keys from authorization server</param>
        /// <param name="jwtVerifier">JSON Web Token verifier</param>
        public WebhooksService(ISerializer<string> serializer, IJwksService jwksService, IVerifier jwtVerifier)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            JwksService = jwksService ?? throw new ArgumentNullException(nameof(jwksService));
            _jwtVerifier = jwtVerifier ?? throw new ArgumentNullException(nameof(jwtVerifier));
        }

        /// <inheritdoc />
        public IJwksService JwksService { get; }

        /// <summary>
        /// Get event type.
        /// </summary>
        /// <param name="payload">Webhook payload</param>
        /// <returns></returns>
        public string GetPayloadType(string payload) =>
            _serializer.Deserialize<Payload<PayloadData>>(payload)?.Data?.Type;

        /// <inheritdoc />
        public async Task<IWebhookEvent> VerifyAndDeserialize(string payload, string signature, CancellationToken? cancellationToken)
        {
            await EnsureSignatureAndDigestAreValid(payload, signature, cancellationToken).ConfigureAwait(false);

            var payloadType = GetPayloadType(payload) ?? throw new IbanityException("Can't get event type");
            if (!Types.TryGetValue(payloadType, out var type))
                throw new IbanityException("Can't find event type: " + payloadType);

            var deserializedPayload = (Payload)_serializer.Deserialize(payload, type);
            return deserializedPayload.UntypedData.Flatten();
        }

        private async Task EnsureSignatureAndDigestAreValid(string payload, string signature, CancellationToken? cancellationToken)
        {
            var digest = await VerifyAndGetDigest(signature, cancellationToken).ConfigureAwait(false);

            byte[] computedDigest;
            using (var algorithm = new SHA512Managed())
                computedDigest = algorithm.ComputeHash(Encoding.UTF8.GetBytes(payload));

            var digestBytes = Convert.FromBase64String(digest);

            if (!digestBytes.SequenceEqual(computedDigest))
                throw new InvalidSignatureException("Digest mismatch");
        }

        private async Task<string> VerifyAndGetDigest(string signature, CancellationToken? cancellationToken)
        {
            var token = await _jwtVerifier.Verify(signature, cancellationToken).ConfigureAwait(false);
            return token.Payload.Digest;
        }
    }

    /// <summary>
    /// Allows to verify and deserialize webhook payloads.
    /// </summary>
    public interface IWebhooksService
    {
        /// <summary>
        /// Verify JWT signature and deserialize payload.
        /// </summary>
        /// <param name="payload">Webhook payload</param>
        /// <param name="signature">Signature header content (JWT token)</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The event payload</returns>
        Task<IWebhookEvent> VerifyAndDeserialize(string payload, string signature, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get public key from JSON Web Key Set endpoint.
        /// </summary>
        IJwksService JwksService { get; }
    }
}
