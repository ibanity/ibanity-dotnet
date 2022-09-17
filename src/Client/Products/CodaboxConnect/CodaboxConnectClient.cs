using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.CodaboxConnect
{
    /// <inheritdoc cref="ICodaboxConnectClient" />
    public class CodaboxConnectClient : ProductClient<ITokenProviderWithoutCodeVerifier>, ICodaboxConnectClient
    {
        /// <summary>
        /// Product name used as prefix in Codabox Connect URIs.
        /// </summary>
        public const string UrlPrefix = "codabox-connect";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="tokenService">Service to generate and refresh access tokens</param>
        /// <param name="clientAccessTokenService">Service to generate and refresh client access tokens.</param>
        public CodaboxConnectClient(IApiClient apiClient, ITokenProviderWithoutCodeVerifier tokenService, IClientAccessTokenProvider clientAccessTokenService)
            : base(apiClient, tokenService, clientAccessTokenService)
        {
        }
    }

    /// <summary>
    /// Contains services for all Codabox Connect-related resources.
    /// </summary>
    public interface ICodaboxConnectClient : IProductClient
    {
    }
}
