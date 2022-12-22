using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.IsabelConnect
{
    /// <summary>
    /// <para>This is an object representing an intraday account transaction. This object will give you the details of the intraday transaction, including its amount and execution date.</para>
    /// <para>At the end of the day, intraday transactions will be converted to transactions by the financial institution. The transactions will never be available as transactions and intraday transactions at the same time.</para>
    /// <para>Important: The ID of the intraday transaction will NOT be the same as the ID of the corresponding transaction.</para>
    /// </summary>
    public class IntradayTransactions : ResourceWithParentClient<IntradayTransaction, object, object, object, string, string, Token>, IIntradayTransactions
    {
        private const string ParentEntityName = "accounts";
        private const string EntityName = "intraday-transactions";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public IntradayTransactions(IApiClient apiClient, IAccessTokenProvider<Token> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<IsabelCollection<IntradayTransaction>> List(Token token, string accountId, long? pageOffset = null, int? pageSize = null, CancellationToken? cancellationToken = null) =>
            InternalOffsetBasedList(token, new[] { accountId }, null, null, pageOffset, pageSize, cancellationToken);

        /// <inheritdoc />
        protected override string ParseId(string id) => id;
    }

    /// <summary>
    /// <para>This is an object representing an intraday account transaction. This object will give you the details of the intraday transaction, including its amount and execution date.</para>
    /// <para>At the end of the day, intraday transactions will be converted to transactions by the financial institution. The transactions will never be available as transactions and intraday transactions at the same time.</para>
    /// <para>Important: The ID of the intraday transaction will NOT be the same as the ID of the corresponding transaction.</para>
    /// </summary>
    public interface IIntradayTransactions
    {
        /// <summary>
        /// List intraday transactions.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Customer's account ID</param>
        /// <param name="pageOffset">Defines the start position of the results by giving the number of records to be skipped</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of transaction resources</returns>
        Task<IsabelCollection<IntradayTransaction>> List(Token token, string accountId, long? pageOffset = null, int? pageSize = null, CancellationToken? cancellationToken = null);
    }
}
