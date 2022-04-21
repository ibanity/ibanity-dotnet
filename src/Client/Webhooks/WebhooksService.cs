using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Utils;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;

namespace Ibanity.Apis.Client.Webhooks
{
    /// <inheritdoc />
    public class WebhooksService : IWebhooksService
    {
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

        /// <inheritdoc />
        public string GetPayloadType(string payload) =>
            _serializer.Deserialize<Resource<object, object, object, object>>(payload)?.Data?.Type;

        /// <inheritdoc />
        public T ValidateAndDeserialize<T>(string payload, string signature)
        {
            EnsureSignatureAndDigestAreValid(payload, signature);

            return _serializer.Deserialize<T>(payload);
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
        /// Get event type.
        /// </summary>
        /// <param name="payload">Webhook payload</param>
        /// <returns></returns>
        string GetPayloadType(string payload);

        /// <summary>
        /// Validate JWT signature and deserialize payload.
        /// </summary>
        /// <typeparam name="T">Payload type</typeparam>
        /// <param name="payload">Webhook payload</param>
        /// <param name="signature">Signature header content (JWT token)</param>
        /// <returns>The event payload</returns>
        T ValidateAndDeserialize<T>(string payload, string signature);
    }
}
