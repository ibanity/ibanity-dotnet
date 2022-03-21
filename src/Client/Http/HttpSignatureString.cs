using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ibanity.Apis.Client.Http
{
    public class HttpSignatureString : IHttpSignatureString
    {
        private static readonly Regex HeaderPattern = new Regex(
            "(authorization|ibanity.*?)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private readonly Uri _ibanityEndpoint;

        public HttpSignatureString(Uri ibanityEndpoint) =>
            _ibanityEndpoint = ibanityEndpoint;

        public string Compute(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string digest, DateTimeOffset timestamp) => string.Join(
            "\n",
            new[]
            {
                $"(request-target): {httpMethod.ToLower()} {url.PathAndQuery}",
                "host: " + _ibanityEndpoint.Host,
                "digest: " + digest,
                "(created): " + timestamp.ToUnixTimeSeconds()
            }.Union(requestHeaders.
                Where(kvp => HeaderPattern.IsMatch(kvp.Key)).
                Select(kvp => $"{kvp.Key.ToLower()}: {kvp.Value}")));

        public IEnumerable<string> GetSignedHeaders(IDictionary<string, string> requestHeaders)
        {
            yield return "(request-target)";
            yield return "host";
            yield return "digest";
            yield return "(created)";

            foreach (var key in requestHeaders.Keys)
                if (HeaderPattern.IsMatch(key))
                    yield return key.ToLower();
        }
    }

    public interface IHttpSignatureString
    {
        string Compute(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string digest, DateTimeOffset timestamp);
        IEnumerable<string> GetSignedHeaders(IDictionary<string, string> requestHeaders);
    }
}
