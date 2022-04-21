using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Webhooks.Models;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;

namespace Ibanity.Apis.Client.Webhooks
{
    /// <inheritdoc />
    public class WebhooksService : IWebhooksService
    {
        private static readonly IReadOnlyDictionary<string, Type> Types = new Dictionary<string, Type>
        {
            { "pontoConnect.synchronization.succeededWithoutChange", typeof(Payload<SynchronizationSucceededWithoutChange>) },
            { "pontoConnect.synchronization.failed", typeof(Payload<SynchronizationFailed>) },
            { "pontoConnect.account.detailsUpdated", typeof(Payload<AccountDetailsUpdated>) },
            { "pontoConnect.account.transactionsCreated", typeof(Payload<AccountTransactionsCreated>) },
            { "pontoConnect.account.transactionsUpdated", typeof(Payload<AccountTransactionsUpdated>) }
        };

        private readonly ISerializer<string> _serializer;
        private readonly X509Certificate2 _certificate;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="serializer">To-string serializer</param>
        /// <param name="certificate">CA certificate</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public WebhooksService(ISerializer<string> serializer, X509Certificate2 certificate)
        {
            _serializer = serializer ?? throw new System.ArgumentNullException(nameof(serializer));
            _certificate = certificate;
        }

        /// <summary>
        /// Get event type.
        /// </summary>
        /// <param name="payload">Webhook payload</param>
        /// <returns></returns>
        public string GetPayloadType(string payload) =>
            _serializer.Deserialize<Payload<PayloadData>>(payload)?.Data?.Type;

        /// <inheritdoc />
        public IWebhookEvent ValidateAndDeserialize(string payload, string signature)
        {
            EnsureSignatureAndDigestAreValid(payload, signature);

            var payloadType = GetPayloadType(payload) ?? throw new IbanityException("Can't get event type");
            if (!Types.TryGetValue(payloadType, out var type))
                throw new IbanityException("Can't find event type: " + payloadType);

            var t = _serializer.Deserialize(payload, type) as Payload;
            return t.UntypedData;
        }

        private void EnsureSignatureAndDigestAreValid(string payload, string signature)
        {
            string digest;
            try
            {
                var token = new JwtBuilder().
                    WithAlgorithm(new RS256Algorithm(_certificate)).
                    //MustVerifySignature().
                    Decode<IDictionary<string, object>>(signature);

                digest = (string)token["digest"];
            }
            catch (SignatureVerificationException exception)
            {
                throw new InvalidSignatureException(exception);
            }

            byte[] computedDigest;
            using (var algorithm = new SHA512Managed())
                computedDigest = algorithm.ComputeHash(Encoding.UTF8.GetBytes(payload));

            var digestBytes = Convert.FromBase64String(digest);

            if (!digestBytes.SequenceEqual(computedDigest))
                throw new InvalidSignatureException("Digest mismatch");
        }
    }

    /// <summary>
    /// Allows to validate and deserialize webhook payloads.
    /// </summary>
    public interface IWebhooksService
    {
        /// <summary>
        /// Validate JWT signature and deserialize payload.
        /// </summary>
        /// <param name="payload">Webhook payload</param>
        /// <param name="signature">Signature header content (JWT token)</param>
        /// <returns>The event payload</returns>
        IWebhookEvent ValidateAndDeserialize(string payload, string signature);
    }
}
