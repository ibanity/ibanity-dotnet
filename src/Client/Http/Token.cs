using System;

namespace Ibanity.Apis.Client.Http
{
    public class Token
    {
        internal Token(string accessToken, DateTimeOffset validUntil, string refreshToken)
        {
            AccessToken = accessToken;
            ValidUntil = validUntil;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; }
        public DateTimeOffset ValidUntil { get; }
        public string RefreshToken { get; }
    }
}
