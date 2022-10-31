using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="IXS2AClient" />
    public class XS2AClient : ProductClient<ITokenProviderWithoutCodeVerifier>, IXS2AClient
    {
        /// <summary>
        /// Product name used as prefix in XS2A URIs.
        /// </summary>
        public const string UrlPrefix = "xs2a";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="tokenService">Service to generate and refresh access tokens</param>
        /// <param name="clientAccessTokenService">Service to generate and refresh client access tokens.</param>
        /// <param name="customerTokenService">Service to generate and refresh customer access tokens.</param>
        public XS2AClient(IApiClient apiClient, ITokenProviderWithoutCodeVerifier tokenService, IClientAccessTokenProvider clientAccessTokenService, ICustomerAccessTokenProvider customerTokenService)
            : base(apiClient, tokenService, clientAccessTokenService, customerTokenService)
        {
            Customers = new Customers(apiClient, customerTokenService, UrlPrefix);
            FinancialInstitutions = new FinancialInstitutions(apiClient, customerTokenService, UrlPrefix);
            FinancialInstitutionCountries = new FinancialInstitutionCountries(apiClient, customerTokenService, UrlPrefix);
        }

        /// <inheritdoc />
        public ICustomers Customers { get; }

        /// <inheritdoc />
        public IFinancialInstitutions FinancialInstitutions { get; }

        /// <inheritdoc />
        public IFinancialInstitutionCountries FinancialInstitutionCountries { get; }
    }

    /// <summary>
    /// Contains services for all XS2A-related resources.
    /// </summary>
    public interface IXS2AClient : IProductClientWithCustomerAccessToken
    {
        /// <summary>
        /// <p>This is an object representing a customer. A customer resource is created with the creation of a related customer access token.</p>
        /// <p>In the case that the contractual relationship between you and your customer is terminated, you should probably use the Delete Customer endpoint to erase ALL customer personal data.</p>
        /// <p>In the case that your customer wants to revoke your access to some accounts, you should use the Delete Account endpoint instead.</p>
        /// </summary>
        ICustomers Customers { get; }

        /// <summary>
        /// This is an object representing a financial institution, providing its basic details - including whether it is a sandbox object or not.
        /// </summary>
        /// <remarks>You can manage fake financial institutions in the sandbox using the create, update, and delete methods. Obviously, these endpoints will not work for real, live financial institutions.</remarks>
        IFinancialInstitutions FinancialInstitutions { get; }

        /// <summary>
        /// This endpoint provides a list of the unique countries for which there are financial institutions available in the list financial institutions endpoint. These codes can be used to filter the financial institutions by country.
        /// </summary>
        IFinancialInstitutionCountries FinancialInstitutionCountries { get; }
    }
}
