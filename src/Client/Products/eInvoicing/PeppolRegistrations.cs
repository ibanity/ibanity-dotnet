using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.eInvoicing.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.eInvoicing
{
    /// <inheritdoc cref="IPeppolDocuments" />
    public class PeppolRegistrations : ResourceWithParentClient<PeppolRegistration, object, object, object, ClientAccessToken>, IPeppolRegistrations
    {
        private const string ParentEntityName = "peppol/suppliers";
        private const string EntityName = "registrations";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PeppolRegistrations(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<EInvoicingCollection<PeppolRegistration>> List(ClientAccessToken token, Guid supplierId, CancellationToken? cancellationToken = null) =>
            InternalPageBasedList(token, new[] { supplierId }, null, null, null, null, cancellationToken);
    }

    /// <summary>
    /// Peppol document
    /// </summary>
    public interface IPeppolRegistrations
    {
        /// <summary>
        /// List Registrations
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="supplierId">Supplier ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of registration resources</returns>
        Task<EInvoicingCollection<PeppolRegistration>> List(ClientAccessToken token, Guid supplierId, CancellationToken? cancellationToken = null);
    }
}
