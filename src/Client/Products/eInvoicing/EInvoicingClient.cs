using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.eInvoicing
{
    /// <summary>
    /// Contains services for all eInvoicing-releated resources.
    /// </summary>
    public class EInvoicingClient : ProductClient<ITokenProviderWithoutCodeVerifier>, IEInvoicingClient
    {
        /// <summary>
        /// Product name use as prefix in eInvoicing URIs.
        /// </summary>
        public const string UrlPrefix = "einvoicing";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="tokenService">Service to generate and refresh access tokens</param>
        /// <param name="clientAccessTokenService">Service to generate and refresh client access tokens.</param>
        public EInvoicingClient(IApiClient apiClient, ITokenProviderWithoutCodeVerifier tokenService, IClientAccessTokenProvider clientAccessTokenService)
            : base(apiClient, tokenService, clientAccessTokenService)
        {
            Suppliers = new Suppliers(apiClient, clientAccessTokenService, UrlPrefix);
        }

        /// <inheritdoc />
        public ISuppliers Suppliers { get; }
    }

    /// <summary>
    /// Contains services for all eInvoicing-related resources.
    /// </summary>
    public interface IEInvoicingClient : IProductClient
    {
        /// <summary>
        /// This resource allows a Software Partner to create a new Supplier.
        /// </summary>
        ISuppliers Suppliers { get; }
    }
}
