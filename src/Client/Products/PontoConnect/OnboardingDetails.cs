using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    /// <summary>
    /// <para>This is an object representing the onboarding details of the user who will undergo the linking process. It allows you to prefill the sign in or sign up forms with the user's details to streamline their Ponto onboarding process.</para>
    /// <para>For security purposes, the onboarding details object will only be available to be linked to a new Ponto user for five minutes following its creation. You should include the id in the access authorization url as an additional query parameter.</para>
    /// </summary>
    public class OnboardingDetails : IOnboardingDetails
    {
        private readonly IApiClient _apiClient;
        private readonly IClientAccessTokenProvider _accessTokenProvider;
        private readonly string _urlPrefix;

        private const string EntityName = "onboarding-details";

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="accessTokenProvider">Service to refresh client access tokens</param>
        /// <param name="urlPrefix">Beginning of URIs, composed by Ibanity API endpoint, followed by product name</param>
        public OnboardingDetails(IApiClient apiClient, IClientAccessTokenProvider accessTokenProvider, string urlPrefix)
        {
            _apiClient = apiClient;
            _accessTokenProvider = accessTokenProvider;
            _urlPrefix = urlPrefix;
        }

        /// <inheritdoc />
        public async Task<Models.OnboardingDetailsResponse> Create(ClientAccessToken token, Models.OnboardingDetails onboardingDetails, Guid? idempotencyKey, CancellationToken? cancellationToken) =>
            Map((await _apiClient.Post<JsonApi.Resource<Models.OnboardingDetails, object, object, object>, JsonApi.Resource<Models.OnboardingDetailsResponse, object, object, object>>(
                $"{_urlPrefix}/{EntityName}",
                (await _accessTokenProvider.RefreshToken(token ?? throw new ArgumentNullException(nameof(token)))).AccessToken,
                new JsonApi.Resource<Models.OnboardingDetails, object, object, object>
                {
                    Data = new JsonApi.Data<Models.OnboardingDetails, object, object, object>
                    {
                        Type = "onboardingDetails",
                        Attributes = onboardingDetails
                    }
                },
                idempotencyKey ?? Guid.NewGuid(),
                cancellationToken ?? CancellationToken.None)).Data);

        private static Models.OnboardingDetailsResponse Map(JsonApi.Data<Models.OnboardingDetailsResponse, object, object, object> data)
        {
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            var result = data.Attributes;

            result.Id = Guid.Parse(data.Id);

            return result;
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
        Task<Models.OnboardingDetailsResponse> Create(ClientAccessToken token, Models.OnboardingDetails onboardingDetails, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
    }
}
