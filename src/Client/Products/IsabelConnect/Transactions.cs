using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.IsabelConnect
{
    /// <summary>
    /// <para>This is an object representing an account transaction. This object will give you the details of the financial transaction, including its amount and execution date.</para>
    /// <para>Unlike an <see cref="IntradayTransaction" />, this is an end-of-day object which will not change.</para>
    /// </summary>
    public class Transactions : ResourceWithParentClient<Transaction, object, object, object, string, Guid>, ITransactions
    {
        private const string ParentEntityName = "accounts";
        private const string EntityName = "transactions";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Transactions(IApiClient apiClient, IAccessTokenProvider<Token> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<IsabelCollection<Transaction>> List(Token token, string accountId, DateTimeOffset? from = null, DateTimeOffset? to = null, long? pageOffset = null, int? pageSize = null, CancellationToken? cancellationToken = null)
        {
            var timespanParameters = new List<(string, string)>();

            if (from.HasValue)
                timespanParameters.Add(("from", from.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)));

            if (to.HasValue)
                timespanParameters.Add(("to", to.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)));

            return InternalOffsetBasedList(token, new[] { accountId }, null, timespanParameters, pageOffset, pageSize, cancellationToken);
        }

        /// <inheritdoc />
        protected override Guid ParseId(string id) => Guid.Parse(id);
    }

    /// <summary>
    /// <para>This is an object representing an account transaction. This object will give you the details of the financial transaction, including its amount and execution date.</para>
    /// <para>Unlike an <see cref="IntradayTransaction" />, this is an end-of-day object which will not change.</para>
    /// </summary>
    public interface ITransactions
    {
        /// <summary>
        /// List Transactions
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Customer's account ID</param>
        /// <param name="from">Start of the period scope, in the ISO8601 format YYYY-MM-DD. Mandatory if to is present. Defaults to today's date. Must be within 365 days of today's date and within 200 days of the to date.</param>
        /// <param name="to">End of the period scope, in the ISO8601 format YYYY-MM-DD. Must be equal to or later than from value. Defaults to today's date.</param>
        /// <param name="pageOffset">Defines the start position of the results by giving the number of records to be skipped</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of transaction resources</returns>
        Task<IsabelCollection<Transaction>> List(Token token, string accountId, DateTimeOffset? from = null, DateTimeOffset? to = null, long? pageOffset = null, int? pageSize = null, CancellationToken? cancellationToken = null);
    }
}
