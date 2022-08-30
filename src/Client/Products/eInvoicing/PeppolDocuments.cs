using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.eInvoicing.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.eInvoicing
{
    /// <inheritdoc cref="IPeppolDocuments" />
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

        /// <inheritdoc />
        public Task<EInvoicingCollection<PeppolDocument>> List(ClientAccessToken token, DateTimeOffset? fromStatusChanged, DateTimeOffset? toStatusChanged, long? pageNumber = null, int? pageSize = null, CancellationToken? cancellationToken = null)
        {
            var parameters = new List<(string, string)>();

            if (fromStatusChanged.HasValue)
                parameters.Add(("fromStatusChanged", fromStatusChanged.Value.ToString("o")));

            if (toStatusChanged.HasValue)
                parameters.Add(("toStatusChanged", toStatusChanged.Value.ToString("o")));

            return InternalPageBasedList(token, null, parameters, pageNumber, pageSize, cancellationToken);
        }
    }

    /// <summary>
    /// Peppol document
    /// </summary>
    public interface IPeppolDocuments
    {
        /// <summary>
        /// List Accounts
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="fromStatusChanged">Start of the document status change period scope. Must be within 7 days of the toStatusChanged date-time.</param>
        /// <param name="toStatusChanged">End of the document status change period scope. Must be equal to or later than fromStatusChanged. Defaults to the current date-time.</param>
        /// <param name="pageNumber">Number of page that should be returned. Must be included to use page-based pagination.</param>
        /// <param name="pageSize">Number (1-2000) of document resources that you want to be returned. Defaults to 2000.</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of Peppol Document resources</returns>
        Task<EInvoicingCollection<PeppolDocument>> List(ClientAccessToken token, DateTimeOffset? fromStatusChanged, DateTimeOffset? toStatusChanged, long? pageNumber = null, int? pageSize = null, CancellationToken? cancellationToken = null);
    }
}
