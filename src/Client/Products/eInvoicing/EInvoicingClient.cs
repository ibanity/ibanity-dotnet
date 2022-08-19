using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.eInvoicing
{
    /// <summary>
    /// Contains services for all eInvoicing-releated resources.
    /// </summary>
    public class EInvoicingClient : ProductClient<ITokenProviderWithoutCodeVerifier>, IEInvoicingClient
    {
        /// <summary>
        /// Product name use as prefix in eInvoicing URIs.
        /// </summary>
        public const string UrlPrefix = "einvoicing";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="tokenService">Service to generate and refresh access tokens</param>
        /// <param name="clientAccessTokenService">Service to generate and refresh client access tokens.</param>
        public EInvoicingClient(IApiClient apiClient, ITokenProviderWithoutCodeVerifier tokenService, IClientAccessTokenProvider clientAccessTokenService)
            : base(apiClient, tokenService, clientAccessTokenService)
        {
            Suppliers = new Suppliers(apiClient, clientAccessTokenService, UrlPrefix);
            PeppolCustomerSearches = new PeppolCustomerSearches(apiClient, clientAccessTokenService, UrlPrefix);
            PeppolInvoices = new PeppolInvoices(apiClient, clientAccessTokenService, UrlPrefix);
            PeppolCreditNotes = new PeppolCreditNotes(apiClient, clientAccessTokenService, UrlPrefix);
        }

        /// <inheritdoc />
        public ISuppliers Suppliers { get; }

        /// <inheritdoc />
        public IPeppolCustomerSearches PeppolCustomerSearches { get; }

        /// <inheritdoc />
        public IPeppolInvoices PeppolInvoices { get; }

        /// <inheritdoc />
        public IPeppolCreditNotes PeppolCreditNotes { get; }
    }

    /// <summary>
    /// Contains services for all eInvoicing-related resources.
    /// </summary>
    public interface IEInvoicingClient : IProductClient
    {
        /// <summary>
        /// This resource allows a Software Partner to create a new Supplier.
        /// </summary>
        ISuppliers Suppliers { get; }

        /// <summary>
        /// <p>This endpoint allows you to search for a customer on the PEPPOL network and the document types it supports. Based on the response you know:</p>
        /// <p>- whether the customer is available on Peppol and is capable to receive documents over Peppol</p>
        /// <p>- which UBL document types the customer is capable to receive</p>
        /// <p>A list of test receivers can be found in <see href="https://documentation.ibanity.com/einvoicing/products#development-resources">Development Resources</see></p>
        /// </summary>
        IPeppolCustomerSearches PeppolCustomerSearches { get; }

        /// <summary>
        /// <p>This is an object representing the invoice that can be sent by a supplier. This document is always an UBL in a format supported by CodaBox.</p>
        /// <p>The maximum file size is 100MB.</p>
        /// </summary>
        IPeppolInvoices PeppolInvoices { get; }

        /// <summary>
        /// <p>This is an object representing the credit note that can be sent by a supplier. This document is always an UBL in a format supported by CodaBox.</p>
        /// <p>The maximum file size is 100MB.</p>
        /// </summary>
        IPeppolCreditNotes PeppolCreditNotes { get; }
    }
}
