using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class Sandbox : ISandbox
    {
        public Sandbox(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix)
        {
            Accounts = new SandboxAccounts(apiClient, accessTokenProvider, urlPrefix);
            Transactions = new SandboxTransactions(apiClient, accessTokenProvider, urlPrefix);
        }

        public ISandboxAccounts Accounts { get; }
        public ISandboxTransactions Transactions { get; }
    }

    public interface ISandbox
    {
        ISandboxAccounts Accounts { get; }
        ISandboxTransactions Transactions { get; }
    }
}
