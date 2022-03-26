using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Http
{
    public class OAuth2ClientAccessTokenProvider : IBearerTokenProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IClock _clock;

        private readonly string _clientId;
        private readonly string _clientSecret;

        private string _accessToken;
        private DateTimeOffset _validUntil = DateTimeOffset.MinValue;

        public OAuth2ClientAccessTokenProvider(HttpClient httpClient, IClock clock, string clientId, string clientSecret)
        {
            _httpClient = httpClient;
            _clock = clock;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public Task<string> GetBearerToken(CancellationToken cancellationToken)
        {
            return Task.FromResult(string.Empty);
        }
    }
}
