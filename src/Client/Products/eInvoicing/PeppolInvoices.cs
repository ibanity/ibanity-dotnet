using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.eInvoicing.Models;

namespace Ibanity.Apis.Client.Products.eInvoicing
{
    /// <inheritdoc />
    public class PeppolInvoices : ResourceWithParentClient<PeppolInvoice, object, object, object, ClientAccessToken>, IPeppolInvoices
    {
        private const string ParentEntityName = "peppol/suppliers";
        private const string EntityName = "invoices";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PeppolInvoices(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }
    }

    /// <summary>
    /// <p>This is an object representing the invoice that can be sent by a supplier. This document is always an UBL in a format supported by CodaBox.</p>
    /// <p>The maximum file size is 100MB.</p>
    /// </summary>
    public interface IPeppolInvoices
    {
    }
}
