using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products
{
    public class ProductClient : IProductClient
    {
        public ProductClient(IApiClient apiClient, ITokenProvider tokenService, IBearerTokenProvider clientTokenService)
        {
            ApiClient = apiClient;
            TokenService = tokenService;
            ClientTokenService = clientTokenService;
        }

        public IApiClient ApiClient { get; }
        public ITokenProvider TokenService { get; }
        public IBearerTokenProvider ClientTokenService { get; }
    }

    public interface IProductClient
    {
        IApiClient ApiClient { get; }
        ITokenProvider TokenService { get; }
        IBearerTokenProvider ClientTokenService { get; }
    }
}
