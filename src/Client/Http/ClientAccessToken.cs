using System;

namespace Ibanity.Apis.Client.Http
{
    /// <summary>
    /// Client access token, generated from a client ID and client secret.
    /// </summary>
    public class ClientAccessToken
    {
        /// <summary>
        /// Build a new instance.
        /// </summary>
        public ClientAccessToken() { }

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="accessToken">Bearer token</param>
        /// <param name="validUntil">Validity limit</param>
        public ClientAccessToken(string accessToken, DateTimeOffset validUntil)
        {
            AccessToken = accessToken;
            ValidUntil = validUntil;
        }

        /// <summary>
        /// Bearer token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Validity limit.
        /// </summary>
        public DateTimeOffset ValidUntil { get; set; }
    }
}
