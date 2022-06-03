using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Products.IsabelConnect.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.IsabelConnect
{
    /// <summary>
    /// <para>This is an object representing a customer account. This object will provide details about the account, including the reference and the currency.</para>
    /// <para>An account has related transactions and balances.</para>
    /// <para>The account API endpoints are customer specific and therefore can only be accessed by providing the corresponding access token.</para>
    /// </summary>
    public class Accounts : ResourceClient<Account, object, object, object, string>, IAccounts
    {
        private const string EntityName = "accounts";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Accounts(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        protected override string ParseId(string id) => id;

        /// <inheritdoc />
        protected override Account Map(Data<Account, object, object, object> data)
        {
            if (data.Attributes == null)
                data.Attributes = new Account();

            return base.Map(data);
        }

        /// <inheritdoc />
        public Task<IsabelCollection<Account>> List(Token token, long? pageOffset, int? pageSize, CancellationToken? cancellationToken) =>
            InternalOffsetBasedList(
                token ?? throw new ArgumentNullException(nameof(token)),
                null,
                null,
                pageOffset,
                pageSize,
                cancellationToken);

        /// <inheritdoc />
        public Task<IsabelCollection<Account>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalOffsetBasedList(
                token ?? throw new ArgumentNullException(nameof(token)),
                continuationToken ?? throw new ArgumentNullException(nameof(continuationToken)),
                cancellationToken);

        /// <inheritdoc />
        public Task<Account> Get(Token token, string id, CancellationToken? cancellationToken = null) =>
            InternalGet(token, id, cancellationToken);
    }

    /// <summary>
    /// <para>This is an object representing a customer account. This object will provide details about the account, including the reference and the currency.</para>
    /// <para>An account has related transactions and balances.</para>
    /// <para>The account API endpoints are customer specific and therefore can only be accessed by providing the corresponding access token.</para>
    /// </summary>
    public interface IAccounts
    {
        /// <summary>
        /// List Accounts
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="pageOffset">Defines the start position of the results by giving the number of records to be skipped</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of account resources</returns>
        Task<IsabelCollection<Account>> List(Token token, long? pageOffset = null, int? pageSize = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Accounts
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="continuationToken">Token referencing the page to request</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of account resources</returns>
        Task<IsabelCollection<Account>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Account
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="id">Account ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The specified account resource</returns>
        Task<Account> Get(Token token, string id, CancellationToken? cancellationToken = null);
    }
}
