using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <inheritdoc />
    public class Integrations : IIntegrations
    {
        private readonly IApiClient _apiClient;
        private readonly IClientAccessTokenProvider _accessTokenProvider;
        private readonly string _urlPrefix;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Integrations(IApiClient apiClient, IClientAccessTokenProvider accessTokenProvider, string urlPrefix)
        {
            _apiClient = apiClient;
            _accessTokenProvider = accessTokenProvider;
            _urlPrefix = urlPrefix;
        }

        /// <inheritdoc />
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

    /// <summary>
    /// This endpoint provides an alternative method to revoke the integration (in addition to the revoke refresh token endpoint). This endpoint remains accessible with a client access token, even if your refresh token is lost or expired.
    /// </summary>
    public interface IIntegrations
    {
        /// <summary>
        /// Revoke the integration.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="organizationId">Corresponding organization ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The deleted integration resource</returns>
        Task<Integration> Delete(ClientAccessToken token, Guid organizationId, CancellationToken? cancellationToken = null);
    }
}
