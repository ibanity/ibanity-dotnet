using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ibanity.Apis.Client.Http
{
    /// <summary>
    /// Manage access tokens.
    /// </summary>
    public interface ITokenProvider : IAccessTokenProvider<Token>
    {
        /// <summary>
        /// Create a new access token using a previously received refresh token.
        /// </summary>
        /// <param name="refreshToken">Refresh token received during in previous authentication</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A new access token</returns>
        Task<Token> GetToken(string refreshToken, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Revoke a token.
        /// </summary>
        /// <param name="token">Token to be revoked</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        Task RevokeToken(Token token, CancellationToken? cancellationToken = null);
    }

    /// <inheritdoc />
    public interface ITokenProviderWithCodeVerifier : ITokenProvider
    {
        /// <summary>
        /// Create a new access token using code received during user linking process.
        /// </summary>
        /// <param name="authorizationCode">Authorization code provided during the user linking process</param>
        /// <param name="codeVerifier">PKCE code verifier corresponding to the code challenge string used in the authorization request</param>
        /// <param name="redirectUri">Redirection URL authorized for your application in the Developer Portal</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A new access token</returns>
        Task<Token> GetToken(string authorizationCode, string codeVerifier, Uri redirectUri, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Create a new access token using code received during user linking process.
        /// </summary>
        /// <param name="authorizationCode">Authorization code provided during the user linking process</param>
        /// <param name="codeVerifier">PKCE code verifier corresponding to the code challenge string used in the authorization request</param>
        /// <param name="redirectUri">Redirection URL authorized for your application in the Developer Portal</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A new access token</returns>
        Task<Token> GetToken(string authorizationCode, string codeVerifier, string redirectUri, CancellationToken? cancellationToken = null);
    }

    /// <inheritdoc />
    public interface ITokenProviderWithoutCodeVerifier : ITokenProvider
    {
        /// <summary>
        /// Create a new access token using code received during user linking process.
        /// </summary>
        /// <param name="authorizationCode">Authorization code provided during the user linking process</param>
        /// <param name="redirectUri">Redirection URL authorized for your application in the Developer Portal</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A new access token</returns>
        Task<Token> GetToken(string authorizationCode, Uri redirectUri, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Create a new access token using code received during user linking process.
        /// </summary>
        /// <param name="authorizationCode">Authorization code provided during the user linking process</param>
        /// <param name="redirectUri">Redirection URL authorized for your application in the Developer Portal</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A new access token</returns>
        Task<Token> GetToken(string authorizationCode, string redirectUri, CancellationToken? cancellationToken = null);
    }

    /// <summary>
    /// Allows clients to refresh (if needed) their access token.
    /// </summary>
    /// <typeparam name="T">Token type</typeparam>
    public interface IAccessTokenProvider<T>
    {
        /// <summary>
        /// Refresh access token if needed
        /// </summary>
        /// <param name="token">Token to check</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The token given as argument, refreshed if it was near expiration</returns>
        Task<T> RefreshToken(T token, CancellationToken? cancellationToken = null);
    }

    /// <summary>
    /// Manage client access tokens.
    /// </summary>
    public interface IClientAccessTokenProvider : IAccessTokenProvider<ClientAccessToken>
    {
        /// <summary>
        /// Create a new client access token from the client ID and client secret.
        /// </summary>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A new client access token</returns>
        Task<ClientAccessToken> GetToken(CancellationToken? cancellationToken = null);
    }
}
