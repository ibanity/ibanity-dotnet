using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.IsabelConnect
{
    /// <summary>
    /// Contains services for all Ponto Connect-releated resources.
    /// </summary>
    public class IsabelConnectClient : ProductClient, IIsabelConnectClient
    {
        /// <summary>
        /// Product name use as prefix in Ponto Connect URIs.
        /// </summary>
        public const string UrlPrefix = "isabel-connect";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="tokenService">Service to generate and refresh access tokens</param>
        /// <param name="clientAccessTokenService">Service to generate and refresh client access tokens.</param>
        public IsabelConnectClient(IApiClient apiClient, ITokenProvider tokenService, IClientAccessTokenProvider clientAccessTokenService)
            : base(apiClient, tokenService, clientAccessTokenService)
        {
        }
    }

    /// <summary>
    /// Contains services for all Ponto Connect-releated resources.
    /// </summary>
    public interface IIsabelConnectClient : IProductClient
    {
    }
}
