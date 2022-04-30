using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <inheritdoc />
    public class Usages : IUsages
    {
        private static readonly Regex MonthPattern = new Regex(
            @"^(?<year>\d{4})-(?<month>\d{2})$",
            RegexOptions.Compiled
        );

        private readonly IApiClient _apiClient;
        private readonly IClientAccessTokenProvider _accessTokenProvider;
        private readonly string _urlPrefix;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Usages(IApiClient apiClient, IClientAccessTokenProvider accessTokenProvider, string urlPrefix)
        {
            _apiClient = apiClient;
            _accessTokenProvider = accessTokenProvider;
            _urlPrefix = urlPrefix;
        }

        /// <inheritdoc />
        public async Task<Usage> Get(ClientAccessToken token, Guid organizationId, int year, int month, CancellationToken? cancellationToken) =>
            Map(await _apiClient.Get<JsonApi.Data<Usage, object, UsageRelationships, object>>(
                $"{_urlPrefix}/organizations/{organizationId}/usage/{year:D4}-{month:D2}",
                (await _accessTokenProvider.RefreshToken(token ?? throw new ArgumentNullException(nameof(token)))).AccessToken,
                cancellationToken ?? CancellationToken.None));

        private static Usage Map(JsonApi.Data<Usage, object, UsageRelationships, object> data)
        {
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            var result = data.Attributes;

            var (year, month) = ParseMonth(data.Id);

            result.Year = year;
            result.Month = month;

            result.OrganizationId = Guid.Parse(data.Relationships.Organization.Data.Id);

            return result;
        }

        private static (int, int) ParseMonth(string month)
        {
            var match = MonthPattern.Match(month);
            if (!match.Success)
                throw new IbanityException("Invalid month: " + month);

            return (
                int.Parse(match.Groups["year"].Value, CultureInfo.InvariantCulture),
                int.Parse(match.Groups["month"].Value, CultureInfo.InvariantCulture)
            );
        }
    }

    /// <summary>
    /// This endpoint provides the usage of your integration by the provided organization during a given month. In order to continue to allow access to this information if an integration is revoked, you must use a client access token for this endpoint.
    /// </summary>
    public interface IUsages
    {
        /// <summary>
        /// Get Organization Usage
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="organizationId">Corresponding organization ID</param>
        /// <param name="year">Year to get usage for</param>
        /// <param name="month">Month to get usage for</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The account and payment usage of the organization's integration for the provided month</returns>
        Task<Usage> Get(ClientAccessToken token, Guid organizationId, int year, int month, CancellationToken? cancellationToken = null);
    }
}
