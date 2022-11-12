using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Products.XS2A.Models;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="IAccountInformationAccessRequestAuthorizations" />
    public class AccountInformationAccessRequestAuthorizations : ResourceWithParentClient<AccountInformationAccessRequestAuthorizationResponse, object, object, AccountInformationAccessRequestAuthorizationLinks, CustomerAccessToken>, IAccountInformationAccessRequestAuthorizations
    {
        private const string GrandParentEntityName = "customer/financial-institutions";
        private const string ParentEntityName = "account-information-access-requests";
        private const string EntityName = "authorizations";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public AccountInformationAccessRequestAuthorizations(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { GrandParentEntityName, ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<AccountInformationAccessRequestAuthorizationResponse> Create(CustomerAccessToken token, Guid financialInstitutionId, Guid accountInformationAccessRequestId, AccountInformationAccessRequestAuthorizationRequest accountInformationAccessRequestAuthorizationRequest, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (accountInformationAccessRequestAuthorizationRequest is null)
                throw new ArgumentNullException(nameof(accountInformationAccessRequestAuthorizationRequest));

            var payload = new JsonApi.Data<AccountInformationAccessRequestAuthorizationRequest, object, object, object>
            {
                Type = "authorization",
                Attributes = accountInformationAccessRequestAuthorizationRequest
            };

            return InternalCreate(token, new[] { financialInstitutionId, accountInformationAccessRequestId }, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        protected override AccountInformationAccessRequestAuthorizationResponse Map(Data<AccountInformationAccessRequestAuthorizationResponse, object, object, AccountInformationAccessRequestAuthorizationLinks> data)
        {
            var result = base.Map(data);

            result.NextRedirect = data.Links?.NextRedirect;

            return result;
        }
    }

    /// <summary>
    /// <p>This object represent the authorization resource. When you perform an Authorization flow using TPP managed authorization flow, you need to create an authorization resource to complete the flow.</p>
    /// <p>The attribute queryParameters contains the query parameters returned by the financial institution when the customer is redirected to your configured redirect uri.</p>
    /// </summary>
    public interface IAccountInformationAccessRequestAuthorizations
    {
        /// <summary>
        /// Create account information access request authorization
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionId">Financial institution ID</param>
        /// <param name="accountInformationAccessRequestId">Account information access request ID</param>
        /// <param name="accountInformationAccessRequestAuthorizationRequest">Details of the account information access request authorization</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created Account Information Access Request Authorization resource</returns>
        Task<AccountInformationAccessRequestAuthorizationResponse> Create(CustomerAccessToken token, Guid financialInstitutionId, Guid accountInformationAccessRequestId, AccountInformationAccessRequestAuthorizationRequest accountInformationAccessRequestAuthorizationRequest, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
    }
}
