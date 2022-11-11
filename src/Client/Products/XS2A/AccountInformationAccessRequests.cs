using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="IAccountInformationAccessRequests" />
    public class AccountInformationAccessRequests : ResourceWithParentClient<AccountInformationAccessRequestResponse, AccountInformationAccessRequestMeta, object, AccountInformationAccessRequestLinks, CustomerAccessToken>, IAccountInformationAccessRequests
    {
        private const string ParentEntityName = "customer/financial-institutions";
        private const string EntityName = "account-information-access-requests";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public AccountInformationAccessRequests(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<AccountInformationAccessRequestResponse> Create(CustomerAccessToken token, Guid financialInstitutionsId, AccountInformationAccessRequestRequest accountInformationAccessRequest, int? requestedPastTransactionDays = null, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (accountInformationAccessRequest is null)
                throw new ArgumentNullException(nameof(accountInformationAccessRequest));

            var payload = new JsonApi.Data<AccountInformationAccessRequestRequest, AccountInformationAccessRequestMeta, object, object>
            {
                Type = "accountInformationAccessRequest",
                Attributes = accountInformationAccessRequest,
                Meta = new AccountInformationAccessRequestMeta
                {
                    RequestedPastTransactionDays = requestedPastTransactionDays
                }
            };

            return InternalCreate(token, new[] { financialInstitutionsId }, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<AccountInformationAccessRequestResponse> Get(CustomerAccessToken token, Guid financialInstitutionsId, Guid id, CancellationToken? cancellationToken = null) =>
            InternalGet(token, new[] { financialInstitutionsId }, id, cancellationToken);

        /// <inheritdoc />
        protected override AccountInformationAccessRequestResponse Map(Data<AccountInformationAccessRequestResponse, AccountInformationAccessRequestMeta, object, AccountInformationAccessRequestLinks> data)
        {
            var result = base.Map(data);

            result.Redirect = data.Links?.Redirect;

            return result;
        }
    }

    /// <summary>
    /// <p>This is an object representing an account information access request. When you want to access the account information of one of your customers, you have to create one to start the authorization flow.</p>
    /// <p>When creating the account information access request, you will receive the redirect link in the response. Your customer should be redirected to this url to begin the authorization process. At the end of the flow, they will be returned to the redirect uri that you defined.</p>
    /// <p>If the access request is not authorized (for example when the customer cancels the flow), an error query parameter will be added to the redirect uri. The possible values of this parameter are access_denied and unsupported_multi_currency_account.</p>
    /// <p>When authorizing account access by a financial institution user (in the sandbox), you should use 123456 as the digipass response. You can also use the Ibanity Sandbox Authorization Portal CLI to automate this authorization.</p>
    /// </summary>
    public interface IAccountInformationAccessRequests
    {
        /// <summary>
        /// Create account information access request
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionsId">Financial institution ID</param>
        /// <param name="accountInformationAccessRequest">Details of the account information access request</param>
        /// <param name="requestedPastTransactionDays">Optional number of days to fetch past transactions. Default is 90</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created account information access request resource</returns>
        Task<AccountInformationAccessRequestResponse> Create(CustomerAccessToken token, Guid financialInstitutionsId, AccountInformationAccessRequestRequest accountInformationAccessRequest, int? requestedPastTransactionDays = null, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get account information access request
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionsId">Financial institution ID</param>
        /// <param name="id">Account information access request ID</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created account information access request resource</returns>
        Task<AccountInformationAccessRequestResponse> Get(CustomerAccessToken token, Guid financialInstitutionsId, Guid id, CancellationToken? cancellationToken = null);
    }
}
