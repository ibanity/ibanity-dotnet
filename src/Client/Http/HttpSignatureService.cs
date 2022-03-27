using System;
using System.Collections.Generic;
using Ibanity.Apis.Client.Crypto;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Http
{
    public class HttpSignatureService : IHttpSignatureService
    {
        private const string SignatureHeaderAlgorithm = "hs2019";

        private readonly IDigest _digest;
        private readonly ISignature _signature;
        private readonly IClock _clock;
        private readonly IHttpSignatureString _signatureString;
        private readonly string _certificateId;

        public HttpSignatureService(IDigest digest, ISignature signature, IClock clock, IHttpSignatureString signatureString, string certificateId)
        {
            if (string.IsNullOrEmpty(certificateId))
                throw new ArgumentException($"'{nameof(certificateId)}' cannot be null or empty.", nameof(certificateId));

            _digest = digest ?? throw new ArgumentNullException(nameof(digest));
            _signature = signature ?? throw new ArgumentNullException(nameof(signature));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
            _signatureString = signatureString ?? throw new ArgumentNullException(nameof(signatureString));
            _certificateId = certificateId;
        }

        public IDictionary<string, string> GetHttpSignatureHeaders(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string payload)
        {
            if (string.IsNullOrEmpty(httpMethod))
                throw new ArgumentException($"'{nameof(httpMethod)}' cannot be null or empty.", nameof(httpMethod));

            if (url is null)
                throw new ArgumentNullException(nameof(url));

            if (requestHeaders is null)
                throw new ArgumentNullException(nameof(requestHeaders));

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

    public class NullHttpSignatureService : IHttpSignatureService
    {
        public static readonly IHttpSignatureService Instance = new NullHttpSignatureService();

        private NullHttpSignatureService() { }

        public IDictionary<string, string> GetHttpSignatureHeaders(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string payload) =>
            new Dictionary<string, string>();
    }

    public interface IHttpSignatureService
    {
        IDictionary<string, string> GetHttpSignatureHeaders(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string payload);
    }
}
