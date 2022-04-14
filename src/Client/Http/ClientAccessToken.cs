using System;

namespace Ibanity.Apis.Client.Http
{
    /// <summary>
    /// Client access token, generated from a client ID and client secret.
    /// </summary>
    public class ClientAccessToken
    {
        internal ClientAccessToken(string accessToken, DateTimeOffset validUntil)
        {
            AccessToken = accessToken;
            ValidUntil = validUntil;
        }

        /// <summary>
        /// Bearer token.
        /// </summary>
        public string AccessToken { get; internal set; }

        /// <summary>
        /// Validity limit.
        /// </summary>
        public DateTimeOffset ValidUntil { get; internal set; }
    }
}
