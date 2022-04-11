using System;
using System.Collections.Generic;
using Ibanity.Apis.Client.Crypto;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Client.Http
{
    /// <inheritdoc />
    public class HttpSignatureService : IHttpSignatureService
    {
        private const string SignatureHeaderAlgorithm = "hs2019";

        private readonly ILogger _logger;
        private readonly IDigest _digest;
        private readonly ISignature _signature;
        private readonly IClock _clock;
        private readonly IHttpSignatureString _signatureString;
        private readonly string _certificateId;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="loggerFactory">Allow to build the logger used within this instance</param>
        /// <param name="digest">Used to compute signature string hash</param>
        /// <param name="signature">Signature algorithm</param>
        /// <param name="clock">Returns current date and time</param>
        /// <param name="signatureString">Used to build signature string from payload</param>
        /// <param name="certificateId">Identifier for the application's signature certificate, obtained from the Developer Portal</param>
        public HttpSignatureService(ILoggerFactory loggerFactory, IDigest digest, ISignature signature, IClock clock, IHttpSignatureString signatureString, string certificateId)
        {
            if (loggerFactory is null)
                throw new ArgumentNullException(nameof(loggerFactory));

            if (string.IsNullOrWhiteSpace(certificateId))
                throw new ArgumentException($"'{nameof(certificateId)}' cannot be null or whitespace.", nameof(certificateId));

            _logger = loggerFactory.CreateLogger<HttpSignatureService>();
            _digest = digest ?? throw new ArgumentNullException(nameof(digest));
            _signature = signature ?? throw new ArgumentNullException(nameof(signature));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
            _signatureString = signatureString ?? throw new ArgumentNullException(nameof(signatureString));
            _certificateId = certificateId;
        }

        /// <inheritdoc />
        public IDictionary<string, string> GetHttpSignatureHeaders(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string payload)
        {
            if (string.IsNullOrWhiteSpace(httpMethod))
                throw new ArgumentException($"'{nameof(httpMethod)}' cannot be null or whitespace.", nameof(httpMethod));

            if (url is null)
                throw new ArgumentNullException(nameof(url));

            if (requestHeaders is null)
                throw new ArgumentNullException(nameof(requestHeaders));

            _logger.Trace("Signing request with certificate " + _certificateId);

            var now = _clock.Now;
            var digest = _digest.Compute(payload ?? string.Empty);

            var signatureString = _signatureString.Compute(
                httpMethod,
                url,
                requestHeaders,
                digest,
                now);

            var signature = _signature.Sign(signatureString);

            var signedHeaders = _signatureString.GetSignedHeaders(requestHeaders);

            var signatureHeaderFields = new[]
            {
                $@"keyId=""{_certificateId}""",
                $@"created={now.ToUnixTimeSeconds()}",
                $@"algorithm=""{SignatureHeaderAlgorithm}""",
                $@"headers=""{string.Join(" ", signedHeaders)}""",
                $@"signature=""{signature}"""
            };

            return new Dictionary<string, string>
            {
                { "Digest", digest },
                { "Signature", string.Join(",", signatureHeaderFields) }
            };
        }
    }

    /// <summary>
    /// Null signature service, used when the signature certificate isn't configured.
    /// </summary>
    public class NullHttpSignatureService : IHttpSignatureService
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static readonly IHttpSignatureService Instance = new NullHttpSignatureService();

        private NullHttpSignatureService() { }

        /// <inheritdoc />
        /// <remarks>Returns an empty dictionary.</remarks>
        public IDictionary<string, string> GetHttpSignatureHeaders(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string payload) =>
            new Dictionary<string, string>();
    }

    /// <summary>
    /// Allow to compute the signature headers for a given request.
    /// </summary>
    public interface IHttpSignatureService
    {
        /// <summary>
        /// Compute the signature headers.
        /// </summary>
        /// <param name="httpMethod">HTTP method (GET, POST, ...)</param>
        /// <param name="url">URI where to request is going to be sent</param>
        /// <param name="requestHeaders">Existing request headers</param>
        /// <param name="payload">Request payload</param>
        /// <returns>Headers names and values</returns>
        IDictionary<string, string> GetHttpSignatureHeaders(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string payload);
    }
}
