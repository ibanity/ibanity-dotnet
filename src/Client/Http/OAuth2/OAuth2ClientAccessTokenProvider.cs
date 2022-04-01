using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Client.Http.OAuth2
{
    public class OAuth2ClientAccessTokenProvider : IClientAccessTokenProvider
    {
        private static readonly TimeSpan ValidityThreshold = TimeSpan.FromMinutes(1d);

        private ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly IClock _clock;
        private readonly ISerializer<string> _serializer;
        private readonly string _urlPrefix;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public OAuth2ClientAccessTokenProvider(ILoggerFactory loggerFactory, HttpClient httpClient, IClock clock, ISerializer<string> serializer, string urlPrefix, string clientId, string clientSecret)
        {
            if (loggerFactory is null)
                throw new ArgumentNullException(nameof(loggerFactory));

            if (string.IsNullOrWhiteSpace(urlPrefix))
                throw new ArgumentException($"'{nameof(urlPrefix)}' cannot be null or empty.", nameof(urlPrefix));

            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentException($"'{nameof(clientId)}' cannot be null or empty.", nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentException($"'{nameof(clientSecret)}' cannot be null or empty.", nameof(clientSecret));

            _logger = loggerFactory.CreateLogger<OAuth2ClientAccessTokenProvider>();
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
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
            if (token is null)
                throw new ArgumentNullException(nameof(token));

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

            _logger.Debug("Getting new token from client credentials");

            var result = await (await _httpClient.SendAsync(request, cancellationToken ?? CancellationToken.None)).ThrowOnOAuth2Failure(_serializer);
            var response = _serializer.Deserialize<OAuth2Response>(await result.Content.ReadAsStringAsync());

            token.AccessToken = response.AccessToken;
            token.ValidUntil = _clock.Now + response.ExpiresIn;

            return token;
        }
    }
}