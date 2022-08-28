using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <inheritdoc />
    public class UserInfo : IUserInfo
    {
        private const string EntityName = "userinfo";

        private readonly IApiClient _apiClient;
        private readonly IAccessTokenProvider<Token> _accessTokenProvider;
        private readonly string _urlPrefix;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public UserInfo(IApiClient apiClient, IAccessTokenProvider<Token> accessTokenProvider, string urlPrefix)
        {
            _apiClient = apiClient;
            _accessTokenProvider = accessTokenProvider;
            _urlPrefix = urlPrefix;
        }

        /// <inheritdoc />
        public async Task<Models.UserInfo> Get(Token token, CancellationToken? cancellationToken) =>
            await _apiClient.Get<Models.UserInfo>(
                $"{_urlPrefix}/{EntityName}",
                (await _accessTokenProvider.RefreshToken(token ?? throw new ArgumentNullException(nameof(token))).ConfigureAwait(false)).AccessToken,
                cancellationToken ?? CancellationToken.None).ConfigureAwait(false);
    }

    /// <summary>
    /// <para>This endpoint provides information about the subject (organization) of an access token. Minimally, it provides the organization's id as the token's sub. If additional organization information was requested in the scope of the authorization, it will be provided here.</para>
    /// <para>The organization's id can be used to request its usage. Keep in mind that if the access token is revoked, this endpoint will no longer be available, so you may want to store the organization's id in your system.</para>
    /// </summary>
    public interface IUserInfo
    {
        /// <summary>
        /// Get User Info
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The subject of the access token, which is the organization's id, and any other attributes authorized in the scope of the token</returns>
        Task<Models.UserInfo> Get(Token token, CancellationToken? cancellationToken = null);
    }
}
