using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class SandboxTransactions : ISandboxTransactions
    {
        public SandboxTransactions(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix)
        {
        }
    }

    public interface ISandboxTransactions
    {

    }
}
