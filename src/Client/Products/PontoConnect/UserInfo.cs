using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class UserInfo : IUserInfo
    {
        private const string EntityName = "userinfo";

        private readonly IApiClient _apiClient;
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly string _urlPrefix;

        public UserInfo(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix)
        {
            _apiClient = apiClient;
            _accessTokenProvider = accessTokenProvider;
            _urlPrefix = urlPrefix;
        }

        public async Task<Models.UserInfo> Get(Token token, CancellationToken? cancellationToken) =>
            await _apiClient.Get<Models.UserInfo>(
                $"{_urlPrefix}/{EntityName}",
                (await _accessTokenProvider.RefreshToken(token ?? throw new ArgumentNullException(nameof(token)))).AccessToken,
                cancellationToken ?? CancellationToken.None);
    }

    public interface IUserInfo
    {
        Task<Models.UserInfo> Get(Token token, CancellationToken? cancellationToken = null);
    }
}
