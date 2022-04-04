using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class SandboxAccounts : ISandboxAccounts
    {
        public SandboxAccounts(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix)
        {
        }
    }

    public interface ISandboxAccounts
    {
    }
}
