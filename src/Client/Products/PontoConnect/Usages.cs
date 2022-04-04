using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class Usages : IUsages
    {
        private static readonly Regex MonthPattern = new Regex(
            @"^(?<year>\d{4})-(?<month>\d{2})$",
            RegexOptions.Compiled
        );

        private readonly IApiClient _apiClient;
        private readonly IClientAccessTokenProvider _accessTokenProvider;
        private readonly string _urlPrefix;

        public Usages(IApiClient apiClient, IClientAccessTokenProvider accessTokenProvider, string urlPrefix)
        {
            _apiClient = apiClient;
            _accessTokenProvider = accessTokenProvider;
            _urlPrefix = urlPrefix;
        }

        public async Task<Usage> Get(ClientAccessToken token, Guid organizationId, int year, int month, CancellationToken? cancellationToken) =>
            Map(await _apiClient.Get<JsonApi.Data<Usage, object, UsageRelationships, object>>(
                $"{_urlPrefix}/organizations/{organizationId}/usage/{year}-{month}",
                (await _accessTokenProvider.RefreshToken(token ?? throw new ArgumentNullException(nameof(token)))).AccessToken,
                cancellationToken ?? CancellationToken.None));

        private Usage Map(JsonApi.Data<Usage, object, UsageRelationships, object> data)
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

        private (int, int) ParseMonth(string month)
        {
            var match = MonthPattern.Match(month);
            if (!match.Success)
                throw new IbanityException("Invalid month: " + month);

            return (
                int.Parse(match.Groups["year"].Value),
                int.Parse(match.Groups["month"].Value)
            );
        }
    }

    public interface IUsages
    {
        Task<Usage> Get(ClientAccessToken token, Guid organizationId, int year, int month, CancellationToken? cancellationToken = null);
    }
}
