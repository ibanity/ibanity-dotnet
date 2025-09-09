using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.eInvoicing.Models;

namespace Ibanity.Apis.Client.Products.eInvoicing
{
    /// <inheritdoc cref="IPeppolInboundDocumentBusinessStatuses" />
    public class PeppolInboundDocumentBusinessStatuses : ResourceWithParentClient<PeppolInboundDocumentBusinessStatusResponse, object, object, object, ClientAccessToken>, IPeppolInboundDocumentBusinessStatuses
    {
        private const string ParentEntityName = "peppol/inbound-documents";
        private const string EntityName = "business-statuses";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PeppolInboundDocumentBusinessStatuses(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }
    }

    /// <summary>
    /// This is an object representing the business status of an inbound document that can be added by a supplier. Once created, business statuses are translated into an IMR document and transmitted to the sender of the inbound document. Unless otherwise stated, business status codes are mapped to the same IMR status code.
    /// </summary>
    public interface IPeppolInboundDocumentBusinessStatuses
    {
    }
}
