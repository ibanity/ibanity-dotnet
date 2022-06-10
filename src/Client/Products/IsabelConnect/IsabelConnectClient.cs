using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;

namespace Ibanity.Apis.Client.Products.IsabelConnect
{
    /// <summary>
    /// Contains services for all Ponto Connect-releated resources.
    /// </summary>
    public class IsabelConnectClient : ProductClient, IIsabelConnectClient
    {
        /// <summary>
        /// Product name use as prefix in Ponto Connect URIs.
        /// </summary>
        public const string UrlPrefix = "isabel-connect";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="tokenService">Service to generate and refresh access tokens</param>
        /// <param name="clientAccessTokenService">Service to generate and refresh client access tokens.</param>
        public IsabelConnectClient(IApiClient apiClient, ITokenProvider tokenService, IClientAccessTokenProvider clientAccessTokenService)
            : base(apiClient, tokenService, clientAccessTokenService)
        {
            Accounts = new Accounts(apiClient, tokenService, UrlPrefix);
            Transactions = new Transactions(apiClient, tokenService, UrlPrefix);
            Balances = new Balances(apiClient, tokenService, UrlPrefix);
            IntradayTransactions = new IntradayTransactions(apiClient, tokenService, UrlPrefix);
            AccountReports = new AccountReports(apiClient, tokenService, UrlPrefix);
            BulkPaymentInitiationRequests = new BulkPaymentInitiationRequests(apiClient, tokenService, UrlPrefix);
        }

        /// <inheritdoc />
        public IAccounts Accounts { get; }

        /// <inheritdoc />
        public IBalances Balances { get; }

        /// <inheritdoc />
        public ITransactions Transactions { get; }

        /// <inheritdoc />
        public IIntradayTransactions IntradayTransactions { get; }

        /// <inheritdoc />
        public IAccountReports AccountReports { get; }

        /// <inheritdoc />
        public IBulkPaymentInitiationRequests BulkPaymentInitiationRequests { get; }
    }

    /// <summary>
    /// Contains services for all Ponto Connect-releated resources.
    /// </summary>
    public interface IIsabelConnectClient : IProductClient
    {
        /// <summary>
        /// <para>This is an object representing a customer account. This object will provide details about the account, including the reference and the currency.</para>
        /// <para>An account has related transactions and balances.</para>
        /// <para>The account API endpoints are customer specific and therefore can only be accessed by providing the corresponding access token.</para>
        /// </summary>
        IAccounts Accounts { get; }

        /// <summary>
        /// This is an object representing a balance related to a customer's account.
        /// </summary>
        IBalances Balances { get; }

        /// <summary>
        /// <para>This is an object representing an account transaction. This object will give you the details of the financial transaction, including its amount and execution date.</para>
        /// <para>Unlike an <see cref="IntradayTransaction" />, this is an end-of-day object which will not change.</para>
        /// </summary>
        ITransactions Transactions { get; }

        /// <summary>
        /// <para>This is an object representing an intraday account transaction. This object will give you the details of the intraday transaction, including its amount and execution date.</para>
        /// <para>At the end of the day, intraday transactions will be converted to transactions by the financial institution. The transactions will never be available as transactions and intraday transactions at the same time.</para>
        /// <para>Important: The ID of the intraday transaction will NOT be the same as the ID of the corresponding transaction.</para>
        /// </summary>
        IIntradayTransactions IntradayTransactions { get; }

        /// <summary>
        /// <para>This object provides details about an account report. From the list endpoint, you will receive a collection of the account report objects for the corresponding customer.</para>
        /// <para>Unlike other endpoints, the get endpoint will return the contents of the account report file instead of a json object. You can also find a link to the report in the account report object links.</para>
        /// </summary>
        IAccountReports AccountReports { get; }

        /// <summary>
        /// <para>This is an object representing a bulk payment initiation request. When you want to request the initiation of payments on behalf of one of your customers, you can create one to start the authorization flow.</para>
        /// <para>When creating the request, you should provide the payment information by uploading a PAIN xml file. <see href="https://documentation.ibanity.com/isabel-connect/products#bulk-payment-initiation">Learn more about the supported formats in Isabel Connect</see>.</para>
        /// </summary>
        IBulkPaymentInitiationRequests BulkPaymentInitiationRequests { get; }
    }
}
