using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products
{
    public abstract class ProductClient : IProductClient
    {
        public ProductClient(IApiClient apiClient, ITokenProvider tokenService, IClientAccessTokenProvider clientAccessTokenService)
        {
            ApiClient = apiClient ?? throw new System.ArgumentNullException(nameof(apiClient));
            TokenService = tokenService ?? throw new System.ArgumentNullException(nameof(tokenService));
            ClientTokenService = clientAccessTokenService ?? throw new System.ArgumentNullException(nameof(clientAccessTokenService));
        }

        public IApiClient ApiClient { get; }
        public ITokenProvider TokenService { get; }
        public IClientAccessTokenProvider ClientTokenService { get; }
    }

    public interface IProductClient
    {
        IApiClient ApiClient { get; }
        ITokenProvider TokenService { get; }
        IClientAccessTokenProvider ClientTokenService { get; }
    }
}
