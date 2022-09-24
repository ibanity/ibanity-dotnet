using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.CodaboxConnect
{
    /// <summary>
    /// Base class for all documents clients.
    /// </summary>
    /// <typeparam name="TResource">Resource type</typeparam>
    /// <typeparam name="TId">Identifier type</typeparam>
    public abstract class DocumentsService<TResource, TId> : ResourceWithParentClient<TResource, object, object, object, string, TId, ClientAccessToken> where TResource : IIdentified<TId>
    {
        private const string TopLevelParentEntityName = "accounting-offices";
        private const string ParentEntityName = "clients";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        /// <param name="entityName">Last URL component</param>
        public DocumentsService(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix, string entityName) : base(apiClient, accessTokenProvider, urlPrefix, new[] { TopLevelParentEntityName, ParentEntityName, entityName }, false)
        { }

        /// <summary>
        /// Get resource.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="clientId">Resource's owner</param>
        /// <param name="id">Resource ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Resource</returns>
        public Task<TResource> Get(ClientAccessToken token, Guid accountingOfficeId, string clientId, TId id, CancellationToken? cancellationToken = null) =>
            InternalGet(token, new[] { accountingOfficeId.ToString(), clientId }, id, cancellationToken);

        /// <summary>
        /// Get PDF document
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="clientId">Resource's owner</param>
        /// <param name="id">Resource ID</param>
        /// <param name="target">Destination stream where the PDF document will be written</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a PDF representation of the resource.</returns>
        public Task GetPdf(ClientAccessToken token, Guid accountingOfficeId, string clientId, TId id, Stream target, CancellationToken? cancellationToken = null) =>
            InternalGetToStream(token, new[] { accountingOfficeId.ToString(), clientId }, id, "application/pdf", target, cancellationToken);

        /// <summary>
        /// Get resource CODA file
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="clientId">Resource's owner</param>
        /// <param name="id">Resource ID</param>
        /// <param name="target">Destination stream where the PDF document will be written</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns the CODA file of the resource.</returns>
        public Task GetCoda(ClientAccessToken token, Guid accountingOfficeId, string clientId, TId id, Stream target, CancellationToken? cancellationToken = null) =>
            InternalGetToStream(token, new[] { accountingOfficeId.ToString(), clientId }, id, "application/vnd.coda.v1+cod", target, cancellationToken);

        /// <summary>
        /// Get resource metadata
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="clientId">Resource's owner</param>
        /// <param name="id">Resource ID</param>
        /// <param name="target">Destination stream where the PDF document will be written</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns the JSON metadata of the resource.</returns>
        public Task GetJsonMetadata(ClientAccessToken token, Guid accountingOfficeId, string clientId, TId id, Stream target, CancellationToken? cancellationToken = null) =>
            InternalGetToStream(token, new[] { accountingOfficeId.ToString(), clientId }, id, "application/vnd.api+json", target, cancellationToken);

        /// <summary>
        /// Get resource as originally received-XML
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="clientId">Resource's owner</param>
        /// <param name="id">Resource ID</param>
        /// <param name="target">Destination stream where the PDF document will be written</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a resource in XML format as originally received.</returns>
        public Task GetXml(ClientAccessToken token, Guid accountingOfficeId, string clientId, TId id, Stream target, CancellationToken? cancellationToken = null) =>
            InternalGetToStream(token, new[] { accountingOfficeId.ToString(), clientId }, id, "application/xml", target, cancellationToken);
    }

    /// <inheritdoc />
    public abstract class GuidIdentifiedDocumentsService<TResource> : DocumentsService<TResource, Guid> where TResource : IIdentified<Guid>
    {
        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        /// <param name="entityName">Last URL component</param>
        protected GuidIdentifiedDocumentsService(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix, string entityName) :
            base(apiClient, accessTokenProvider, urlPrefix, entityName)
        {
        }

        /// <inheritdoc />
        protected override Guid ParseId(string id) => Guid.Parse(id);
    }
}
