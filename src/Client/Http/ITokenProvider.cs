using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ibanity.Apis.Client.Http
{
    public interface ITokenProvider : IAccessTokenProvider
    {
        Task<Token> GetToken(string authorizationCode, string codeVerifier, Uri redirectUri, CancellationToken? cancellationToken = null);
        Task<Token> GetToken(string refreshToken, CancellationToken? cancellationToken = null);
        Task RevokeToken(Token token, CancellationToken? cancellationToken = null);
    }

    public interface IAccessTokenProvider
    {
        Task<Token> RefreshToken(Token token, CancellationToken? cancellationToken = null);
    }

    public interface IClientAccessTokenProvider
    {
        Task<ClientAccessToken> GetToken(CancellationToken? cancellationToken = null);
        Task<ClientAccessToken> RefreshToken(ClientAccessToken token, CancellationToken? cancellationToken = null);
    }
}
