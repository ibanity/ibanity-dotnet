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
    }

    /// <summary>
    /// Peppol document
    /// </summary>
    public interface IPeppolRegistrations
    {
    }
}
