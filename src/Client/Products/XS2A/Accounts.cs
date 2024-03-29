using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="IAccounts" />
    public class Accounts : ResourceWithParentClient<AccountResponse, AccountMeta, object, object, CustomerAccessToken>, IAccounts
    {
        private const string ParentEntityName = "customer/financial-institutions";
        private const string EntityName = "accounts";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Accounts(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<IbanityCollection<AccountResponse>> List(CustomerAccessToken token, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null) =>
            InternalCursorBasedList(
                token,
                $"{UrlPrefix}/customer/accounts",
                null,
                pageLimit,
                pageBefore,
                pageAfter,
                cancellationToken);

        /// <inheritdoc />
        public Task<IbanityCollection<AccountResponse>> ListForFinancialInstitution(CustomerAccessToken token, Guid financialInstitutionId, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null) =>
            InternalCursorBasedList(token, new[] { financialInstitutionId }, null, pageLimit, pageBefore, pageAfter, cancellationToken);

        /// <inheritdoc />
        public Task<IbanityCollection<AccountResponse>> ListForAccountInformationAccessRequest(CustomerAccessToken token, Guid financialInstitutionId, Guid accountInformationAccessRequestId, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null) =>
            InternalCursorBasedList(
                token,
                $"{UrlPrefix}/customer/financial-institutions/{financialInstitutionId}/account-information-access-requests/{accountInformationAccessRequestId}/accounts",
                null,
                pageLimit,
                pageBefore,
                pageAfter,
                cancellationToken);

        /// <inheritdoc />
        public Task<AccountResponse> Get(CustomerAccessToken token, Guid financialInstitutionId, Guid id, CancellationToken? cancellationToken = null) =>
            InternalGet(token, new[] { financialInstitutionId }, id, cancellationToken);

        /// <inheritdoc />
        public Task Delete(CustomerAccessToken token, Guid financialInstitutionId, Guid id, CancellationToken? cancellationToken = null) =>
            InternalDelete(token, new[] { financialInstitutionId }, id, cancellationToken);

        /// <inheritdoc />
        protected override AccountResponse Map(JsonApi.Data<AccountResponse, AccountMeta, object, object> data)
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
    /// <p>This is an object representing a customer account. This object will provide details about the account, including the balance and the currency.</p>
    /// <p>An account has related transactions and belongs to a financial institution.</p>
    /// <p>The account API endpoints are customer specific and therefore can only be accessed by providing the corresponding customer access token.</p>
    /// </summary>
    public interface IAccounts
    {
        /// <summary>
        /// List Accounts
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="pageLimit">Maximum number (1-100) of resources that might be returned. It is possible that the response contains fewer elements. Defaults to 10</param>
        /// <param name="pageBefore">Cursor for pagination. Indicates that the API should return the synchronization resources which are immediately before this one in the list (the previous page)</param>
        /// <param name="pageAfter">Cursor for pagination. Indicates that the API should return the synchronization resources which are immediately after this one in the list (the next page)</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of account resources</returns>
        Task<IbanityCollection<AccountResponse>> List(CustomerAccessToken token, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Accounts for Financial Institution
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="pageLimit">Maximum number (1-100) of resources that might be returned. It is possible that the response contains fewer elements. Defaults to 10</param>
        /// <param name="pageBefore">Cursor for pagination. Indicates that the API should return the synchronization resources which are immediately before this one in the list (the previous page)</param>
        /// <param name="pageAfter">Cursor for pagination. Indicates that the API should return the synchronization resources which are immediately after this one in the list (the next page)</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of account resources</returns>
        Task<IbanityCollection<AccountResponse>> ListForFinancialInstitution(CustomerAccessToken token, Guid financialInstitutionId, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Accounts for Account Information Access Request
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="accountInformationAccessRequestId">Account information access request ID</param>
        /// <param name="pageLimit">Maximum number (1-100) of resources that might be returned. It is possible that the response contains fewer elements. Defaults to 10</param>
        /// <param name="pageBefore">Cursor for pagination. Indicates that the API should return the synchronization resources which are immediately before this one in the list (the previous page)</param>
        /// <param name="pageAfter">Cursor for pagination. Indicates that the API should return the synchronization resources which are immediately after this one in the list (the next page)</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of account resources</returns>
        Task<IbanityCollection<AccountResponse>> ListForAccountInformationAccessRequest(CustomerAccessToken token, Guid financialInstitutionId, Guid accountInformationAccessRequestId, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Account
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="id">Account ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The specified account resource or 404 if not found</returns>
        Task<AccountResponse> Get(CustomerAccessToken token, Guid financialInstitutionId, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Delete Account
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="id">Account ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <remarks>All account details and transactions have been destroyed. In the case that the customer would add this account again later, the account id and the transaction ids will be different since they will be considered completely new resources.</remarks>
        Task Delete(CustomerAccessToken token, Guid financialInstitutionId, Guid id, CancellationToken? cancellationToken = null);
    }
}
