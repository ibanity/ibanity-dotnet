using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Http
{
    public class OAuth2TokenProvider : IOAuth2TokenProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IClock _clock;

        private readonly string? _clientId;
        private readonly string? _clientSecret;
        private readonly string? _authorizationCode;
        private readonly string? _codeVerifier;
        private readonly Uri? _redirectUri;

        private string? _accessToken;
        private DateTimeOffset _validUntil = DateTimeOffset.MinValue;
        private string? _refreshToken;

        public OAuth2TokenProvider(HttpClient httpClient, IClock clock, string clientId, string clientSecret, string authorizationCode, string codeVerifier, Uri redirectUri)
        {
            _httpClient = httpClient;
            _clock = clock;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _authorizationCode = authorizationCode;
            _codeVerifier = codeVerifier;
            _redirectUri = redirectUri;
        }

        public OAuth2TokenProvider(HttpClient httpClient, IClock clock, string refreshToken)
        {
            _httpClient = httpClient;
            _clock = clock;
            _refreshToken = refreshToken;
        }

        public Task<string> GetRefreshToken(CancellationToken cancellationToken)
        {
            return Task.FromResult(string.Empty);
        }

        public Task<string> GetBearerToken(CancellationToken cancellationToken)
        {
            return Task.FromResult(string.Empty);
        }
    }

    public interface IOAuth2TokenProvider
    {
        Task<string> GetRefreshToken(CancellationToken cancellationToken);
        Task<string> GetBearerToken(CancellationToken cancellationToken);
    }
}
