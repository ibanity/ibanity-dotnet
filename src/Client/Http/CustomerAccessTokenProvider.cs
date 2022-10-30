using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Client.Http
{
    /// <inheritdoc />
    public class CustomerAccessTokenProvider : ICustomerAccessTokenProvider
    {
        private readonly ILogger _logger;
        private readonly IApiClient _apiClient;
        private readonly string _urlPrefix;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="loggerFactory">Allow to build the logger used within this instance</param>
        /// <param name="apiClient">Generic API client</param>
        /// <param name="urlPrefix">Product endpoint</param>
        public CustomerAccessTokenProvider(ILoggerFactory loggerFactory, IApiClient apiClient, string urlPrefix)
        {
            if (loggerFactory is null)
                throw new ArgumentNullException(nameof(loggerFactory));

            if (string.IsNullOrWhiteSpace(urlPrefix))
                throw new ArgumentException($"'{nameof(urlPrefix)}' cannot be null or whitespace.", nameof(urlPrefix));

            _logger = loggerFactory.CreateLogger<CustomerAccessTokenProvider>();
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _urlPrefix = urlPrefix;
        }

        /// <inheritdoc />
        public Task<CustomerAccessToken> GetToken(string applicationCustomerReference, Guid? idempotencyKey = null, CancellationToken? cancellationToken = null)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        /// <remarks>Does nothing.</remarks>
        public Task<CustomerAccessToken> RefreshToken(CustomerAccessToken token, CancellationToken? cancellationToken = null) =>
            Task.FromResult(token);
    }
}
