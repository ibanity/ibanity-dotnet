using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.CodaboxConnect.Models;

namespace Ibanity.Apis.Client.Products.CodaboxConnect
{
    /// <inheritdoc cref="IBankAccountStatements" />
    public class BankAccountStatements : ResourceWithParentClient<BankAccountStatement, object, object, object, string, Guid, ClientAccessToken>, IBankAccountStatements
    {
        private const string TopLevelParentEntityName = "accounting-offices";
        private const string ParentEntityName = "clients";
        private const string EntityName = "bank-account-statements";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public BankAccountStatements(IApiClient apiClient, IAccessTokenProvider<ClientAccessToken> accessTokenProvider, string urlPrefix) : base(apiClient, accessTokenProvider, urlPrefix, new[] { TopLevelParentEntityName, ParentEntityName, EntityName }, false)
        { }

        /// <inheritdoc />
        public Task<BankAccountStatement> Get(ClientAccessToken token, Guid accountingOfficeId, string clientId, Guid id, CancellationToken? cancellationToken = null) =>
            InternalGet(token, new[] { accountingOfficeId.ToString(), clientId }, id, cancellationToken);

        /// <inheritdoc />
        protected override Guid ParseId(string id) => Guid.Parse(id);
    }

    /// <summary>
    /// This resource allows an Accounting Software to retrieve a bank account statement for a client of an accounting office.
    /// </summary>
    public interface IBankAccountStatements
    {
        /// <summary>
        /// Get Bank Account Statement
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountingOfficeId">Accounting office identifier</param>
        /// <param name="clientId">Bank account statement's owner</param>
        /// <param name="id">Bank Account Statement ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>Returns a Bank Account Statement resource.</returns>
        Task<BankAccountStatement> Get(ClientAccessToken token, Guid accountingOfficeId, string clientId, Guid id, CancellationToken? cancellationToken = null);
    }
}
