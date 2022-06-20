using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Client.Http.OAuth2
{
    /// <inheritdoc />
    public class OAuth2TokenProvider : ITokenProviderWithCodeVerifier, ITokenProviderWithoutCodeVerifier
    {
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
        public OAuth2TokenProvider(ILoggerFactory loggerFactory, HttpClient httpClient, IClock clock, ISerializer<string> serializer, string urlPrefix, string clientId, string clientSecret)
        {
            if (loggerFactory is null)
                throw new ArgumentNullException(nameof(loggerFactory));

            if (string.IsNullOrWhiteSpace(urlPrefix))
                throw new ArgumentException($"'{nameof(urlPrefix)}' cannot be null or whitespace.", nameof(urlPrefix));

            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentException($"'{nameof(clientId)}' cannot be null or whitespace.", nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentException($"'{nameof(clientSecret)}' cannot be null or whitespace.", nameof(clientSecret));

            _logger = loggerFactory.CreateLogger<OAuth2TokenProvider>();
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _urlPrefix = urlPrefix;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        /// <inheritdoc />
        public async Task<Token> GetToken(string authorizationCode, string codeVerifier, Uri redirectUri, CancellationToken? cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(authorizationCode))
                throw new ArgumentException($"'{nameof(authorizationCode)}' cannot be null or whitespace.", nameof(authorizationCode));

            if (redirectUri is null)
                throw new ArgumentNullException(nameof(redirectUri));

            var payload = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", authorizationCode },
                { "client_id", _clientId },
                { "redirect_uri", redirectUri.OriginalString }
            };

            if (!string.IsNullOrWhiteSpace(codeVerifier))
                payload.Add("code_verifier", codeVerifier);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_urlPrefix}/oauth2/token")
            {
                Content = new FormUrlEncodedContent(payload)
            };

            request.Headers.Authorization = new BasicAuthenticationHeaderValue(_clientId, _clientSecret);

            _logger.Debug("Getting new token from authorization code");

            var result = await (await _httpClient.SendAsync(request, cancellationToken ?? CancellationToken.None)).ThrowOnOAuth2Failure(_serializer, _logger);
            var response = _serializer.Deserialize<OAuth2Response>(await result.Content.ReadAsStringAsync());

            return new Token(
                response.AccessToken,
                _clock.Now + response.ExpiresIn,
                response.RefreshToken);
        }

        /// <inheritdoc />
        public Task<Token> GetToken(string authorizationCode, string codeVerifier, string redirectUri, CancellationToken? cancellationToken) =>
            GetToken(
                authorizationCode,
                codeVerifier,
                new Uri(redirectUri ?? throw new ArgumentException($"'{nameof(redirectUri)}' cannot be null or whitespace.", nameof(redirectUri))),
                cancellationToken);

        /// <inheritdoc />
        public Task<Token> GetToken(string authorizationCode, Uri redirectUri, CancellationToken? cancellationToken) =>
            GetToken(authorizationCode, null, redirectUri, cancellationToken);

        /// <inheritdoc />
        public Task<Token> GetToken(string authorizationCode, string redirectUri, CancellationToken? cancellationToken) =>
            GetToken(authorizationCode, null, redirectUri, cancellationToken);

        /// <inheritdoc />
        public async Task<Token> GetToken(string refreshToken, CancellationToken? cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentException($"'{nameof(refreshToken)}' cannot be null or whitespace.", nameof(refreshToken));

            var token = new Token(
                null,
                DateTimeOffset.MinValue,
                refreshToken);

            return await RefreshToken(token, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Token> RefreshToken(Token token, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (token.ValidUntil - _clock.Now >= ValidityThreshold)
                return token;

            var payload = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", token.RefreshToken },
                { "client_id", _clientId }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_urlPrefix}/oauth2/token")
            {
                Content = new FormUrlEncodedContent(payload)
            };

            request.Headers.Authorization = new BasicAuthenticationHeaderValue(_clientId, _clientSecret);

            _logger.Debug("Getting new token from refresh token");

            var result = await (await _httpClient.SendAsync(request, cancellationToken ?? CancellationToken.None)).ThrowOnOAuth2Failure(_serializer, _logger);
            var response = _serializer.Deserialize<OAuth2Response>(await result.Content.ReadAsStringAsync());

            token.AccessToken = response.AccessToken;
            token.ValidUntil = _clock.Now + response.ExpiresIn;
            token.RefreshToken = response.RefreshToken;

            return token;
        }

        /// <inheritdoc />
        public async Task RevokeToken(Token token, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            var payload = new Dictionary<string, string>
            {
                { "token", token.RefreshToken }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_urlPrefix}/oauth2/revoke")
            {
                Content = new FormUrlEncodedContent(payload)
            };

            request.Headers.Authorization = new BasicAuthenticationHeaderValue(_clientId, _clientSecret);

            await (await _httpClient.SendAsync(request, cancellationToken ?? CancellationToken.None)).ThrowOnOAuth2Failure(_serializer, _logger);
        }
    }
}
