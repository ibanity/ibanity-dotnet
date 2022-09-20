using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.CodaboxConnect.Models;

namespace Ibanity.Apis.Client.Products.CodaboxConnect
{
    /// <inheritdoc cref="IPurchaseInvoices" />
    public class PurchaseInvoices : DocumentsService<PurchaseInvoice>, IPurchaseInvoices
    {
        private const string EntityName = "purchase-invoices";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PurchaseInvoices(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) : base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }
    }

    /// <summary>
    /// This resource allows an Accounting Software to retrieve a purchase invoice or credit note document for a client of an accounting office.
    /// </summary>
    public interface IPurchaseInvoices
    {
        /// <summary>
        /// Get Purchase Invoice Statement
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="clientId">Purchase Invoice's owner</param>
        /// <param name="id">Purchase Invoice Statement ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a Purchase Invoice Statement resource.</returns>
        Task<PurchaseInvoice> Get(ClientAccessToken token, Guid accountingOfficeId, string clientId, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Purchase Invoice Statement PDF
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="clientId">Purchase Invoice's owner</param>
        /// <param name="id">Purchase Invoice Statement ID</param>
        /// <param name="target">Destination stream where the PDF document will be written</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a PDF representation of the Purchase Invoice Statement.</returns>
        Task GetPdf(ClientAccessToken token, Guid accountingOfficeId, string clientId, Guid id, Stream target, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Purchase Invoice as originally received-XML
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="clientId">Purchase Invoice's owner</param>
        /// <param name="id">Purchase Invoice ID</param>
        /// <param name="target">Destination stream where the PDF document will be written</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a sales invoice in XML format as originally received.</returns>
        Task GetXml(ClientAccessToken token, Guid accountingOfficeId, string clientId, Guid id, Stream target, CancellationToken? cancellationToken = null);
    }
}
