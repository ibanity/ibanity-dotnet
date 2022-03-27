using System;

namespace Ibanity.Apis.Client.Http
{
    public class ClientAccessToken
    {
        internal ClientAccessToken(string accessToken, DateTimeOffset validUntil)
        {
            AccessToken = accessToken;
            ValidUntil = validUntil;
        }

        public string AccessToken { get; }
        public DateTimeOffset ValidUntil { get; }
    }
}
