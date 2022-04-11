using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Http.OAuth2
{
    /// <summary>
    /// Payload received on OAuth2 operations.
    /// </summary>
    [DataContract]
    public class OAuth2Response
    {
        /// <summary>
        /// Access token.
        /// </summary>
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Duration in seconds before the access token expires.
        /// </summary>
        [DataMember(Name = "expires_in")]
        public int ExpiresInSeconds { get; set; }

        /// <summary>
        /// Duration before the access token expires.
        /// </summary>
        public TimeSpan ExpiresIn => TimeSpan.FromSeconds(ExpiresInSeconds);

        /// <summary>
        /// Refresh token.
        /// </summary>
        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Scope.
        /// </summary>
        [DataMember(Name = "scope")]
        public string Scope { get; set; }

        /// <summary>
        /// Token type (always bearer).
        /// </summary>
        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }
    }
}
