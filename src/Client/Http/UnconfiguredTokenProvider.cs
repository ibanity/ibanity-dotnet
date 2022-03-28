using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ibanity.Apis.Client.Http
{
    public class UnconfiguredTokenProvider : ITokenProvider, IClientAccessTokenProvider
    {
        private static readonly UnconfiguredTokenProvider _instance = new UnconfiguredTokenProvider();

        public static readonly ITokenProvider Instance = _instance;
        public static readonly IClientAccessTokenProvider ClientAccessInstance = _instance;

        private UnconfiguredTokenProvider() { }

        private const string Message = "Missing client ID and client secret";

        public Task<Token> GetToken(string authorizationCode, string codeVerifier, Uri redirectUri, CancellationToken? cancellationToken = null) =>
            throw new IbanityConfigurationException(Message);

        public Task<Token> GetToken(string refreshToken, CancellationToken? cancellationToken = null) =>
            throw new IbanityConfigurationException(Message);

        public Task<ClientAccessToken> GetToken(CancellationToken? cancellationToken = null) =>
            throw new IbanityConfigurationException(Message);

        public Task<Token> RefreshToken(Token token, CancellationToken? cancellationToken = null) =>
            throw new IbanityConfigurationException(Message);

        public Task<ClientAccessToken> RefreshToken(ClientAccessToken token, CancellationToken? cancellationToken = null) =>
            throw new IbanityConfigurationException(Message);

        public Task RevokeToken(Token token, CancellationToken? cancellationToken = null) =>
            throw new IbanityConfigurationException(Message);
    }
}
