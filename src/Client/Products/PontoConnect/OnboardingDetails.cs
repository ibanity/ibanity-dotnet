using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <summary>
    /// <para>This is an object representing the onboarding details of the user who will undergo the linking process. It allows you to prefill the sign in or sign up forms with the user's details to streamline their Ponto onboarding process.</para>
    /// <para>For security purposes, the onboarding details object will only be available to be linked to a new Ponto user for five minutes following its creation. You should include the id in the access authorization url as an additional query parameter.</para>
    /// </summary>
    public class OnboardingDetails : ResourceClient<OnboardingDetailsResponse, object, object, object>, IOnboardingDetails
    {
        private const string EntityName = "onboarding-details";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public OnboardingDetails(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        /// <inheritdoc />
        public Task<OnboardingDetailsResponse> Create(Token token, Models.OnboardingDetails onboardingDetails, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (onboardingDetails is null)
                throw new ArgumentNullException(nameof(onboardingDetails));

            var payload = new JsonApi.Data<Models.OnboardingDetails, object, object, object>
            {
                Type = "onboardingDetails",
                Attributes = onboardingDetails
            };

            return InternalCreate(token, payload, idempotencyKey, cancellationToken);
        }
    }

    /// <summary>
    /// <para>This is an object representing the onboarding details of the user who will undergo the linking process. It allows you to prefill the sign in or sign up forms with the user's details to streamline their Ponto onboarding process.</para>
    /// <para>For security purposes, the onboarding details object will only be available to be linked to a new Ponto user for five minutes following its creation. You should include the id in the access authorization url as an additional query parameter.</para>
    /// </summary>
    public interface IOnboardingDetails
    {
        /// <summary>
        /// Create Onboarding Details
        /// </summary>
        /// <param name="token">Authentication token</param>
        /// <param name="onboardingDetails">Onboarding details of the user who will undergo the linking process</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created onboarding details resource</returns>
        Task<OnboardingDetailsResponse> Create(Token token, Models.OnboardingDetails onboardingDetails, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
    }
}
