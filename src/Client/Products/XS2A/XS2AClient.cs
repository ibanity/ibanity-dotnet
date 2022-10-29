using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="IXS2AClient" />
    public class XS2AClient : ProductClient<ITokenProviderWithoutCodeVerifier>, IXS2AClient
    {
        /// <summary>
        /// Product name used as prefix in XS2A URIs.
        /// </summary>
        public const string UrlPrefix = "xs2a";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="tokenService">Service to generate and refresh access tokens</param>
        /// <param name="clientAccessTokenService">Service to generate and refresh client access tokens.</param>
        /// <param name="customerTokenService">Service to generate and refresh customer access tokens.</param>
        public XS2AClient(IApiClient apiClient, ITokenProviderWithoutCodeVerifier tokenService, IClientAccessTokenProvider clientAccessTokenService, ICustomerAccessTokenProvider customerTokenService)
            : base(apiClient, tokenService, clientAccessTokenService, customerTokenService)
        {
        }
    }

    /// <summary>
    /// Contains services for all XS2A-related resources.
    /// </summary>
    public interface IXS2AClient : IProductClientWithCustomerAccessToken
    {
    }
}
