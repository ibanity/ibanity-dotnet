using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc />
    public class Sandbox : ISandbox
    {
        /// <summary>
        /// Product name used as prefix in sandbox URIs.
        /// </summary>
        public const string UrlPrefix = "sandbox";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        public Sandbox(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider)
        {
        }
    }

    /// <summary>
    /// Fake accounts and transactions.
    /// </summary>
    public interface ISandbox
    {
    }
}
