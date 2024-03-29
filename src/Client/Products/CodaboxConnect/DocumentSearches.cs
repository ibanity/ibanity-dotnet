using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.CodaboxConnect.Models;

namespace Ibanity.Apis.Client.Products.CodaboxConnect
{
    /// <inheritdoc cref="IDocumentSearches" />
    public class DocumentSearches : ResourceWithParentClient<DocumentSearchResponse, JsonApi.CollectionMeta<JsonApi.CursorBasedPaging>, DocumentSearchRelationshipsResponse, object, ClientAccessToken>, IDocumentSearches
    {
        private const string ParentEntityName = "accounting-offices";
        private const string EntityName = "document-searches";

        private readonly IApiClient _apiClient;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public DocumentSearches(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) : base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName }, false) =>
            _apiClient = apiClient;

        /// <inheritdoc />
        public async Task<DocumentSearchResponse> Create(ClientAccessToken token, Guid accountingOfficeId, DocumentSearch documentSearch, IEnumerable<string> clients, int? pageLimit = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (documentSearch is null)
                throw new ArgumentNullException(nameof(documentSearch));

            if (clients is null)
                throw new ArgumentNullException(nameof(clients));

            var payload = new JsonApi.Data<DocumentSearch, object, DocumentSearchRelationships, object>
            {
                Type = "documentSearch",
                Attributes = documentSearch,
                Relationships = new DocumentSearchRelationships
                {
                    Clients = new JsonApi.Relationships
                    {
                        Data = clients.Select(client => new JsonApi.Data
                        {
                            Type = "client",
                            Id = client
                        }).ToArray()
                    }
                }
            };

            var meta = pageLimit.HasValue || pageAfter.HasValue
                ? new JsonApi.CollectionMeta<JsonApi.CursorBasedPaging>
                {
                    Paging = new JsonApi.CursorBasedPaging
                    {
                        Limit = pageLimit,
                        After = pageAfter
                    }
                }
                : null;

            var fullResponse = await _apiClient.Post<JsonApi.Resource<DocumentSearch, object, DocumentSearchRelationships, object>, DocumentSearchFullResponse>(
                UrlPrefix + "/" + ParentEntityName + "/" + accountingOfficeId + "/" + EntityName,
                await GetAccessToken(token).ConfigureAwait(false),
                new JsonApi.Resource<DocumentSearch, object, DocumentSearchRelationships, object> { Data = payload, Meta = meta },
                null,
                cancellationToken ?? CancellationToken.None).ConfigureAwait(false);

            var result = Map(fullResponse.Data);

            result.Documents = fullResponse.Included.Documents.Select(d =>
            {
                switch (d.Attributes)
                {
                    case Document<Guid> typedDocument:
                        typedDocument.Type = d.Type;
                        typedDocument.Id = Guid.Parse(d.Id);
                        typedDocument.Client = d.Relationships.Client.Data.Id;
                        break;
                    case Document<string> typedDocument:
                        typedDocument.Type = d.Type;
                        typedDocument.Id = d.Id;
                        typedDocument.Client = d.Relationships.Client.Data.Id;
                        break;
                    default:
                        throw new IbanityException("Unsupported document ID: " + d.Id);
                }

                return d.Attributes;
            }).ToArray();

            return result;
        }

        /// <inheritdoc />
        protected override DocumentSearchResponse Map(JsonApi.Data<DocumentSearchResponse, JsonApi.CollectionMeta<JsonApi.CursorBasedPaging>, DocumentSearchRelationshipsResponse, object> data)
        {
            var result = base.Map(data);

            result.Clients = data.Relationships.Clients.Data.Select(c => c.Id).ToArray();

            return result;
        }
    }

    /// <summary>
    /// This resource allows an Accounting Software to search for documents of clients of an Accounting Office. Documents can be searched by type, for one or multiple clients. Additionally, it is possible to filter documents within a given period of time. This resource supports pagination.
    /// </summary>
    public interface IDocumentSearches
    {
        /// <summary>
        /// Create Document Search
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="documentSearch">An object representing a new Document Search</param>
        /// <param name="clients">Resource identifiers of the clients used to search for documents. Must contain at least one resource identifier and no more than 5000.</param>
        /// <param name="pageLimit">Maximum number (1-1000) of resources that might be returned. It is possible that the response contains fewer elements. Defaults to 100</param>
        /// <param name="pageAfter">Cursor that specifies the last resource of the previous page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created Accounting Office Consent resource</returns>
        Task<DocumentSearchResponse> Create(ClientAccessToken token, Guid accountingOfficeId, DocumentSearch documentSearch, IEnumerable<string> clients, int? pageLimit = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);
    }
}
