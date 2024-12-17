using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Products.eInvoicing.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.eInvoicing
{
    /// <inheritdoc cref="IPeppolInboundDocuments" />
    public class PeppolInboundDocuments : ResourceClient<PeppolInboundDocument, object, PeppolInboundDocumentRelationships, object, ClientAccessToken>, IPeppolInboundDocuments
    {
        private const string EntityName = "peppol/inbound-documents";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PeppolInboundDocuments(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        protected override PeppolInboundDocument Map(Data<PeppolInboundDocument, object, PeppolInboundDocumentRelationships, object> data)
        {
            var result = base.Map(data);

            result.SupplierId = Guid.Parse(data.Relationships.Supplier.Data.Id);

            return result;
        }
    }

    /// <summary>
    /// Peppol inbound documents
    /// </summary>
    public interface IPeppolInboundDocuments
    {
    }
}
