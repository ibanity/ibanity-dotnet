using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products
{
    public class ProductClient : IProductClient
    {
        public ProductClient(IApiClient apiClient, ITokenProvider tokenService, IAccessTokenProvider clientTokenService)
        {
            ApiClient = apiClient;
            TokenService = tokenService;
            ClientTokenService = clientTokenService;
        }

        public IApiClient ApiClient { get; }
        public ITokenProvider TokenService { get; }
        public IAccessTokenProvider ClientTokenService { get; }
    }

    public interface IProductClient
    {
        IApiClient ApiClient { get; }
        ITokenProvider TokenService { get; }
        IAccessTokenProvider ClientTokenService { get; }
    }
}
