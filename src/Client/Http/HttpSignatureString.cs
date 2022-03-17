using System;
using System.Collections.Generic;

namespace Ibanity.Apis.Client.Http
{
    public class HttpSignatureString : IHttpSignatureString
    {
        public string Compute(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string digest, DateTimeOffset timestamp)
        {
            return string.Empty;
        }
    }

    public interface IHttpSignatureString
    {
        string Compute(string httpMethod, Uri url, IDictionary<string, string> requestHeaders, string digest, DateTimeOffset timestamp);
    }
}
