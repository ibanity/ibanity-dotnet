using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Http.OAuth2
{
    [DataContract]
    public class OAuth2Response
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "expires_in")]
        public int ExpiresInSeconds { get; set; }

        public TimeSpan ExpiresIn => TimeSpan.FromSeconds(ExpiresInSeconds);

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        [DataMember(Name = "scope")]
        public string Scope { get; set; }

        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }
    }
}
