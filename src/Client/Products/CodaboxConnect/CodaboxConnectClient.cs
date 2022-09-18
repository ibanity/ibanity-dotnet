using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.CodaboxConnect
{
    /// <inheritdoc cref="ICodaboxConnectClient" />
    public class CodaboxConnectClient : ProductClient<ITokenProviderWithoutCodeVerifier>, ICodaboxConnectClient
    {
        /// <summary>
        /// Product name used as prefix in Codabox Connect URIs.
        /// </summary>
        public const string UrlPrefix = "codabox-connect";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="tokenService">Service to generate and refresh access tokens</param>
        /// <param name="clientAccessTokenService">Service to generate and refresh client access tokens.</param>
        public CodaboxConnectClient(IApiClient apiClient, ITokenProviderWithoutCodeVerifier tokenService, IClientAccessTokenProvider clientAccessTokenService)
            : base(apiClient, tokenService, clientAccessTokenService)
        {
            AccountingOfficeConsents = new AccountingOfficeConsents(apiClient, clientAccessTokenService, UrlPrefix);
            DocumentSearches = new DocumentSearches(apiClient, clientAccessTokenService, UrlPrefix);
            BankAccountStatements = new BankAccountStatements(apiClient, clientAccessTokenService, UrlPrefix);
            PayrollStatements = new PayrollStatements(apiClient, clientAccessTokenService, UrlPrefix);
            CreditCardStatements = new CreditCardStatements(apiClient, clientAccessTokenService, UrlPrefix);
            SalesInvoices = new SalesInvoices(apiClient, clientAccessTokenService, UrlPrefix);
        }

        /// <inheritdoc />
        public IAccountingOfficeConsents AccountingOfficeConsents { get; }

        /// <inheritdoc />
        public IDocumentSearches DocumentSearches { get; }

        /// <inheritdoc />
        public IBankAccountStatements BankAccountStatements { get; }

        /// <inheritdoc />
        public IPayrollStatements PayrollStatements { get; }

        /// <inheritdoc />
        public ICreditCardStatements CreditCardStatements { get; }

        /// <inheritdoc />
        public ISalesInvoices SalesInvoices { get; }
    }

    /// <summary>
    /// Contains services for all Codabox Connect-related resources.
    /// </summary>
    public interface ICodaboxConnectClient : IProductClient
    {
        /// <summary>
        /// This resource allows an Accounting Software to create a new Accounting Office Consent. This consent allows an Accounting Software to retrieve the documents of clients of an Accounting Office.
        /// </summary>
        IAccountingOfficeConsents AccountingOfficeConsents { get; }

        /// <summary>
        /// This resource allows an Accounting Software to search for documents of clients of an Accounting Office. Documents can be searched by type, for one or multiple clients. Additionally, it is possible to filter documents within a given period of time. This resource supports pagination.
        /// </summary>
        IDocumentSearches DocumentSearches { get; }

        /// <summary>
        /// This resource allows an Accounting Software to retrieve a bank account statement for a client of an accounting office.
        /// </summary>
        IBankAccountStatements BankAccountStatements { get; }

        /// <summary>
        /// This resource allows an Accounting Software to retrieve a Payroll Statement for a client of an accounting office.
        /// </summary>
        IPayrollStatements PayrollStatements { get; }

        /// <summary>
        /// This resource allows an Accounting Software to retrieve a credit card statement for a client of an accounting office.
        /// </summary>
        ICreditCardStatements CreditCardStatements { get; }

        /// <summary>
        /// This resource allows an Accounting Software to retrieve a sales invoice or credit note document for a client of an accounting office.
        /// </summary>
        ISalesInvoices SalesInvoices { get; }
    }
}
