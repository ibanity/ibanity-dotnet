using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Client.Http
{
    /// <inheritdoc />
    public class ApiClient : IApiClient
    {
        private static readonly Encoding Encoding = Encoding.UTF8;

        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly ISerializer<string> _serializer;
        private readonly IHttpSignatureService _signatureService;
        private readonly string _apiVersion;
        private readonly Action<HttpRequestMessage> _customizeRequest;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="loggerFactory">Allow to build the logger used within this instance</param>
        /// <param name="httpClient">Low-level HTTP client</param>
        /// <param name="serializer">To-string serializer</param>
        /// <param name="signatureService">HTTP request signature service</param>
        /// <param name="apiVersion">Used in accept header</param>
        /// <param name="customizeRequest">Allow to modify requests before sending them</param>
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

        /// <inheritdoc />
        public Task<T> Get<T>(string path, string bearerToken, CancellationToken cancellationToken) =>
            SendWithoutPayload<T>(HttpMethod.Get, path, bearerToken, cancellationToken);

        private async Task<T> SendWithoutPayload<T>(HttpMethod method, string path, string bearerToken, CancellationToken cancellationToken)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            var headers = GetCommonHeaders(method, bearerToken, path, null);

            _logger.Debug($"Sending request: {method.ToString().ToUpper(CultureInfo.InvariantCulture)} {path}");

            using (var request = new HttpRequestMessage(method, path))
            {
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);

                _customizeRequest(request);

                var response = await (await _httpClient.SendAsync(request, cancellationToken)).ThrowOnFailure(_serializer);
                var body = await response.Content.ReadAsStringAsync();
                return _serializer.Deserialize<T>(body);
            }
        }

        /// <inheritdoc />
        public Task<T> Delete<T>(string path, string bearerToken, CancellationToken cancellationToken) =>
            SendWithoutPayload<T>(HttpMethod.Delete, path, bearerToken, cancellationToken);

        /// <inheritdoc />
        public Task Delete(string path, string bearerToken, CancellationToken cancellationToken) =>
            Delete<object>(path, bearerToken, cancellationToken);

        /// <inheritdoc />
        public Task<TResponse> Post<TRequest, TResponse>(string path, string bearerToken, TRequest payload, Guid idempotencyKey, CancellationToken cancellationToken) =>
            SendWithPayload<TRequest, TResponse>(HttpMethod.Post, path, bearerToken, payload, idempotencyKey, cancellationToken);

        /// <inheritdoc />
        public Task<TResponse> Patch<TRequest, TResponse>(string path, string bearerToken, TRequest payload, Guid idempotencyKey, CancellationToken cancellationToken) =>
            SendWithPayload<TRequest, TResponse>(new HttpMethod("PATCH"), path, bearerToken, payload, idempotencyKey, cancellationToken);

        private async Task<TResponse> SendWithPayload<TRequest, TResponse>(HttpMethod method, string path, string bearerToken, TRequest payload, Guid idempotencyKey, CancellationToken cancellationToken)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            var headers = GetCommonHeaders(method, bearerToken, path, idempotencyKey);

            _logger.Debug($"Sending request: {method.ToString().ToUpper(CultureInfo.InvariantCulture)} {path}");

            using (var request = new HttpRequestMessage(method, path))
            {
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);

                var content = _serializer.Serialize(payload);
                request.Content = new StringContent(content, Encoding, "application/vnd.api+json");

                _customizeRequest(request);

                var response = await (await _httpClient.SendAsync(request, cancellationToken)).ThrowOnFailure(_serializer);
                var body = await response.Content.ReadAsStringAsync();
                return _serializer.Deserialize<TResponse>(body);
            }
        }

        private Dictionary<string, string> GetCommonHeaders(HttpMethod method, string bearerToken, string path, Guid? idempotencyKey)
        {
            var headers = new Dictionary<string, string>
            {
                { "Accept", "application/vnd.api+json;version=" + _apiVersion }
            };

            if (!string.IsNullOrWhiteSpace(bearerToken))
                headers["Authorization"] = $"Bearer {bearerToken}";

            if (idempotencyKey.HasValue)
                headers["Ibanity-Idempotency-Key"] = idempotencyKey.Value.ToString();

            var signatureHeaders = _signatureService.GetHttpSignatureHeaders(
                method.Method.ToUpper(CultureInfo.InvariantCulture),
                new Uri(_httpClient.BaseAddress + path),
                headers,
                null);

            foreach (var header in signatureHeaders)
                headers.Add(header.Key, header.Value);

            return headers;
        }
    }

    /// <summary>
    /// Low-level HTTP client
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Send a GET request.
        /// </summary>
        /// <typeparam name="T">Type of the received payload</typeparam>
        /// <param name="path">Query string, absolute, or relative to product root</param>
        /// <param name="bearerToken">Token added to Authorization header</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The specified resource</returns>
        Task<T> Get<T>(string path, string bearerToken, CancellationToken cancellationToken);

        /// <summary>
        /// Send a DELETE request with a returned payload.
        /// </summary>
        /// <typeparam name="T">Type of the received payload</typeparam>
        /// <param name="path">Query string, absolute, or relative to product root</param>
        /// <param name="bearerToken">Token added to Authorization header</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The deleted resource</returns>
        Task<T> Delete<T>(string path, string bearerToken, CancellationToken cancellationToken);

        /// <summary>
        /// Send a DELETE request without a returned payload.
        /// </summary>
        /// <param name="path">Query string, absolute, or relative to product root</param>
        /// <param name="bearerToken">Token added to Authorization header</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        Task Delete(string path, string bearerToken, CancellationToken cancellationToken);

        /// <summary>
        /// Send a POST request.
        /// </summary>
        /// <typeparam name="TRequest">Type of the payload to be sent</typeparam>
        /// <typeparam name="TResponse">Type of the received payload</typeparam>
        /// <param name="path">Query string, absolute, or relative to product root</param>
        /// <param name="bearerToken">Token added to Authorization header</param>
        /// <param name="payload">Data to be sent</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created resource</returns>
        Task<TResponse> Post<TRequest, TResponse>(string path, string bearerToken, TRequest payload, Guid idempotencyKey, CancellationToken cancellationToken);

        /// <summary>
        /// Send a PATCH request.
        /// </summary>
        /// <typeparam name="TRequest">Type of the payload to be sent</typeparam>
        /// <typeparam name="TResponse">Type of the received payload</typeparam>
        /// <param name="path">Query string, absolute, or relative to product root</param>
        /// <param name="bearerToken">Token added to Authorization header</param>
        /// <param name="payload">Data to be sent</param>
        /// <param name="idempotencyKey">Several requests with the same idempotency key will be executed only once</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The updated resource</returns>
        Task<TResponse> Patch<TRequest, TResponse>(string path, string bearerToken, TRequest payload, Guid idempotencyKey, CancellationToken cancellationToken);
    }
}
