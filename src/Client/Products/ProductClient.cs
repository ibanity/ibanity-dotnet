using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products
{
    /// <summary>
    /// Base product client.
    /// </summary>
    public abstract class ProductClient : IProductClient
    {
        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="tokenService">Service to generate and refresh access tokens</param>
        /// <param name="clientAccessTokenService">Service to generate and refresh client access tokens.</param>
        protected ProductClient(IApiClient apiClient, ITokenProvider tokenService, IClientAccessTokenProvider clientAccessTokenService)
        {
            ApiClient = apiClient ?? throw new System.ArgumentNullException(nameof(apiClient));
            TokenService = tokenService ?? throw new System.ArgumentNullException(nameof(tokenService));
            ClientTokenService = clientAccessTokenService ?? throw new System.ArgumentNullException(nameof(clientAccessTokenService));
        }

        /// <inheritdoc />
        public IApiClient ApiClient { get; }

        /// <inheritdoc />
        public ITokenProvider TokenService { get; }

        /// <inheritdoc />
        public IClientAccessTokenProvider ClientTokenService { get; }
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
        /// Service to generate and refresh access tokens.
        /// </summary>
        ITokenProvider TokenService { get; }

        /// <summary>
        /// Service to generate and refresh client access tokens.
        /// </summary>
        IClientAccessTokenProvider ClientTokenService { get; }
    }
}
