using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        private const string RequestIdHeader = "ibanity-request-id";
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

            var headers = GetCommonHeaders(method, bearerToken, path, null, null);

            _logger.Trace($"Sending request: {method.ToString().ToUpper(CultureInfo.InvariantCulture)} {path}");

            using (var request = new HttpRequestMessage(method, path))
            {
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);

                _customizeRequest(request);

                var response = await (await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false)).ThrowOnFailure(_serializer, _logger).ConfigureAwait(false);

                var requestId = response.Headers.TryGetValues(RequestIdHeader, out var values) ? values.SingleOrDefault() : null;
                _logger.Debug($"Response received ({response.StatusCode:D} {response.StatusCode:G}): {method.ToString().ToUpper(CultureInfo.InvariantCulture)} {path} (request ID: {requestId})");

                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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
        public Task<TResponse> Post<TRequest, TResponse>(string path, string bearerToken, TRequest payload, Guid? idempotencyKey, CancellationToken cancellationToken) =>
            SendWithPayload<TRequest, TResponse>(HttpMethod.Post, path, bearerToken, payload, idempotencyKey, cancellationToken);

        /// <inheritdoc />
        public Task<TResponse> Patch<TRequest, TResponse>(string path, string bearerToken, TRequest payload, Guid? idempotencyKey, CancellationToken cancellationToken) =>
            SendWithPayload<TRequest, TResponse>(new HttpMethod("PATCH"), path, bearerToken, payload, idempotencyKey, cancellationToken);

        private async Task<TResponse> SendWithPayload<TRequest, TResponse>(HttpMethod method, string path, string bearerToken, TRequest payload, Guid? idempotencyKey, CancellationToken cancellationToken)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            var content = _serializer.Serialize(payload);

            Dictionary<string, string> headers;

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream, Encoding))
            {
                writer.Write(payload);
                writer.Flush();

                stream.Seek(0L, SeekOrigin.Begin);
                headers = GetCommonHeaders(method, bearerToken, path, idempotencyKey, stream);
            }

            _logger.Trace($"Sending request: {method.ToString().ToUpper(CultureInfo.InvariantCulture)} {path}");

            using (var request = new HttpRequestMessage(method, path))
            {
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);

                request.Content = new StringContent(content, Encoding, "application/vnd.api+json");

                _customizeRequest(request);

                var response = await (await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false)).ThrowOnFailure(_serializer, _logger).ConfigureAwait(false);

                var requestId = response.Headers.TryGetValues(RequestIdHeader, out var values) ? values.SingleOrDefault() : null;
                _logger.Debug($"Response received ({response.StatusCode:D} {response.StatusCode:G}): {method.ToString().ToUpper(CultureInfo.InvariantCulture)} {path} (request ID: {requestId})");

                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return _serializer.Deserialize<TResponse>(body);
            }
        }

        private Dictionary<string, string> GetCommonHeaders(HttpMethod method, string bearerToken, string path, Guid? idempotencyKey, Stream payload)
        {
            var headers = new Dictionary<string, string>
            {
                {
                    "Accept",
                    "application/vnd.api+json" + (string.IsNullOrWhiteSpace(_apiVersion) ? string.Empty : $";version={_apiVersion}")
                }
            };

            if (!string.IsNullOrWhiteSpace(bearerToken))
                headers["Authorization"] = $"Bearer {bearerToken}";

            if (idempotencyKey.HasValue)
                headers["Ibanity-Idempotency-Key"] = idempotencyKey.Value.ToString();

            var signatureHeaders = _signatureService.GetHttpSignatureHeaders(
                method.Method.ToUpper(CultureInfo.InvariantCulture),
                new Uri(_httpClient.BaseAddress + path),
                headers,
                payload);

            foreach (var header in signatureHeaders)
                headers.Add(header.Key, header.Value);

            return headers;
        }

        /// <inheritdoc />
        public async Task<TResponse> PostInline<TResponse>(string path, string bearerToken, IDictionary<string, string> additionalHeaders, string filename, Stream payload, CancellationToken cancellationToken)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            var headers = GetCommonHeaders(HttpMethod.Post, bearerToken, path, null, payload);
            payload.Seek(0L, SeekOrigin.Begin);

            _logger.Trace($"Sending request: {HttpMethod.Post.ToString().ToUpper(CultureInfo.InvariantCulture)} {path}");

            using (var request = new HttpRequestMessage(HttpMethod.Post, path))
            {
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);

                foreach (var header in additionalHeaders)
                    request.Headers.Add(header.Key, header.Value);

                request.Content = new StreamContent(payload);
                request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/xml");
                request.Content.Headers.Add("Content-Disposition", $"inline; filename='{filename}'");

                _customizeRequest(request);

                var response = await (await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false)).ThrowOnFailure(_serializer, _logger).ConfigureAwait(false);

                var requestId = response.Headers.TryGetValues(RequestIdHeader, out var values) ? values.SingleOrDefault() : null;
                _logger.Debug($"Response received ({response.StatusCode:D} {response.StatusCode:G}): {HttpMethod.Post.ToString().ToUpper(CultureInfo.InvariantCulture)} {path} (request ID: {requestId})");

                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return _serializer.Deserialize<TResponse>(body);
            }
        }

        /// <inheritdoc />
        public async Task GetToStream(string path, string bearerToken, Stream target, CancellationToken cancellationToken)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            if (target is null)
                throw new ArgumentNullException(nameof(target));

            var headers = GetCommonHeaders(HttpMethod.Get, bearerToken, path, null, null);

            using (var request = new HttpRequestMessage(HttpMethod.Get, path))
            {
                foreach (var header in headers)
                    if (header.Key != "Accept")
                        request.Headers.Add(header.Key, header.Value);

                _customizeRequest(request);

                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                using (var streamToReadFrom = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    await streamToReadFrom.CopyToAsync(target, 81920, cancellationToken).ConfigureAwait(false);
            }
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
        /// Send a GET request and save the result to a provided stream.
        /// </summary>
        /// <param name="path">Query string, absolute, or relative to product root</param>
        /// <param name="bearerToken">Token added to Authorization header</param>
        /// <param name="target">Destination stream where the payload will be written to</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The specified resource</returns>
        Task GetToStream(string path, string bearerToken, Stream target, CancellationToken cancellationToken);

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
        Task<TResponse> Post<TRequest, TResponse>(string path, string bearerToken, TRequest payload, Guid? idempotencyKey, CancellationToken cancellationToken);

        /// <summary>
        /// Send an inline payload with a POST request.
        /// </summary>
        /// <typeparam name="TResponse">Type of the received payload</typeparam>
        /// <param name="path">Query string, absolute, or relative to product root</param>
        /// <param name="bearerToken">Token added to Authorization header</param>
        /// <param name="additionalHeaders">Additional headers</param>
        /// <param name="filename">Content disposition filename</param>
        /// <param name="payload">Data to be sent</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        /// <returns>The created resource</returns>
        Task<TResponse> PostInline<TResponse>(string path, string bearerToken, IDictionary<string, string> additionalHeaders, string filename, Stream payload, CancellationToken cancellationToken);

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
        Task<TResponse> Patch<TRequest, TResponse>(string path, string bearerToken, TRequest payload, Guid? idempotencyKey, CancellationToken cancellationToken);
    }
}
