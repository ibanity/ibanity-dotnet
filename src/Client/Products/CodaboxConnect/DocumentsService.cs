using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.CodaboxConnect
{
    /// <inheritdoc cref="IBankAccountStatements" />
    public abstract class DocumentsService<T> : ResourceWithParentClient<T, object, object, object, string, Guid, ClientAccessToken> where T : IIdentified<Guid>
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

        /// <inheritdoc />
        public Task<T> Get(ClientAccessToken token, Guid accountingOfficeId, string clientId, Guid id, CancellationToken? cancellationToken = null) =>
            InternalGet(token, new[] { accountingOfficeId.ToString(), clientId }, id, cancellationToken);

        /// <inheritdoc />
        public Task GetPdf(ClientAccessToken token, Guid accountingOfficeId, string clientId, Guid id, Stream target, CancellationToken? cancellationToken = null) =>
            InternalGetToStream(token, new[] { accountingOfficeId.ToString(), clientId }, id, "application/pdf", target, cancellationToken);

        /// <inheritdoc />
        public Task GetCoda(ClientAccessToken token, Guid accountingOfficeId, string clientId, Guid id, Stream target, CancellationToken? cancellationToken = null) =>
            InternalGetToStream(token, new[] { accountingOfficeId.ToString(), clientId }, id, "application/vnd.coda.v1+cod", target, cancellationToken);

        /// <inheritdoc />
        protected override Guid ParseId(string id) => Guid.Parse(id);
    }
}
