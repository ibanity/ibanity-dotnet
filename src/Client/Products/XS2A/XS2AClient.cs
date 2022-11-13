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
            Synchronizations = new Synchronizations(apiClient, customerTokenService, UrlPrefix);
            BatchSynchronizations = new BatchSynchronizations(apiClient, customerTokenService, UrlPrefix);
            AccountInformationAccessRequests = new AccountInformationAccessRequests(apiClient, customerTokenService, UrlPrefix);
            AccountInformationAccessRequestAuthorizations = new AccountInformationAccessRequestAuthorizations(apiClient, customerTokenService, UrlPrefix);
            Accounts = new Accounts(apiClient, customerTokenService, UrlPrefix);
            Transactions = new Transactions(apiClient, customerTokenService, UrlPrefix);
        }

        /// <inheritdoc />
        public ICustomers Customers { get; }

        /// <inheritdoc />
        public IFinancialInstitutions FinancialInstitutions { get; }

        /// <inheritdoc />
        public IFinancialInstitutionCountries FinancialInstitutionCountries { get; }

        /// <inheritdoc />
        public ISynchronizations Synchronizations { get; }

        /// <inheritdoc />
        public IBatchSynchronizations BatchSynchronizations { get; }

        /// <inheritdoc />
        public IAccountInformationAccessRequests AccountInformationAccessRequests { get; }

        /// <inheritdoc />
        public IAccountInformationAccessRequestAuthorizations AccountInformationAccessRequestAuthorizations { get; }

        /// <inheritdoc />
        public IAccounts Accounts { get; }

        /// <inheritdoc />
        public ITransactions Transactions { get; }
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

        /// <summary>
        /// <p>This is an object representing a resource synchronization. This object will give you the details of the synchronization, including its resource, type, and status.</p>
        /// <p>The synchronization API endpoints are customer specific and therefore can only be accessed by providing the corresponding customer access token.</p>
        /// </summary>
        ISynchronizations Synchronizations { get; }

        /// <summary>
        /// This is an object representing a resource batch-synchronization. This object will give you the details of the batch-synchronization.
        /// </summary>
        IBatchSynchronizations BatchSynchronizations { get; }

        /// <summary>
        /// <p>This is an object representing an account information access request. When you want to access the account information of one of your customers, you have to create one to start the authorization flow.</p>
        /// <p>When creating the account information access request, you will receive the redirect link in the response. Your customer should be redirected to this url to begin the authorization process. At the end of the flow, they will be returned to the redirect uri that you defined.</p>
        /// <p>If the access request is not authorized (for example when the customer cancels the flow), an error query parameter will be added to the redirect uri. The possible values of this parameter are access_denied and unsupported_multi_currency_account.</p>
        /// <p>When authorizing account access by a financial institution user (in the sandbox), you should use 123456 as the digipass response. You can also use the Ibanity Sandbox Authorization Portal CLI to automate this authorization.</p>
        /// </summary>
        IAccountInformationAccessRequests AccountInformationAccessRequests { get; }

        /// <summary>
        /// <p>This object represent the authorization resource. When you perform an Authorization flow using TPP managed authorization flow, you need to create an authorization resource to complete the flow.</p>
        /// <p>The attribute queryParameters contains the query parameters returned by the financial institution when the customer is redirected to your configured redirect uri.</p>
        /// </summary>
        IAccountInformationAccessRequestAuthorizations AccountInformationAccessRequestAuthorizations { get; }

        /// <summary>
        /// <p>This is an object representing a customer account. This object will provide details about the account, including the balance and the currency.</p>
        /// <p>An account has related transactions and belongs to a financial institution.</p>
        /// <p>The account API endpoints are customer specific and therefore can only be accessed by providing the corresponding customer access token.</p>
        /// </summary>
        IAccounts Accounts { get; }

        /// <summary>
        /// <p>This is an object representing an account transaction. This object will give you the details of the financial transaction, including its amount and description.</p>
        /// <p>From this object, you can link back to its account.</p>
        /// <p>The transaction API endpoints are customer specific and therefore can only be accessed by providing the corresponding customer access token.</p>
        /// </summary>
        ITransactions Transactions { get; }
    }
}
