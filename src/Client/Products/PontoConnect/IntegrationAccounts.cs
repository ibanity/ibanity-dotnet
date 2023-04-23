using System;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <inheritdoc />
    public class IntegrationAccounts : ResourceClient<IntegrationAccount, object, IntegrationAccountRelationships, object, ClientAccessToken>, IIntegrationAccounts
    {
        private readonly IApiClient _apiClient;
        private readonly IClientAccessTokenProvider _accessTokenProvider;
        private readonly string _urlPrefix;

        private const string EntityName = "integration-accounts";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public IntegrationAccounts(IApiClient apiClient, IClientAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        {
            _apiClient = apiClient;
            _accessTokenProvider = accessTokenProvider;
            _urlPrefix = urlPrefix;
        }

        /// <inheritdoc />
        protected override IntegrationAccount Map(Data<IntegrationAccount, object, IntegrationAccountRelationships, object> data)
        {
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            var result = data.Attributes ?? new IntegrationAccount();

            result.Id = Guid.Parse(data.Id);
            result.AccountId = Guid.Parse(data.Relationships.Organization.Data.Id);
            result.FinancialInstitutionId = Guid.Parse(data.Relationships.Organization.Data.Id);
            result.OrganizationId = Guid.Parse(data.Relationships.Organization.Data.Id);

            return result;
        }
    }

    /// <summary>
    /// <p>This is an object representing the link between a user's account and an integration.</p>
    /// <p>All accounts linked to your Ponto Connect application are returned by this endpoint.</p>
    /// </summary>
    public interface IIntegrationAccounts
    {
    }
}
