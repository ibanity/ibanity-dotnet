using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.eInvoicing.Models;

namespace Ibanity.Apis.Client.Products.eInvoicing
{
    /// <inheritdoc />
    public class PeppolDocuments : ResourceClient<PeppolDocument, object, object, object, ClientAccessToken>, IPeppolDocuments
    {
        private const string EntityName = "peppol/documents";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PeppolDocuments(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }
    }

    /// <summary>
    /// Peppol document
    /// </summary>
    public interface IPeppolDocuments
    {
    }
}
