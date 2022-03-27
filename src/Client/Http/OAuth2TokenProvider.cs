using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Http
{
    public class OAuth2TokenProvider : ITokenProvider
    {
        private static readonly TimeSpan ValidityThreshold = TimeSpan.FromMinutes(1d);

        private readonly HttpClient _httpClient;
        private readonly IClock _clock;
        private readonly ISerializer<string> _serializer;
        private readonly string _urlPrefix;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public OAuth2TokenProvider(HttpClient httpClient, IClock clock, ISerializer<string> serializer, string urlPrefix, string clientId, string clientSecret)
        {
            _httpClient = httpClient;
            _clock = clock;
            _serializer = serializer;
            _urlPrefix = urlPrefix;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task<Token> GetToken(string authorizationCode, string codeVerifier, Uri redirectUri, CancellationToken? cancellationToken)
        {
            var payload = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", authorizationCode },
                { "client_id", _clientId },
                { "redirect_uri", redirectUri.ToString() },
                { "code_verifier", codeVerifier }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_urlPrefix}/oauth2/token")
            {
                Content = new FormUrlEncodedContent(payload)
            };

            request.Headers.Authorization = new BasicAuthenticationHeaderValue(_clientId, _clientSecret);

            var result = (await _httpClient.SendAsync(request, cancellationToken ?? CancellationToken.None)).EnsureSuccessStatusCode();
            var response = _serializer.Deserialize<OAuth2Response>(await result.Content.ReadAsStringAsync());

            return new Token(
                response.AccessToken,
                _clock.Now + response.ExpiresIn,
                response.RefreshToken);
        }

        public async Task<Token> GetToken(string refreshToken, CancellationToken? cancellationToken)
        {
            var token = new Token(
                null,
                DateTimeOffset.MinValue,
                refreshToken);

            return await RefreshToken(token, cancellationToken);
        }

        public async Task<Token> RefreshToken(Token token, CancellationToken? cancellationToken)
        {
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

            var result = (await _httpClient.SendAsync(request, cancellationToken ?? CancellationToken.None)).EnsureSuccessStatusCode();
            var response = _serializer.Deserialize<OAuth2Response>(await result.Content.ReadAsStringAsync());

            return new Token(
                response.AccessToken,
                _clock.Now + response.ExpiresIn,
                response.RefreshToken);
        }

        public async Task RevokeToken(Token token, CancellationToken? cancellationToken)
        {
            var payload = new Dictionary<string, string>
            {
                { "token", token.RefreshToken }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_urlPrefix}/oauth2/revoke")
            {
                Content = new FormUrlEncodedContent(payload)
            };

            request.Headers.Authorization = new BasicAuthenticationHeaderValue(_clientId, _clientSecret);

            var result = (await _httpClient.SendAsync(request, cancellationToken ?? CancellationToken.None)).EnsureSuccessStatusCode();
        }
    }
}
