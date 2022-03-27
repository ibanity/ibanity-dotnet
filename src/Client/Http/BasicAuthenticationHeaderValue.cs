using System;
using System.Net.Http.Headers;
using System.Text;

namespace Ibanity.Apis.Client.Http
{
    public class BasicAuthenticationHeaderValue : AuthenticationHeaderValue
    {
        private static readonly Encoding Encoding = Encoding.ASCII;

        public BasicAuthenticationHeaderValue(string username, string password)
            : base("Basic", Convert.ToBase64String(Encoding.GetBytes($"{username}:{password}")))
        { }
    }
}
