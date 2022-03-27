using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products
{
    public class ProductClient : IProductClient
    {
        public ProductClient(IApiClient apiClient, ITokenProvider tokenService, IClientAccessTokenProvider clientAccessTokenService)
        {
            ApiClient = apiClient;
            TokenService = tokenService;
            ClientTokenService = clientAccessTokenService;
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
