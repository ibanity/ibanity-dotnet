using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class Integrations : IIntegrations
    {
        private readonly IApiClient _apiClient;
        private readonly IClientAccessTokenProvider _accessTokenProvider;
        private readonly string _urlPrefix;

        public Integrations(IApiClient apiClient, IClientAccessTokenProvider accessTokenProvider, string urlPrefix)
        {
            _apiClient = apiClient;
            _accessTokenProvider = accessTokenProvider;
            _urlPrefix = urlPrefix;
        }

        public async Task<Integration> Delete(ClientAccessToken token, Guid organizationId, CancellationToken? cancellationToken) =>
            Map(await _apiClient.Delete<JsonApi.Data<Integration, object, IntegrationRelationships, object>>(
                $"{_urlPrefix}/organizations/{organizationId}/integration",
                (await _accessTokenProvider.RefreshToken(token ?? throw new ArgumentNullException(nameof(token)))).AccessToken,
                cancellationToken ?? CancellationToken.None));

        private Integration Map(JsonApi.Data<Integration, object, IntegrationRelationships, object> data)
        {
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            var result = data.Attributes;

            result.Id = Guid.Parse(data.Id);
            result.OrganizationId = Guid.Parse(data.Relationships.Organization.Data.Id);

            return result;
        }
    }

    public interface IIntegrations
    {
        Task<Integration> Delete(ClientAccessToken token, Guid organizationId, CancellationToken? cancellationToken = null);
    }
}
