using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.eInvoicing.Models;

namespace Ibanity.Apis.Client.Products.eInvoicing
{
    /// <inheritdoc cref="IPeppolCustomerSearches" />
    public class PeppolCustomerSearches : ResourceClient<PeppolCustomerSearchResponse, object, object, object, ClientAccessToken>, IPeppolCustomerSearches
    {
        private const string EntityName = "peppol/customer-searches";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public PeppolCustomerSearches(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) : base(apiClient, accessTokenProvider, urlPrefix, EntityName, false)
        { }

        /// <inheritdoc />
        public Task<PeppolCustomerSearchResponse> Create(ClientAccessToken token, PeppolCustomerSearch peppolCustomerSearch, CancellationToken? cancellationToken = null)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (peppolCustomerSearch is null)
                throw new ArgumentNullException(nameof(peppolCustomerSearch));

            var payload = new JsonApi.Data<PeppolCustomerSearch, object, object, object>
            {
                Type = "peppolCustomerSearch",
                Attributes = peppolCustomerSearch
            };

            return InternalCreate(token, payload, null, cancellationToken);
        }
    }

    /// <summary>
    /// <p>This endpoint allows you to search for a customer on the PEPPOL network and the document types it supports. Based on the response you know:</p>
    /// <p>- whether the customer is available on Peppol and is capable to receive documents over Peppol</p>
    /// <p>- which UBL document types the customer is capable to receive</p>
    /// <p>A list of test receivers can be found in <see href="https://documentation.ibanity.com/einvoicing/products#development-resources">Development Resources</see></p>
    /// </summary>
    public interface IPeppolCustomerSearches
    {
        /// <summary>
        /// Create Peppol Customer search
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="peppolCustomerSearch">An object representing a new Peppol Customer search</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created Peppol Customer search resource</returns>
        Task<PeppolCustomerSearchResponse> Create(ClientAccessToken token, PeppolCustomerSearch peppolCustomerSearch, CancellationToken? cancellationToken = null);
    }
}
