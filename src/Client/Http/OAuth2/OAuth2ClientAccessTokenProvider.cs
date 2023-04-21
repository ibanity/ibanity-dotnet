using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Client.Http.OAuth2
{
    /// <inheritdoc />
    public class OAuth2ClientAccessTokenProvider : IClientAccessTokenProvider
    {
        private const string RequestIdHeader = "ibanity-request-id";
        private static readonly TimeSpan ValidityThreshold = TimeSpan.FromMinutes(1d);

        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly IClock _clock;
        private readonly ISerializer<string> _serializer;
        private readonly string _urlPrefix;
        private readonly string _clientId;
        private readonly string _clientSecret;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="loggerFactory">Allow to build the logger used within this instance</param>
        /// <param name="httpClient">Low-level HTTP client</param>
        /// <param name="clock">Returns current date and time</param>
        /// <param name="serializer">To-string serializer</param>
        /// <param name="urlPrefix">Product endpoint</param>
        /// <param name="clientId">OAuth2 client ID</param>
        /// <param name="clientSecret">OAuth2 client secret</param>
        public OAuth2ClientAccessTokenProvider(ILoggerFactory loggerFactory, HttpClient httpClient, IClock clock, ISerializer<string> serializer, string urlPrefix, string clientId, string clientSecret)
        {
            if (loggerFactory is null)
                throw new ArgumentNullException(nameof(loggerFactory));

            if (string.IsNullOrWhiteSpace(urlPrefix))
                throw new ArgumentException($"'{nameof(urlPrefix)}' cannot be null or whitespace.", nameof(urlPrefix));

            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentException($"'{nameof(clientId)}' cannot be null or whitespace.", nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentException($"'{nameof(clientSecret)}' cannot be null or whitespace.", nameof(clientSecret));

            _logger = loggerFactory.CreateLogger<OAuth2ClientAccessTokenProvider>();
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _urlPrefix = urlPrefix;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        /// <inheritdoc />
        public async Task<ClientAccessToken> GetToken(CancellationToken? cancellationToken)
        {
            var token = new ClientAccessToken(
                null,
                DateTimeOffset.MinValue);

            return await RefreshToken(token, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
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

            var result = await (await _httpClient.SendAsync(request, cancellationToken ?? CancellationToken.None).ConfigureAwait(false)).ThrowOnOAuth2Failure(_serializer, _logger).ConfigureAwait(false);

            var requestId = result.Headers.TryGetValues(RequestIdHeader, out var values) ? values.SingleOrDefault() : null;
            _logger.Debug($"Response received ({result.StatusCode:D} {result.StatusCode:G}): {request.Method.ToString().ToUpper(CultureInfo.InvariantCulture)} {request.RequestUri.AbsolutePath} (request ID: {requestId})");

            var response = _serializer.Deserialize<OAuth2Response>(await result.Content.ReadAsStringAsync().ConfigureAwait(false));

            token.AccessToken = response.AccessToken;
            token.ValidUntil = _clock.Now + response.ExpiresIn;

            return token;
        }
    }
}
