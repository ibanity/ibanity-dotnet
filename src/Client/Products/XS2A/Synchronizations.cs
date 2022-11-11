using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.XS2A.Models;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A
{
    /// <inheritdoc cref="ISynchronizations" />
    public class Synchronizations : ResourceClient<SynchronizationResponse, object, object, object, CustomerAccessToken>, ISynchronizations
    {
        private const string EntityName = "customer/synchronizations";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public Synchronizations(IApiClient apiClient, IAccessTokenProvider<CustomerAccessToken> accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        public Task<SynchronizationResponse> Create(CustomerAccessToken token, SynchronizationRequest synchronization, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (synchronization is null)
                throw new ArgumentNullException(nameof(synchronization));

            var payload = new JsonApi.Data<SynchronizationRequest, object, object, object>
            {
                Type = "synchronization",
                Attributes = synchronization
            };

            return InternalCreate(token, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<SynchronizationResponse> Get(CustomerAccessToken token, Guid id, CancellationToken? cancellationToken = null) =>
            InternalGet(token, id, cancellationToken);

        /// <inheritdoc />
        public Task<IbanityCollection<SynchronizationResponse>> List(CustomerAccessToken token, Guid financialInstitutionsId, Guid accountInformationAccessRequestsId, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null) =>
            InternalCursorBasedList(
                token,
                $"{UrlPrefix}/customer/financial-institutions/{financialInstitutionsId}/account-information-access-requests/{accountInformationAccessRequestsId}/initial-account-transactions-synchronizations",
                null,
                pageLimit,
                pageBefore,
                pageAfter,
                cancellationToken);
    }

    /// <summary>
    /// <p>This is an object representing a resource synchronization. This object will give you the details of the synchronization, including its resource, type, and status.</p>
    /// <p>The synchronization API endpoints are customer specific and therefore can only be accessed by providing the corresponding customer access token.</p>
    /// </summary>
    public interface ISynchronizations
    {
        /// <summary>
        /// Create Synchronization
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="synchronization">Details of the synchronization, including its resource, type, and status</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A synchronization resource</returns>
        Task<SynchronizationResponse> Create(CustomerAccessToken token, SynchronizationRequest synchronization, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Get Synchronization
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="id">ID of the synchronization</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A synchronization resource</returns>
        Task<SynchronizationResponse> Get(CustomerAccessToken token, Guid id, CancellationToken? cancellationToken = null);

        /// <summary>
        /// List Initial Account Transactions Synchronization for Account Information Access Request
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="financialInstitutionsId">Financial institutions ID</param>
        /// <param name="accountInformationAccessRequestsId">Account information access requests ID</param>
        /// <param name="pageLimit">Maximum number (1-100) of resources that might be returned. It is possible that the response contains fewer elements. Defaults to 10</param>
        /// <param name="pageBefore">Cursor for pagination. Indicates that the API should return the synchronization resources which are immediately before this one in the list (the previous page)</param>
        /// <param name="pageAfter">Cursor for pagination. Indicates that the API should return the synchronization resources which are immediately after this one in the list (the next page)</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>A list of initiation account's transactions synchronization resources related to the account information access request</returns>
        Task<IbanityCollection<SynchronizationResponse>> List(CustomerAccessToken token, Guid financialInstitutionsId, Guid accountInformationAccessRequestsId, int? pageLimit = null, Guid? pageBefore = null, Guid? pageAfter = null, CancellationToken? cancellationToken = null);
    }
}
