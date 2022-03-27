using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Http
{
    public class OAuth2ClientAccessTokenProvider : IAccessTokenProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IClock _clock;
        private readonly string _urlPrefix;
        private readonly string _clientId;
        private readonly string _clientSecret;

        private string _accessToken;
        private DateTimeOffset _validUntil = DateTimeOffset.MinValue;

        public OAuth2ClientAccessTokenProvider(HttpClient httpClient, IClock clock, string urlPrefix, string clientId, string clientSecret)
        {
            _httpClient = httpClient;
            _clock = clock;
            _urlPrefix = urlPrefix;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public Task<Token> RefreshToken(Token token, CancellationToken? cancellationToken)
        {
            return Task.FromResult(token);
        }
    }
}
