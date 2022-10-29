using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Client.Http
{
    /// <inheritdoc />
    public class CustomerAccessTokenProvider : ICustomerAccessTokenProvider
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly ISerializer<string> _serializer;
        private readonly string _urlPrefix;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="loggerFactory">Allow to build the logger used within this instance</param>
        /// <param name="httpClient">Low-level HTTP client</param>
        /// <param name="serializer">To-string serializer</param>
        /// <param name="urlPrefix">Product endpoint</param>
        public CustomerAccessTokenProvider(ILoggerFactory loggerFactory, HttpClient httpClient, ISerializer<string> serializer, string urlPrefix)
        {
            if (loggerFactory is null)
                throw new ArgumentNullException(nameof(loggerFactory));

            if (string.IsNullOrWhiteSpace(urlPrefix))
                throw new ArgumentException($"'{nameof(urlPrefix)}' cannot be null or whitespace.", nameof(urlPrefix));

            _logger = loggerFactory.CreateLogger<CustomerAccessTokenProvider>();
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _urlPrefix = urlPrefix;
        }

        /// <inheritdoc />
        public Task<CustomerAccessToken> GetToken(CancellationToken? cancellationToken = null)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        /// <remarks>Does nothing.</remarks>
        public Task<CustomerAccessToken> RefreshToken(CustomerAccessToken token, CancellationToken? cancellationToken = null) =>
            Task.FromResult(token);
    }
}
