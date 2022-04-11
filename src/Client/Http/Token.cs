using System;

namespace Ibanity.Apis.Client.Http
{
    /// <summary>
    /// Access token, generated from an authorization code or a refresh token.
    /// </summary>
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

        /// <summary>
        /// Bearer token.
        /// </summary>
        public string AccessToken { get; internal set; }

        /// <summary>
        /// Validaty limit.
        /// </summary>
        public DateTimeOffset ValidUntil { get; internal set; }

        /// <summary>
        /// Token used to get a new bearer token.
        /// </summary>
        /// <remarks>Don't forget to save this value if you want to reuse the token later.</remarks>
        public string RefreshToken { get; internal set; }
    }
}
