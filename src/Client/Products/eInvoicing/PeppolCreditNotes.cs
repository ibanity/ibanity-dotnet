using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.eInvoicing.Models;

namespace Ibanity.Apis.Client.Products.eInvoicing
{
    /// <inheritdoc />
    public class PeppolCreditNotes : ResourceWithParentClient<PeppolCreditNote, object, object, object, ClientAccessToken>, IPeppolCreditNotes
    {
        private const string ParentEntityName = "peppol/suppliers";
        private const string EntityName = "credit-notes";

        private readonly IApiClient _apiClient;
        private readonly IAccessTokenProvider<ClientAccessToken> _accessTokenProvider;
        private readonly string _urlPrefix;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PeppolCreditNotes(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        {
            _apiClient = apiClient;
            _accessTokenProvider = accessTokenProvider;
            _urlPrefix = urlPrefix;
        }

        /// <inheritdoc />
        public Task<PeppolCreditNote> Get(ClientAccessToken token, Guid supplierId, Guid id, CancellationToken? cancellationToken = null) =>
            InternalGet(token, new[] { supplierId }, id, cancellationToken);

        /// <inheritdoc />
        public async Task<PeppolCreditNote> Create(ClientAccessToken token, Guid supplierId, string filename, string path, CancellationToken? cancellationToken = null)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                return await Create(token, supplierId, filename, stream, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<PeppolCreditNote> Create(ClientAccessToken token, Guid supplierId, string filename, Stream xmlContent, CancellationToken? cancellationToken = null)
        {
            var result = await _apiClient.PostInline<JsonApi.Resource<PeppolCreditNote, object, object, object>>(
                $"{_urlPrefix}/{ParentEntityName}/{supplierId}/{EntityName}",
                (await _accessTokenProvider.RefreshToken(token ?? throw new ArgumentNullException(nameof(token))).ConfigureAwait(false)).AccessToken,
                new Dictionary<string, string>(),
                filename,
                xmlContent,
                cancellationToken ?? CancellationToken.None).ConfigureAwait(false);

            return Map(result.Data);
        }
    }

    /// <summary>
    /// <p>This is an object representing the credit note that can be sent by a supplier. This document is always an UBL in a format supported by CodaBox.</p>
    /// <p>The maximum file size is 100MB.</p>
    /// </summary>
    public interface IPeppolCreditNotes
    {
        /// <summary>
        /// Get Peppol Credit Note
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="supplierId">Supplier ID</param>
        /// <param name="id">Credit Note ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The specified Peppol Credit Note resource</returns>
        Task<PeppolCreditNote> Get(ClientAccessToken token, Guid supplierId, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Create Peppol Credit Note
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="supplierId">Supplier ID</param>
        /// <param name="filename">Your filename</param>
        /// <param name="path">Local path the the XML file to upload</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a Peppol Credit Note resource</returns>
        Task<PeppolCreditNote> Create(ClientAccessToken token, Guid supplierId, string filename, string path, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Create Peppol Credit Note
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="supplierId">Supplier ID</param>
        /// <param name="filename">Your filename</param>
        /// <param name="xmlContent">XML content to upload</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a Peppol Credit Note resource</returns>
        Task<PeppolCreditNote> Create(ClientAccessToken token, Guid supplierId, string filename, Stream xmlContent, CancellationToken? cancellationToken = null);
    }
}
