using System;
using System.Text;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Webhooks.Jwt
{
    /// <inheritdoc />
    public class Parser : IParser
    {
        private readonly ISerializer<string> _serializer;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="serializer">To-string serializer</param>
        public Parser(ISerializer<string> serializer) =>
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));

        /// <inheritdoc />
        public Header GetHeader(string token)
        {
            var (header, _, _) = GetParts(token);
            return _serializer.Deserialize<Header>(GetStringFromBase64(header));
        }

        /// <inheritdoc />
        public T GetPayload<T>(string token) where T : Payload
        {
            var (_, payload, _) = GetParts(token);
            return _serializer.Deserialize<T>(GetStringFromBase64(payload));
        }

        /// <inheritdoc />
        public byte[] GetSignature(string token)
        {
            var (_, _, signature) = GetParts(token);
            return GetBytesFromBase64(signature);
        }

        /// <inheritdoc />
        public string RemoveSignature(string token)
        {
            var (header, payload, _) = GetParts(token);
            return $"{header}.{payload}";
        }

        private static (string, string, string) GetParts(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException($"'{nameof(token)}' cannot be null or whitespace.", nameof(token));

            var parts = token.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 3)
                throw new InvalidSignatureException("Expected 3 parts in token but got " + parts.Length);

            return (parts[0], parts[1], parts[2]);
        }

        private static string GetStringFromBase64(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                throw new ArgumentException($"'{nameof(base64)}' cannot be null or whitespace.", nameof(base64));

            return Encoding.UTF8.GetString(GetBytesFromBase64(base64));
        }

        private static byte[] GetBytesFromBase64(string base64) =>
            Convert.FromBase64String(
                (base64.Length % 4 == 0 ? base64 : base64 + "====".Substring(base64.Length % 4)).Replace('-', '+').Replace('_', '/'));
    }

    /// <summary>
    /// Parse Json Web Tokens.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Extract header part.
        /// </summary>
        /// <param name="token">Json Web Token</param>
        /// <returns>Header object</returns>
        Header GetHeader(string token);

        /// <summary>
        /// Extract payload part.
        /// </summary>
        /// <typeparam name="T">Payload type</typeparam>
        /// <param name="token">Json Web Token</param>
        /// <returns>Payload object</returns>
        T GetPayload<T>(string token) where T : Payload;

        /// <summary>
        /// Extract signature part.
        /// </summary>
        /// <param name="token">Json Web Token</param>
        /// <returns>Signature bytes</returns>
        byte[] GetSignature(string token);

        /// <summary>
        /// Extract header and payload parts.
        /// </summary>
        /// <param name="token">Json Web Token</param>
        /// <returns>Token without the signature part</returns>
        string RemoveSignature(string token);
    }
}
