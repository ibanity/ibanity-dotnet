using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.IsabelConnect
{
    /// <summary>
    /// <para>This object provides details about an account report. From the list endpoint, you will receive a collection of the account report objects for the corresponding customer.</para>
    /// <para>Unlike other endpoints, the get endpoint will return the contents of the account report file instead of a json object. You can also find a link to the report in the account report object links.</para>
    /// </summary>
    public class AccountReports : ResourceClient<AccountReport, object, object, object, string>, IAccountReports
    {
        private const string EntityName = "account-reports";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public AccountReports(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        public Task<IsabelCollection<AccountReport>> List(Token token, long? pageOffset = null, int? pageSize = null, string after = null, CancellationToken? cancellationToken = null) =>
            InternalOffsetBasedList(
                token,
                null,
                string.IsNullOrWhiteSpace(after) ? null : new[] { ("after", after) },
                pageOffset,
                pageSize,
                cancellationToken);

        /// <inheritdoc />
        protected override string ParseId(string id) => id;
    }

    /// <summary>
    /// <para>This object provides details about an account report. From the list endpoint, you will receive a collection of the account report objects for the corresponding customer.</para>
    /// <para>Unlike other endpoints, the get endpoint will return the contents of the account report file instead of a json object. You can also find a link to the report in the account report object links.</para>
    /// </summary>
    public interface IAccountReports
    {
        /// <summary>
        /// List intraday transactions.
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="pageOffset">Defines the start position of the results by giving the number of records to be skipped</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="after">Identifier of an account report that serves as a cursor, limiting results to account reports which have been received and processed after it. In practice, you can provide your last-processed ID to retrieve only the newer account reports.</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of transaction resources</returns>
        Task<IsabelCollection<AccountReport>> List(Token token, long? pageOffset = null, int? pageSize = null, string after = null, CancellationToken? cancellationToken = null);
    }
}
