using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <inheritdoc />
    public class Sandbox : ISandbox
    {
        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Sandbox(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix)
        {
            Accounts = new SandboxAccounts(apiClient, accessTokenProvider, urlPrefix);
            Transactions = new SandboxTransactions(apiClient, accessTokenProvider, urlPrefix);
        }

        /// <inheritdoc />
        public ISandboxAccounts Accounts { get; }

        /// <inheritdoc />
        public ISandboxTransactions Transactions { get; }
    }

    /// <summary>
    /// Fake accounts and transactions.
    /// </summary>
    public interface ISandbox
    {
        /// <summary>
        /// <para>This is an object representing a financial institution account, a fake account you can use for test purposes in a sandbox integration.</para>
        /// <para>These sandbox accounts are available only to the related organization, and can be authorized in the Ponto dashboard.</para>
        /// <para>A financial institution account belongs to a financial institution and can have many associated financial institution transactions.</para>
        /// </summary>
        ISandboxAccounts Accounts { get; }

        /// <summary>
        /// <para>This is an object representing a financial institution transaction, a fake transaction on a fake account you can create for test purposes.</para>
        /// <para>Once the account corresponding to the financial institution account has been synchronized, your custom financial institution transactions will be visible in the transactions list.</para>
        /// </summary>
        ISandboxTransactions Transactions { get; }
    }
}
