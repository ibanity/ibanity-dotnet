using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.eInvoicing.Models;

namespace Ibanity.Apis.Client.Products.eInvoicing
{
    /// <inheritdoc />
    public class Suppliers : ResourceClient<SupplierResponse, object, object, object, ClientAccessToken>, ISuppliers
    {
        private const string EntityName = "suppliers";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Suppliers(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) : base(apiClient, accessTokenProvider, urlPrefix, EntityName, false)
        { }

        /// <inheritdoc />
        public Task<SupplierResponse> Get(ClientAccessToken token, Guid id, CancellationToken? cancellationToken = null) =>
            InternalGet(token, id, cancellationToken);

        /// <inheritdoc />
        public Task<SupplierResponse> Create(ClientAccessToken token, NewSupplier supplier, CancellationToken? cancellationToken = null)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (supplier is null)
                throw new ArgumentNullException(nameof(supplier));

            var payload = new JsonApi.Data<NewSupplier, object, object, object>
            {
                Type = "supplier",
                Attributes = supplier
            };

            return InternalCreate(token, payload, null, cancellationToken);
        }

        /// <inheritdoc />
        public Task<SupplierResponse> Update(ClientAccessToken token, Guid id, Supplier supplier, CancellationToken? cancellationToken = null)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (supplier is null)
                throw new ArgumentNullException(nameof(supplier));

            var payload = new JsonApi.Data<Supplier, object, object, object>
            {
                Id = id.ToString(),
                Type = "supplier",
                Attributes = supplier
            };

            return InternalUpdate(token, id, payload, null, cancellationToken);
        }

        /// <inheritdoc />
        public Task Delete(ClientAccessToken token, Guid id, CancellationToken? cancellationToken = null) =>
            InternalDelete(token, id, cancellationToken);
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
        Task<SupplierResponse> Get(ClientAccessToken token, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Create Supplier
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="supplier">An object representing a new supplier</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created supplier resource</returns>
        Task<SupplierResponse> Create(ClientAccessToken token, NewSupplier supplier, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Update Supplier
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="id">Financial institution transaction ID</param>
        /// <param name="supplier">An object representing a supplier</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The updated supplier resource</returns>
        Task<SupplierResponse> Update(ClientAccessToken token, Guid id, Supplier supplier, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Delete Supplier
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="id">Supplier ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        Task Delete(ClientAccessToken token, Guid id, CancellationToken? cancellationToken = null);
    }
}
