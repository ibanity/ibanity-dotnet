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
        public Task<EInvoicingCollection<PeppolInboundDocument>> List(ClientAccessToken token, DateTimeOffset? fromCreatedAt, Guid? supplierId, long? pageNumber = null, int? pageSize = null, CancellationToken? cancellationToken = null)
        {
            var parameters = new List<(string, string)>();

            if (fromCreatedAt.HasValue)
                parameters.Add(("fromCreatedAt", fromCreatedAt.Value.ToString("o")));

            if (supplierId.HasValue)
                parameters.Add(("supplierId", supplierId.Value.ToString("D")));

            return InternalPageBasedList(token, null, parameters, pageNumber, pageSize, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PeppolInboundDocument> Get(ClientAccessToken token, Guid id, CancellationToken? cancellationToken = null) =>
            InternalGet(token, id, cancellationToken);

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
        /// <summary>
        /// List Peppol Inbound Documents
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="fromCreatedAt">Start of the document created date period scope.</param>
        /// <param name="supplierId">The uuid of the supplier given during the on boarding process.</param>
        /// <param name="pageNumber">Number of page that should be returned. Must be included to use page-based pagination.</param>
        /// <param name="pageSize">Number (1-2000) of document resources that you want to be returned. Defaults to 2000.</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of Peppol Inbound Document resources</returns>
        Task<EInvoicingCollection<PeppolInboundDocument>> List(ClientAccessToken token, DateTimeOffset? fromCreatedAt, Guid? supplierId, long? pageNumber = null, int? pageSize = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Peppol Inbound Document
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="id">Document ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns></returns>
        Task<PeppolInboundDocument> Get(ClientAccessToken token, Guid id, CancellationToken? cancellationToken = null);
    }
}
