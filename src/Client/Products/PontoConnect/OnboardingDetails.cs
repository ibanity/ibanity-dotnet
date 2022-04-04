using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;
using Ibanity.Apis.Client.Products.PontoConnect.Models;

namespace Ibanity.Apis.Client.Products.PontoConnect
{
    public class OnboardingDetails : ResourceClient<OnboardingDetailsResponse, object, object, object>, IOnboardingDetails
    {
        private const string EntityName = "onboarding-details";

        public OnboardingDetails(IApiClient apiClient, IAccessTokenProvider accessTokenProvider, string urlPrefix) :
            base(apiClient, accessTokenProvider, urlPrefix, EntityName)
        { }

        public Task<OnboardingDetailsResponse> Create(Token token, Models.OnboardingDetails onboardingDetails, Guid? idempotencyKey, CancellationToken? cancellationToken)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (onboardingDetails is null)
                throw new ArgumentNullException(nameof(onboardingDetails));

            var payload = new JsonApi.Data<Models.OnboardingDetails, object, object, object>();
            payload.Type = "onboardingDetails";
            payload.Attributes = onboardingDetails;

            return InternalCreate(token, payload, idempotencyKey, cancellationToken);
        }
    }

    public interface IOnboardingDetails
    {
        Task<OnboardingDetailsResponse> Create(Token token, Models.OnboardingDetails onboardingDetails, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null);
    }
}
