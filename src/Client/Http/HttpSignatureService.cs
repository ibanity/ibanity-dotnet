using System;
using System.Collections.Generic;

namespace Ibanity.Apis.Client.Http
{
    public class HttpSignatureService : IHttpSignatureService
    {
        public IDictionary<string, string> GetHttpSignatureHeaders(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string? payload)
        {
            return new Dictionary<string, string>
            {
                { "Digest", "" },
                { "Signature", "" }
            };
        }
    }

    public interface IHttpSignatureService
    {
        IDictionary<string, string> GetHttpSignatureHeaders(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string? payload);
    }
}
