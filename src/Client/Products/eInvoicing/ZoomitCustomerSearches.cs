using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.eInvoicing.Models;

namespace Ibanity.Apis.Client.Products.eInvoicing
{
    /// <inheritdoc />
    public class ZoomitCustomerSearches : ResourceWithParentClient<ZoomitCustomerSearchResponse, object, object, object, ClientAccessToken>, IZoomitCustomerSearches
    {
        private const string ParentEntityName = "zoomit/suppliers";
        private const string EntityName = "customer-searches";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public ZoomitCustomerSearches(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName }, false)
        { }

        /// <inheritdoc />
        public Task<ZoomitCustomerSearchResponse> Create(ClientAccessToken token, Guid supplierId, ZoomitCustomerSearch zoomitCustomerSearch, CancellationToken? cancellationToken = null)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (zoomitCustomerSearch is null)
                throw new ArgumentNullException(nameof(zoomitCustomerSearch));

            var payload = new JsonApi.Data<ZoomitCustomerSearch, object, object, object>
            {
                Type = "zoomitCustomerSearch",
                Attributes = zoomitCustomerSearch
            };

            return InternalCreate(token, new[] { supplierId }, payload, null, cancellationToken);
        }
    }

    /// <summary>
    /// <p>This endpoint allows you to search for a customer on the Zoomit network. Based on the response you know whether the customer is reachable on Zoomit or not.</p>
    /// <p>A list of test receivers can be found in <see href="https://documentation.ibanity.com/einvoicing/products#development-resources">Development Resources</see></p>
    /// </summary>
    public interface IZoomitCustomerSearches
    {
        /// <summary>
        /// Create Supplier
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="supplierId">Supplier ID</param>
        /// <param name="zoomitCustomerSearch">An object representing a new Zoomit Customer search</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created supplier resource</returns>
        Task<ZoomitCustomerSearchResponse> Create(ClientAccessToken token, Guid supplierId, ZoomitCustomerSearch zoomitCustomerSearch, CancellationToken? cancellationToken = null);
    }
}
