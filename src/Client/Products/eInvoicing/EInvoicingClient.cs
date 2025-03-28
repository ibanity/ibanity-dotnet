using System;
using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.eInvoicing
{
    /// <inheritdoc cref="IEInvoicingClient" />
    public class EInvoicingClient : ProductClient<ITokenProviderWithoutCodeVerifier>, IEInvoicingClient
    {
        /// <summary>
        /// Product name used as prefix in eInvoicing URIs.
        /// </summary>
        public const string UrlPrefix = "einvoicing";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="tokenService">Service to generate and refresh access tokens</param>
        /// <param name="clientAccessTokenService">Service to generate and refresh client access tokens.</param>
        /// <param name="customerTokenService">Service to generate and refresh customer access tokens.</param>
        public EInvoicingClient(IApiClient apiClient, ITokenProviderWithoutCodeVerifier tokenService, IClientAccessTokenProvider clientAccessTokenService, ICustomerAccessTokenProvider customerTokenService)
            : base(apiClient, tokenService, clientAccessTokenService, customerTokenService)
        {
            var peppolOutboundDocuments = new PeppolOutboundDocuments(apiClient, clientAccessTokenService, UrlPrefix);

            Suppliers = new Suppliers(apiClient, clientAccessTokenService, UrlPrefix);
            PeppolCustomerSearches = new PeppolCustomerSearches(apiClient, clientAccessTokenService, UrlPrefix);
            PeppolRegistrations = new PeppolRegistrations(apiClient, clientAccessTokenService, UrlPrefix);
            PeppolInvoices = new PeppolInvoices(apiClient, clientAccessTokenService, UrlPrefix);
            PeppolCreditNotes = new PeppolCreditNotes(apiClient, clientAccessTokenService, UrlPrefix);
            PeppolDocuments = peppolOutboundDocuments;
            PeppolOutboundDocuments = peppolOutboundDocuments;
            PeppolInboundDocuments = new PeppolInboundDocuments(apiClient, clientAccessTokenService, UrlPrefix);
            ZoomitCustomerSearches = new ZoomitCustomerSearches(apiClient, clientAccessTokenService, UrlPrefix);
            ZoomitInvoices = new ZoomitInvoices(apiClient, clientAccessTokenService, UrlPrefix);
            ZoomitCreditNotes = new ZoomitCreditNotes(apiClient, clientAccessTokenService, UrlPrefix);
            ZoomitDocuments = new ZoomitDocuments(apiClient, clientAccessTokenService, UrlPrefix);
        }

        /// <inheritdoc />
        public ISuppliers Suppliers { get; }

        /// <inheritdoc />
        public IPeppolCustomerSearches PeppolCustomerSearches { get; }

        /// <inheritdoc />
        public IPeppolRegistrations PeppolRegistrations { get; }

        /// <inheritdoc />
        public IPeppolInvoices PeppolInvoices { get; }

        /// <inheritdoc />
        public IPeppolCreditNotes PeppolCreditNotes { get; }

        /// <inheritdoc />
        public IPeppolDocuments PeppolDocuments { get; }

        /// <inheritdoc />
        public IPeppolOutboundDocuments PeppolOutboundDocuments { get; }

        /// <inheritdoc />
        public IPeppolInboundDocuments PeppolInboundDocuments { get; }

        /// <inheritdoc />
        public IZoomitCustomerSearches ZoomitCustomerSearches { get; }

        /// <inheritdoc />
        public IZoomitInvoices ZoomitInvoices { get; }

        /// <inheritdoc />
        public IZoomitCreditNotes ZoomitCreditNotes { get; }

        /// <inheritdoc />
        public IZoomitDocuments ZoomitDocuments { get; }
    }

    /// <summary>
    /// Contains services for all eInvoicing-related resources.
    /// </summary>
    public interface IEInvoicingClient : IProductClientWithClientAccessToken
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
        /// Peppol Registrations
        /// </summary>
        IPeppolRegistrations PeppolRegistrations { get; }

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

        /// <summary>
        /// Peppol Outbound Document
        /// </summary>
        [Obsolete("Prefer 'PeppolOutboundDocument' property")]
        IPeppolDocuments PeppolDocuments { get; }

        /// <summary>
        /// Peppol Outbound Document
        /// </summary>
        IPeppolOutboundDocuments PeppolOutboundDocuments { get; }

        /// <summary>
        /// Peppol inbound documents
        /// </summary>
        IPeppolInboundDocuments PeppolInboundDocuments { get; }

        /// <summary>
        /// <p>This endpoint allows you to search for a customer on the Zoomit network. Based on the response you know whether the customer is reachable on Zoomit or not.</p>
        /// <p>A list of test receivers can be found in <see href="https://documentation.ibanity.com/einvoicing/products#development-resources">Development Resources</see></p>
        /// </summary>
        IZoomitCustomerSearches ZoomitCustomerSearches { get; }

        /// <summary>
        /// <p>This is an object representing the invoice that can be sent by a supplier. This document is always an UBL in BIS 3 format with additional validations.</p>
        /// <p>CodaBox expects the following format for Zoomit invoices: <see href="http://docs.peppol.eu/poacc/billing/3.0/">Peppol BIS 3.0</see></p>
        /// <p>CodaBox will verify the compliance of the UBL with XSD and schematron rules (you can find the CodaBox schematron rules <see href="https://documentation.ibanity.com/einvoicing/ZOOMIT-EN16931-UBL.sch">here</see>)</p>
        /// <p>In order to send an invoice to Zoomit, some additional fields are required</p>
        /// </summary>
        IZoomitInvoices ZoomitInvoices { get; }

        /// <summary>
        /// <p>This is an object representing the credit note that can be sent by a supplier. This document is always an UBL in BIS 3 format with additional validations.</p>
        /// <p>CodaBox expects the following format for Zoomit credit notes: <see href="http://docs.peppol.eu/poacc/billing/3.0/">Peppol BIS 3.0</see></p>
        /// <p>CodaBox will verify the compliance of the UBL with XSD and schematron rules (you can find the CodaBox schematron rules <see href="https://documentation.ibanity.com/einvoicing/ZOOMIT-EN16931-UBL.sch">here</see>)</p>
        /// <p>In order to send a credit note to Zoomit, some additional fields are required</p>
        /// </summary>
        IZoomitCreditNotes ZoomitCreditNotes { get; }

        /// <summary>
        /// Zoomit document
        /// </summary>
        IZoomitDocuments ZoomitDocuments { get; }
    }
}
