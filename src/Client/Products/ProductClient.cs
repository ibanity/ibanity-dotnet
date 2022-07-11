using System;
using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products
{
    /// <summary>
    /// Base product client.
    /// </summary>
    public abstract class ProductClient<T> : IProductWithRefreshTokenClient<T> where T : class, ITokenProvider
    {
        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="tokenService">Service to generate and refresh access tokens</param>
        /// <param name="clientAccessTokenService">Service to generate and refresh client access tokens.</param>
        protected ProductClient(IApiClient apiClient, T tokenService, IClientAccessTokenProvider clientAccessTokenService)
        {
            ApiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            TokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            ClientTokenService = clientAccessTokenService ?? throw new ArgumentNullException(nameof(clientAccessTokenService));
        }

        /// <inheritdoc />
        public IApiClient ApiClient { get; }

        /// <inheritdoc />
        public T TokenService { get; }

        /// <inheritdoc />
        public IClientAccessTokenProvider ClientTokenService { get; }
    }

    /// <summary>
    /// Base product client interface.
    /// </summary>
    public interface IProductWithRefreshTokenClient<T> : IProductClient where T : ITokenProvider
    {
        /// <summary>
        /// Service to generate and refresh access tokens.
        /// </summary>
        T TokenService { get; }
    }

    /// <summary>
    /// Base product client interface.
    /// </summary>
    public interface IProductClient
    {
        /// <summary>
        /// Generic API client.
        /// </summary>
        IApiClient ApiClient { get; }

        /// <summary>
        /// Service to generate and refresh client access tokens.
        /// </summary>
        IClientAccessTokenProvider ClientTokenService { get; }
    }
}
