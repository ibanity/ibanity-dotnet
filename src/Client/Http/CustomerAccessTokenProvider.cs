using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Client.Http
{
    /// <inheritdoc />
    public class CustomerAccessTokenProvider : ICustomerAccessTokenProvider
    {
        private readonly IApiClient _apiClient;
        private readonly string _urlPrefix;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="urlPrefix">Product endpoint</param>
        public CustomerAccessTokenProvider(IApiClient apiClient, string urlPrefix)
        {
            if (string.IsNullOrWhiteSpace(urlPrefix))
                throw new ArgumentException($"'{nameof(urlPrefix)}' cannot be null or whitespace.", nameof(urlPrefix));

            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _urlPrefix = urlPrefix;
        }

        /// <inheritdoc />
        public async Task<CustomerAccessToken> GetToken(string applicationCustomerReference, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            var response = await _apiClient.Post<JsonApi.Resource<CustomerAccessTokenRequest, object, object, object>, JsonApi.Resource<CustomerAccessTokenResponse, object, object, object>>(
                _urlPrefix + "/customer-access-tokens",
                null,
                new JsonApi.Resource<CustomerAccessTokenRequest, object, object, object>
                {
                    Data = new JsonApi.Data<CustomerAccessTokenRequest, object, object, object>
                    {
                        Type = "customerAccessToken",
                        Attributes = new CustomerAccessTokenRequest
                        {
                            ApplicationCustomerReference = applicationCustomerReference
                        }
                    }
                },
                idempotencyKey ?? Guid.NewGuid(),
                cancellationToken ?? CancellationToken.None).ConfigureAwait(false);

            return new CustomerAccessToken
            {
                AccessToken = response.Data.Attributes.Token,
                Id = Guid.Parse(response.Data.Id)
            };
        }

        /// <inheritdoc />
        /// <remarks>Does nothing.</remarks>
        public Task<CustomerAccessToken> RefreshToken(CustomerAccessToken token, CancellationToken? cancellationToken = null) =>
            Task.FromResult(token);
    }
}
