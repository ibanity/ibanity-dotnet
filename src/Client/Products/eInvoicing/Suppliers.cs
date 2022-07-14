using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.eInvoicing.Models;

namespace Ibanity.Apis.Client.Products.eInvoicing
{
    /// <inheritdoc />
    public class Suppliers : ResourceClient<Supplier, object, object, object, ClientAccessToken>, ISuppliers
    {
        private const string EntityName = "suppliers";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Suppliers(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) : base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        public Task<Supplier> Get(ClientAccessToken token, Guid id, CancellationToken? cancellationToken = null) =>
            InternalGet(token, id, cancellationToken);
    }

    /// <summary>
    /// This resource allows a Software Partner to create a new Supplier.
    /// </summary>
    public interface ISuppliers
    {
        /// <summary>
        /// Get Supplier
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="id">Supplier ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a supplier resource.</returns>
        Task<Supplier> Get(ClientAccessToken token, Guid id, CancellationToken? cancellationToken = null);
    }
}
