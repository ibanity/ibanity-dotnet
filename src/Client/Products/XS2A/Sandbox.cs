using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc />
    public class Sandbox : ISandbox
    {
        /// <summary>
        /// Product name used as prefix in sandbox URIs.
        /// </summary>
        public const string UrlPrefix = "sandbox";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        public Sandbox(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider)
        {
            FinancialInstitutions = new SandboxFinancialInstitutions(apiClient, accessTokenProvider, UrlPrefix);
            FinancialInstitutionUsers = new SandboxFinancialInstitutionUsers(apiClient, accessTokenProvider, UrlPrefix);
            FinancialInstitutionAccounts = new SandboxFinancialInstitutionAccounts(apiClient, accessTokenProvider, UrlPrefix);
        }

        /// <inheritdoc />
        public ISandboxFinancialInstitutions FinancialInstitutions { get; }

        /// <inheritdoc />
        public ISandboxFinancialInstitutionUsers FinancialInstitutionUsers { get; }

        /// <inheritdoc />
        public ISandboxFinancialInstitutionAccounts FinancialInstitutionAccounts { get; }
    }

    /// <summary>
    /// Fake accounts and transactions.
    /// </summary>
    public interface ISandbox
    {
        /// <summary>
        /// This is an object representing a financial institution, providing its basic details - including whether it is a sandbox object or not.
        /// </summary>
        /// <remarks>You can manage fake financial institutions in the sandbox using the create, update, and delete methods. Obviously, these endpoints will not work for real, live financial institutions.</remarks>
        ISandboxFinancialInstitutions FinancialInstitutions { get; }

        /// <summary>
        /// <p>This is an object representing a financial institution user. It is a fake financial institution customer you can create for test purposes.</p>
        /// <p>From this object, you can follow the links to its related financial institution accounts and transactions.</p>
        /// </summary>
        ISandboxFinancialInstitutionUsers FinancialInstitutionUsers { get; }

        /// <summary>
        /// <p>This is an object representing a financial institution account, a fake account you create for test purposes.</p>
        /// <p>In addition to the regular account API calls, there are sandbox-specific endpoints available to manage your sandbox data.</p>
        /// <p>A financial institution account belongs to a financial institution user and financial institution and can have many associated financial institution transactions.</p>
        /// </summary>
        ISandboxFinancialInstitutionAccounts FinancialInstitutionAccounts { get; }
    }
}
