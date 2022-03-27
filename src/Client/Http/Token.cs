using System;

namespace Ibanity.Apis.Client.Http
{
    public class Token
    {
        internal Token(string accessToken, DateTimeOffset validUntil, string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentException($"'{nameof(refreshToken)}' cannot be null or whitespace.", nameof(refreshToken));

            AccessToken = accessToken;
            ValidUntil = validUntil;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; }
        public DateTimeOffset ValidUntil { get; }
        public string RefreshToken { get; }
    }
}
