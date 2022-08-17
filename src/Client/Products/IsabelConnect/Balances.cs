using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.IsabelConnect
{
    /// <summary>
    /// This is an object representing a balance related to a customer's account.
    /// </summary>
    public class Balances : ResourceWithParentClient<BalanceWithFakeId, object, object, object, string, string>, IBalances
    {
        private const string ParentEntityName = "accounts";
        private const string EntityName = "balances";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Balances(IApiClient apiClient, IAccessTokenProvider<Token> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public async Task<IsabelCollection<Balance>> List(Token token, string accountId, DateTimeOffset? from = null, DateTimeOffset? to = null, long? pageOffset = null, int? pageSize = null, CancellationToken? cancellationToken = null)
        {
            var timespanParameters = new List<(string, string)>();

            if (from.HasValue)
                timespanParameters.Add(("from", from.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)));

            if (to.HasValue)
                timespanParameters.Add(("to", to.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)));

            var result = await InternalOffsetBasedList(token, new[] { accountId }, null, timespanParameters, pageOffset, pageSize, cancellationToken);

            return new IsabelCollection<Balance>
            {
                Offset = result.Offset,
                Total = result.Total,
                Items = result.Items.Select(b => new Balance(b)).ToList(),
                ContinuationToken = result.ContinuationToken
            };
        }

        /// <inheritdoc />
        protected override string ParseId(string id) => id;
    }

    /// <summary>
    /// This is an object representing a balance related to a customer's account.
    /// </summary>
    public interface IBalances
    {
        /// <summary>
        /// List Accounts Balances
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Customer's account ID</param>
        /// <param name="from">Start of the period scope. Mandatory if to is present. If not provided, the most recent balance of each type is returned.</param>
        /// <param name="to">End of the period scope. Must be equal to or later than from value. Defaults to today's date.</param>
        /// <param name="pageOffset">Defines the start position of the results by giving the number of records to be skipped</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of balance resources</returns>
        /// <remarks>The range may contain maximum 31 calendar days (e.g. 2020-10-01-2020-10-31).</remarks>
        Task<IsabelCollection<Balance>> List(Token token, string accountId, DateTimeOffset? from = null, DateTimeOffset? to = null, long? pageOffset = null, int? pageSize = null, CancellationToken? cancellationToken = null);
    }
}
