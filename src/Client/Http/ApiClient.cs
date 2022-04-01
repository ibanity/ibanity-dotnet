using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Client.Http
{
    public class ApiClient : IApiClient
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly ISerializer<string> _serializer;
        private readonly IHttpSignatureService _signatureService;
        private readonly string _apiVersion;
        private readonly Action<HttpRequestMessage> _customizeRequest;

        public ApiClient(ILoggerFactory loggerFactory, HttpClient httpClient, ISerializer<string> serializer, IHttpSignatureService signatureService, string apiVersion, Action<HttpRequestMessage> customizeRequest)
        {
            if (loggerFactory is null)
                throw new ArgumentNullException(nameof(loggerFactory));

            if (string.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException($"'{nameof(apiVersion)}' cannot be null or whitespace.", nameof(apiVersion));

            _logger = loggerFactory.CreateLogger<ApiClient>();
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _signatureService = signatureService ?? throw new ArgumentNullException(nameof(signatureService));
            _apiVersion = apiVersion;
            _customizeRequest = customizeRequest ?? throw new ArgumentNullException(nameof(customizeRequest));
        }

        public async Task<T> Get<T>(string path, string bearerToken, CancellationToken cancellationToken)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            var headers = new Dictionary<string, string>
            {
                { "Accept", "application/vnd.api+json;version=" + _apiVersion }
            };

            if (!string.IsNullOrWhiteSpace(bearerToken))
                headers["Authorization"] = $"Bearer {bearerToken}";

            var signatureHeaders = _signatureService.GetHttpSignatureHeaders(
                "GET",
                new Uri(_httpClient.BaseAddress + path),
                headers,
                null);

            _logger.Debug("Sending request: GET " + path);

            using (var request = new HttpRequestMessage(HttpMethod.Get, path))
            {
                foreach (var kvp in headers.Union(signatureHeaders))
                    request.Headers.Add(kvp.Key, kvp.Value);

                _customizeRequest(request);

                var response = await (await _httpClient.SendAsync(request, cancellationToken)).ThrowOnFailure(_serializer);
                var body = await response.Content.ReadAsStringAsync();
                return _serializer.Deserialize<T>(body);
            }
        }
    }

    public interface IApiClient
    {
        Task<T> Get<T>(string path, string bearerToken, CancellationToken cancellationToken);
    }
}
