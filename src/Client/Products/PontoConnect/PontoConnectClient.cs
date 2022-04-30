using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <summary>
    /// Contains services for all Ponto Connect-releated resources.
    /// </summary>
    public class PontoConnectClient : ProductClient, IPontoConnectClient
    {
        /// <summary>
        /// Product name use as prefix in Ponto Connect URIs.
        /// </summary>
        public const string UrlPrefix = "ponto-connect";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="tokenService">Service to generate and refresh access tokens</param>
        /// <param name="clientAccessTokenService">Service to generate and refresh client access tokens.</param>
        public PontoConnectClient(IApiClient apiClient, ITokenProvider tokenService, IClientAccessTokenProvider clientAccessTokenService)
            : base(apiClient, tokenService, clientAccessTokenService)
        {
            FinancialInstitutions = new FinancialInstitutions(apiClient, tokenService, UrlPrefix);
            Accounts = new Accounts(apiClient, tokenService, UrlPrefix);
            Transactions = new Transactions(apiClient, tokenService, UrlPrefix);
            ReauthorizationRequests = new ReauthorizationRequests(apiClient, tokenService, UrlPrefix);
            Payments = new Payments(apiClient, tokenService, UrlPrefix);
            BulkPayments = new BulkPayments(apiClient, tokenService, UrlPrefix);
            Synchronizations = new Synchronizations(apiClient, tokenService, UrlPrefix);
            Sandbox = new Sandbox(apiClient, tokenService, UrlPrefix);
            OnboardingDetails = new OnboardingDetails(apiClient, clientAccessTokenService, UrlPrefix);
            UserInfo = new UserInfo(apiClient, tokenService, UrlPrefix);
            PaymentActivationRequests = new PaymentActivationRequests(apiClient, tokenService, UrlPrefix);
            Usages = new Usages(apiClient, clientAccessTokenService, UrlPrefix);
            Integrations = new Integrations(apiClient, clientAccessTokenService, UrlPrefix);
        }

        /// <inheritdoc />
        public IFinancialInstitutions FinancialInstitutions { get; }

        /// <inheritdoc />
        public IAccounts Accounts { get; }

        /// <inheritdoc />
        public ITransactions Transactions { get; }

        /// <inheritdoc />
        public IReauthorizationRequests ReauthorizationRequests { get; }

        /// <inheritdoc />
        public IPayments Payments { get; }

        /// <inheritdoc />
        public IBulkPayments BulkPayments { get; }

        /// <inheritdoc />
        public ISynchronizations Synchronizations { get; }

        /// <inheritdoc />
        public ISandbox Sandbox { get; }

        /// <inheritdoc />
        public IOnboardingDetails OnboardingDetails { get; }

        /// <inheritdoc />
        public IUserInfo UserInfo { get; }

        /// <inheritdoc />
        public IPaymentActivationRequests PaymentActivationRequests { get; }

        /// <inheritdoc />
        public IUsages Usages { get; }

        /// <inheritdoc />
        public IIntegrations Integrations { get; }
    }

    /// <summary>
    /// Contains services for all Ponto Connect-releated resources.
    /// </summary>
    public interface IPontoConnectClient : IProductClient
    {
        /// <summary>
        /// This is an object representing a financial institution, providing its basic details - ID and name. Only the financial institutions corresponding to authorized accounts will be available on the API.
        /// </summary>
        IFinancialInstitutions FinancialInstitutions { get; }

        /// <summary>
        /// <para>This is an object representing a user's account. This object will provide details about the account, including the balance and the currency.</para>
        /// <para>An account has related transactions and belongs to a financial institution.</para>
        /// <para>An account may be revoked from an integration using the revoke account endpoint. To recover access, the user must add the account back to the integration in their Ponto Dashboard or in a new authorization flow.</para>
        /// </summary>
        IAccounts Accounts { get; }

        /// <summary>
        /// <para>This is an object representing an account transaction. This object will give you the details of the financial transaction, including its amount and description.</para>
        /// <para>From this object, you can link back to its account.</para>
        /// </summary>
        ITransactions Transactions { get; }

        /// <summary>
        /// <para>This object allows you to request the reauthorization of a specific bank account.</para>
        /// <para>By providing a redirect URI, you can create a redirect link to which you can send your customer so they can directly reauthorize their account on Ponto. After reauthorizing at their bank portal, they are redirected automatically back to your application, to the redirect URI of your choosing.</para>
        /// </summary>
        IReauthorizationRequests ReauthorizationRequests { get; }

        /// <summary>
        /// <para>This is an object representing a payment. When you want to initiate payment from one of your user's accounts, you have to create one to start the authorization flow.</para>
        /// <para>If you provide a redirect URI when creating the payment, you will receive a redirect link to send your customer to to start the authorization flow. Note that for live payments, your user must have already requested and been granted payment service for their organization to use this flow.</para>
        /// <para>Otherwise, the user can sign the payment in the Ponto Dashboard.</para>
        /// <para>When authorizing payment initiation in the sandbox, you should use the pre-filled credentials and 123456 as the digipass response.</para>
        /// </summary>
        IPayments Payments { get; }

        /// <summary>
        /// <para>This is an object representing a bulk payment. When you want to initiate a bulk payment from one of your user's accounts, you have to create one to start the authorization flow.</para>
        /// <para>If you provide a redirect URI when creating the bulk payment, you will receive a redirect link to send your customer to to start the authorization flow. Note that for live bulk payments, your user must have already requested and been granted payment service for their organization to use this flow.</para>
        /// <para>Otherwise, the user can sign the bulk payment in the Ponto Dashboard.</para>
        /// <para>When authorizing bulk payment initiation in the sandbox, you should use the pre-filled credentials and 123456 as the digipass response.</para>
        /// </summary>
        IBulkPayments BulkPayments { get; }

        /// <summary>
        /// This is an object representing a resource synchronization. This object will give you the details of the synchronization, including its resource, type, and status.
        /// </summary>
        ISynchronizations Synchronizations { get; }

        /// <summary>
        /// Fake accounts and transactions.
        /// </summary>
        ISandbox Sandbox { get; }

        /// <summary>
        /// <para>This is an object representing the onboarding details of the user who will undergo the linking process. It allows you to prefill the sign in or sign up forms with the user's details to streamline their Ponto onboarding process.</para>
        /// <para>For security purposes, the onboarding details object will only be available to be linked to a new Ponto user for five minutes following its creation. You should include the id in the access authorization url as an additional query parameter.</para>
        /// </summary>
        IOnboardingDetails OnboardingDetails { get; }

        /// <summary>
        /// <para>This endpoint provides information about the subject (organization) of an access token. Minimally, it provides the organization's id as the token's sub. If additional organization information was requested in the scope of the authorization, it will be provided here.</para>
        /// <para>The organization's id can be used to request its usage. Keep in mind that if the access token is revoked, this endpoint will no longer be available, so you may want to store the organization's id in your system.</para>
        /// </summary>
        IUserInfo UserInfo { get; }

        /// <summary>
        /// <para>This is an object representing a payment activation request. If your customer has not yet requested payment activation for their organization (as indicated by the user info endpoint), you can redirect them to Ponto to submit a request for payment activation.</para>
        /// <para>When creating the payment activation request, you will receive the redirect link in the response. Your customer should be redirected to this url to begin the process. At the end of the flow, they will be returned to the redirect uri that you defined.</para>
        /// <para>When using this endpoint in the sandbox, the redirection flow will work but the user will not be prompted to request payment activation as this is enabled by default in the sandbox.</para>
        /// </summary>
        IPaymentActivationRequests PaymentActivationRequests { get; }

        /// <summary>
        /// This endpoint provides the usage of your integration by the provided organization during a given month. In order to continue to allow access to this information if an integration is revoked, you must use a client access token for this endpoint.
        /// </summary>
        IUsages Usages { get; }

        /// <summary>
        /// This endpoint provides an alternative method to revoke the integration (in addition to the revoke refresh token endpoint). This endpoint remains accessible with a client access token, even if your refresh token is lost or expired.
        /// </summary>
        IIntegrations Integrations { get; }
    }
}
