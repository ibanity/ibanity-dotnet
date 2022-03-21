using System;
using System.Collections.Generic;
using Ibanity.Apis.Client.Crypto;

namespace Ibanity.Apis.Client.Http
{
    public class HttpSignatureService : IHttpSignatureService
    {
        private readonly IDigest _digest;

        public HttpSignatureService(IDigest digest)
        {
            _digest = digest;
        }

        public IDictionary<string, string> GetHttpSignatureHeaders(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string? payload)
        {
            var digest = _digest.Compute(payload ?? string.Empty);

            return new Dictionary<string, string>
            {
                { "Digest", digest },
                { "Signature", "" }
            };
        }
    }

    public interface IHttpSignatureService
    {
        IDictionary<string, string> GetHttpSignatureHeaders(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string? payload);
    }
}
