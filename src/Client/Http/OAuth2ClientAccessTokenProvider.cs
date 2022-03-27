using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Http
{
    public class OAuth2ClientAccessTokenProvider : IClientAccessTokenProvider
    {
        private static readonly TimeSpan ValidityThreshold = TimeSpan.FromMinutes(1d);

        private readonly HttpClient _httpClient;
        private readonly IClock _clock;
        private readonly ISerializer<string> _serializer;
        private readonly string _urlPrefix;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public OAuth2ClientAccessTokenProvider(HttpClient httpClient, IClock clock, ISerializer<string> serializer, string urlPrefix, string clientId, string clientSecret)
        {
            _httpClient = httpClient;
            _clock = clock;
            _serializer = serializer;
            _urlPrefix = urlPrefix;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task<ClientAccessToken> GetToken(CancellationToken? cancellationToken)
        {
            var token = new ClientAccessToken(
                null,
                DateTimeOffset.MinValue);

            return await RefreshToken(token, cancellationToken);
        }

        public async Task<ClientAccessToken> RefreshToken(ClientAccessToken token, CancellationToken? cancellationToken)
        {
            if (token.ValidUntil - _clock.Now >= ValidityThreshold)
                return token;

            var payload = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_urlPrefix}/oauth2/token")
            {
                Content = new FormUrlEncodedContent(payload)
            };

            request.Headers.Authorization = new BasicAuthenticationHeaderValue(_clientId, _clientSecret);

            var result = (await _httpClient.SendAsync(request, cancellationToken ?? CancellationToken.None)).EnsureSuccessStatusCode();
            var response = _serializer.Deserialize<OAuth2Response>(await result.Content.ReadAsStringAsync());

            return new ClientAccessToken(
                response.AccessToken,
                _clock.Now + response.ExpiresIn);
        }
    }
}
