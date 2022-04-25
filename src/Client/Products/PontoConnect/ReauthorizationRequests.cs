using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <summary>
    /// <para>This object allows you to request the reauthorization of a specific bank account.</para>
    /// <para>By providing a redirect URI, you can create a redirect link to which you can send your customer so they can directly reauthorize their account on Ponto. After reauthorizing at their bank portal, they are redirected automatically back to your application, to the redirect URI of your choosing.</para>
    /// </summary>
    public class ReauthorizationRequests : ResourceWithParentClient<ReauthorizationRequest, object, object, ReauthorizationRequestLinks>, IReauthorizationRequests
    {
        private const string ParentEntityName = "accounts";
        private const string EntityName = "transactions";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public ReauthorizationRequests(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, new[] { ParentEntityName, EntityName })
        { }

        /// <inheritdoc />
        public Task<ReauthorizationRequest> Create(Token token, Guid accountId, Uri redirect, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (redirect is null)
                throw new ArgumentNullException(nameof(redirect));

            var payload = new JsonApi.Data<ReauthorizationRequest, object, object, object>();
            payload.Type = "reauthorizationRequest";
            payload.Attributes.Redirect = redirect;

            return InternalCreate(token, new[] { accountId }, payload, idempotencyKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<ReauthorizationRequest> Create(Token token, Guid accountId, string redirectUri, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(redirectUri))
                throw new ArgumentException($"'{nameof(redirectUri)}' cannot be null or whitespace.", nameof(redirectUri));

            return Create(
                token,
                accountId,
                new Uri(redirectUri),
                idempotencyKey,
                cancellationToken);
        }

        /// <inheritdoc />
        protected override ReauthorizationRequest Map(JsonApi.Data<ReauthorizationRequest, object, object, ReauthorizationRequestLinks> data)
        {
            var result = base.Map(data);

            result.Redirect = data.Links.Redirect;

            return result;
        }
    }

    /// <summary>
    /// <para>This object allows you to request the reauthorization of a specific bank account.</para>
    /// <para>By providing a redirect URI, you can create a redirect link to which you can send your customer so they can directly reauthorize their account on Ponto. After reauthorizing at their bank portal, they are redirected automatically back to your application, to the redirect URI of your choosing.</para>
    /// </summary>
    public interface IReauthorizationRequests
    {
        /// <summary>
        /// Request Account Reauthorization
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Bank account ID</param>
        /// <param name="redirect">URI that your user will be redirected to at the end of the authorization flow</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created reauthorization request resource</returns>
        Task<ReauthorizationRequest> Create(Token token, Guid accountId, Uri redirect, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Request Account Reauthorization
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="accountId">Bank account ID</param>
        /// <param name="redirectUri">URI that your user will be redirected to at the end of the authorization flow</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created reauthorization request resource</returns>
        Task<ReauthorizationRequest> Create(Token token, Guid accountId, string redirectUri, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
    }
}
