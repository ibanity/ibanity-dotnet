using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ibanity.Apis.Client.Http
{
    /// <inheritdoc />
    public class HttpSignatureString : IHttpSignatureString
    {
        private static readonly Regex HeaderPattern = new Regex(
            "(authorization|ibanity.*?)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private readonly Uri _ibanityEndpoint;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="ibanityEndpoint">Base Ibanity endpoint</param>
        public HttpSignatureString(Uri ibanityEndpoint) =>
            _ibanityEndpoint = ibanityEndpoint;

        /// <inheritdoc />
        public string Compute(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string digest, DateTimeOffset timestamp)
        {
            if (string.IsNullOrWhiteSpace(httpMethod))
                throw new ArgumentException($"'{nameof(httpMethod)}' cannot be null or whitespace.", nameof(httpMethod));

            if (url is null)
                throw new ArgumentNullException(nameof(url));

            if (requestHeaders is null)
                throw new ArgumentNullException(nameof(requestHeaders));

            if (string.IsNullOrWhiteSpace(digest))
                throw new ArgumentException($"'{nameof(digest)}' cannot be null or whitespace.", nameof(digest));

            return string.Join(
                "\n",
                new[]
                {
                    $"(request-target): {httpMethod.ToLower(CultureInfo.InvariantCulture)} {url.PathAndQuery}",
                    "host: " + _ibanityEndpoint.Host,
                    "digest: " + digest,
                    "(created): " + timestamp.ToUnixTimeSeconds()
                }.Union(requestHeaders.
                    Where(kvp => HeaderPattern.IsMatch(kvp.Key)).
                    Select(kvp => $"{kvp.Key.ToLower(CultureInfo.InvariantCulture)}: {kvp.Value}")));
        }

        /// <inheritdoc />
        public IEnumerable<string> GetSignedHeaders(IDictionary<string, string> requestHeaders)
        {
            yield return "(request-target)";
            yield return "host";
            yield return "digest";
            yield return "(created)";

            foreach (var key in requestHeaders.Keys)
                if (HeaderPattern.IsMatch(key))
                    yield return key.ToLower(CultureInfo.InvariantCulture);
        }
    }

    /// <summary>
    /// Allow to build, from a request, the string to be signed
    /// </summary>
    public interface IHttpSignatureString
    {
        /// <summary>
        /// Build, from a request, the string to be signed
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="url"></param>
        /// <param name="requestHeaders">Existing request headers</param>
        /// <param name="digest">Hash of the payload</param>
        /// <param name="timestamp">Now</param>
        /// <returns>String to be signed</returns>
        string Compute(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string digest, DateTimeOffset timestamp);

        /// <summary>
        /// Get the list of headers to be signed.
        /// </summary>
        /// <param name="requestHeaders">Existing request headers</param>
        /// <returns>A list of header names</returns>
        IEnumerable<string> GetSignedHeaders(IDictionary<string, string> requestHeaders);
    }
}
