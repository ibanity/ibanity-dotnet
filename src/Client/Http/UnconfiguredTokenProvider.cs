using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ibanity.Apis.Client.Http
{
    /// <summary>
    /// Token provider only throwing exception.
    /// </summary>
    /// <remarks>Used when client ID and client secret aren't configured.</remarks>
    public class UnconfiguredTokenProvider : ITokenProviderWithCodeVerifier, ITokenProviderWithoutCodeVerifier, IClientAccessTokenProvider, ICustomerAccessTokenProvider
    {
        /// <summary>
        /// Singleton instance
        /// </summary>
        public static readonly ITokenProviderWithCodeVerifier InstanceWithCodeVerifier;

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static readonly ITokenProviderWithoutCodeVerifier InstanceWithoutCodeVerifier;

        /// <summary>
        /// Singleton instance for client access token
        /// </summary>
        public static readonly IClientAccessTokenProvider ClientAccessInstance;

        /// <summary>
        /// Singleton instance for customer access token
        /// </summary>
        public static readonly ICustomerAccessTokenProvider CustomerAccessInstance;

        static UnconfiguredTokenProvider()
        {
            var instance = new UnconfiguredTokenProvider();

            InstanceWithCodeVerifier = instance;
            InstanceWithoutCodeVerifier = instance;
            ClientAccessInstance = instance;
            CustomerAccessInstance = instance;
        }

        private UnconfiguredTokenProvider() { }

        private const string Message = "Missing client ID and client secret";

        /// <inheritdoc />
        /// <remarks>Does nothing besides throwing an exception.</remarks>
        public Task<Token> GetToken(string authorizationCode, string codeVerifier, Uri redirectUri, CancellationToken? cancellationToken = null) =>
            throw new IbanityConfigurationException(Message);

        /// <inheritdoc />
        /// <remarks>Does nothing besides throwing an exception.</remarks>
        public Task<Token> GetToken(string authorizationCode, string codeVerifier, string redirectUri, CancellationToken? cancellationToken = null) =>
            throw new IbanityConfigurationException(Message);

        /// <inheritdoc />
        /// <remarks>Does nothing besides throwing an exception.</remarks>
        public Task<Token> GetToken(string authorizationCode, Uri redirectUri, CancellationToken? cancellationToken = null) =>
            throw new IbanityConfigurationException(Message);

        /// <inheritdoc />
        /// <remarks>Does nothing besides throwing an exception.</remarks>
        public Task<Token> GetToken(string authorizationCode, string redirectUri, CancellationToken? cancellationToken = null) =>
            throw new IbanityConfigurationException(Message);

        /// <inheritdoc />
        /// <remarks>Does nothing besides throwing an exception.</remarks>
        public Task<Token> GetToken(string refreshToken, CancellationToken? cancellationToken = null) =>
            throw new IbanityConfigurationException(Message);

        /// <inheritdoc />
        /// <remarks>Does nothing besides throwing an exception.</remarks>
        public Task<ClientAccessToken> GetToken(CancellationToken? cancellationToken = null) =>
            throw new IbanityConfigurationException(Message);

        /// <inheritdoc />
        /// <remarks>Does nothing besides throwing an exception.</remarks>
        public Task<Token> RefreshToken(Token token, CancellationToken? cancellationToken = null) =>
            throw new IbanityConfigurationException(Message);

        /// <inheritdoc />
        /// <remarks>Does nothing besides throwing an exception.</remarks>
        public Task<ClientAccessToken> RefreshToken(ClientAccessToken token, CancellationToken? cancellationToken = null) =>
            throw new IbanityConfigurationException(Message);

        /// <inheritdoc />
        /// <remarks>Does nothing besides throwing an exception.</remarks>
        public Task RevokeToken(Token token, CancellationToken? cancellationToken = null) =>
            throw new IbanityConfigurationException(Message);

        /// <inheritdoc />
        /// <remarks>Does nothing besides throwing an exception.</remarks>
        Task<CustomerAccessToken> ICustomerAccessTokenProvider.GetToken(CancellationToken? cancellationToken) =>
            throw new IbanityConfigurationException("Product doesn't support customer access tokens");

        /// <inheritdoc />
        /// <remarks>Does nothing besides throwing an exception.</remarks>
        Task<CustomerAccessToken> IAccessTokenProvider<CustomerAccessToken>.RefreshToken(CustomerAccessToken token, CancellationToken? cancellationToken) =>
            throw new IbanityConfigurationException("Product doesn't support customer access tokens");
    }
}
