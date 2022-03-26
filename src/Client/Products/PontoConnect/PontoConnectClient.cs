using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class PontoConnectClient : ProductClient, IPontoConnectClient
    {
        public PontoConnectClient(IApiClient apiClient, ITokenProvider tokenService, IBearerTokenProvider clientTokenService)
            : base(apiClient, tokenService, clientTokenService)
        {
        }
    }

    public interface IPontoConnectClient : IProductClient
    {
    }
}
