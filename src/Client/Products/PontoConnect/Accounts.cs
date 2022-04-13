using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Models;
using Ibanity.Apis.Client.Products.PontoConnect.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <summary>
    /// <para>This is an object representing a user's account. This object will provide details about the account, including the balance and the currency.</para>
    /// <para>An account has related transactions and belongs to a financial institution.</para>
    /// <para>An account may be revoked from an integration using the revoke account endpoint. To recover access, the user must add the account back to the integration in their Ponto Dashboard or in a new authorization flow.</para>
    /// </summary>
    public class Accounts : ResourceClient<AccountResponse, AccountMeta, object, object>, IAccounts
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
        public Task<PaginatedCollection<AccountResponse>> List(Token token, IEnumerable<Filter> filters, int? pageSize, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                filters,
                pageSize,
                cancellationToken);

        /// <inheritdoc />
        public Task<PaginatedCollection<AccountResponse>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken) =>
            InternalList(
                token ?? throw new ArgumentNullException(nameof(token)),
                continuationToken ?? throw new ArgumentNullException(nameof(continuationToken)),
                cancellationToken);

        /// <inheritdoc />
        public Task<AccountResponse> Get(Token token, Guid id, CancellationToken? cancellationToken) =>
            InternalGet(token, id, cancellationToken);

        /// <inheritdoc />
        public Task Revoke(Token token, Guid id, CancellationToken? cancellationToken = null) =>
            InternalDelete(token, id, cancellationToken);

        /// <inheritdoc />
        protected override AccountResponse Map(Data<AccountResponse, AccountMeta, object, object> data)
        {
            var result = base.Map(data);

            result.SynchronizedAt = data.Meta.SynchronizedAt;
            result.Availability = data.Meta.Availability;

            result.LatestSynchronization = data.Meta.LatestSynchronization.Attributes;
            result.LatestSynchronization.Id = Guid.Parse(data.Meta.LatestSynchronization.Id);

            return result;
        }
    }

    /// <summary>
    /// <para>This is an object representing a user's account. This object will provide details about the account, including the balance and the currency.</para>
    /// <para>An account has related transactions and belongs to a financial institution.</para>
    /// <para>An account may be revoked from an integration using the revoke account endpoint. To recover access, the user must add the account back to the integration in their Ponto Dashboard or in a new authorization flow.</para>
    /// </summary>
    public interface IAccounts
    {
        /// <summary>
        /// List Accounts
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="filters">Attributes to be filtered from the results</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of account resources</returns>
        Task<PaginatedCollection<AccountResponse>> List(Token token, IEnumerable<Filter> filters = null, int? pageSize = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Accounts
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="continuationToken">Token referencing the page to request</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of account resources</returns>
        Task<PaginatedCollection<AccountResponse>> List(Token token, ContinuationToken continuationToken, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Account
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="id">Account ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The specified account resource</returns>
        Task<AccountResponse> Get(Token token, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Revoke Account
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="id">Account ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        Task Revoke(Token token, Guid id, CancellationToken? cancellationToken = null);
    }
}
