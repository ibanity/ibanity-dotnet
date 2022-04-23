using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Webhooks.Models;

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
        private readonly IJwksService _jwksService;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="serializer">To-string serializer</param>
        /// <param name="jwksService">JSON Web Key Set client</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public WebhooksService(ISerializer<string> serializer, IJwksService jwksService)
        {
            _serializer = serializer ?? throw new System.ArgumentNullException(nameof(serializer));
            _jwksService = jwksService ?? throw new ArgumentNullException(nameof(jwksService));
        }

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
            await EnsureSignatureAndDigestAreValid(payload, signature, cancellationToken);

            var payloadType = GetPayloadType(payload) ?? throw new IbanityException("Can't get event type");
            if (!Types.TryGetValue(payloadType, out var type))
                throw new IbanityException("Can't find event type: " + payloadType);

            var deserializedPayload = _serializer.Deserialize(payload, type) as Payload;
            return deserializedPayload.UntypedData;
        }

        private async Task EnsureSignatureAndDigestAreValid(string payload, string signature, CancellationToken? cancellationToken)
        {
            var digest = await VerifyAndGetDigest(signature, cancellationToken);

            byte[] computedDigest;
            using (var algorithm = new SHA512Managed())
                computedDigest = algorithm.ComputeHash(Encoding.UTF8.GetBytes(payload));

            var digestBytes = Convert.FromBase64String(digest);

            if (!digestBytes.SequenceEqual(computedDigest))
                throw new InvalidSignatureException("Digest mismatch");
        }

        private async Task<string> VerifyAndGetDigest(string signature, CancellationToken? cancellationToken)
        {
            var keys = await _jwksService.GetKeys(cancellationToken);

            for (var i = 0; i < keys.Length; i++)
                try
                {
                    // we should run in two passes to get the proper key
                    var token = new JWT.Builder.JwtBuilder().
                        WithAlgorithm(new JWT.Algorithms.RS256Algorithm(keys[i])).
                        //MustVerifySignature(). // I can't get it working :'(
                        WithValidator(new JWT.JwtValidator(new JWT.Serializers.JsonNetSerializer(), new JWT.UtcDateTimeProvider(), 3600)).
                        Decode<IDictionary<string, object>>(signature);

                    return (string)token["digest"];
                }
                catch (JWT.Exceptions.SignatureVerificationException exception)
                {
                    if (i == keys.Length - 1)
                        throw new InvalidSignatureException(exception);
                }

            throw new InvalidSignatureException("Can't get any key from JWKS authorization server");
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
    }
}
