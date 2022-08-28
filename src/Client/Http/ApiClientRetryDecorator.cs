using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Client.Http
{
    /// <summary>
    /// Wrapper around the actual client, allowing to retry failed requests.
    /// </summary>
    public class ApiClientRetryDecorator : IApiClient
    {
        private readonly ILogger _logger;
        private readonly IApiClient _underlyingInstance;
        private readonly int _count;
        private readonly TimeSpan _baseDelay;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="loggerFactory">Allow to build the logger used within this instance</param>
        /// <param name="underlyingInstance">Actual client</param>
        /// <param name="count">Number of retries after failed operations</param>
        /// <param name="baseDelay">Delay before a retry with exponential backoff</param>
        /// <remarks>Delay is increased by a 2-factor after each failure: 1 second, then 2 seconds, then 4 seconds, ...</remarks>
        public ApiClientRetryDecorator(ILoggerFactory loggerFactory, IApiClient underlyingInstance, int count, TimeSpan baseDelay)
        {
            if (loggerFactory is null)
                throw new ArgumentNullException(nameof(loggerFactory));

            _logger = loggerFactory.CreateLogger<ApiClientRetryDecorator>();
            _underlyingInstance = underlyingInstance ?? throw new ArgumentNullException(nameof(underlyingInstance));
            _count = count;
            _baseDelay = baseDelay;
        }

        private async Task<T> Execute<T>(string operation, Func<Task<T>> action, CancellationToken cancellationToken)
        {
            for (var i = 0; ; i++)
                try
                {
                    return await action().ConfigureAwait(false);
                }
                catch (IbanityClientException)
                {
                    throw; // no need to retry a faulty request
                }
                catch (Exception e) when (i < _count)
                {
                    var delay = TimeSpan.FromSeconds(Math.Pow(_baseDelay.TotalSeconds, i + 1));
                    _logger.Warn($"{operation} failed (try {i + 1} of {_count}), retrying in {delay.TotalSeconds:F2} seconds", e);
                    await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                }
        }

        /// <inheritdoc />
        public Task<T> Delete<T>(string path, string bearerToken, CancellationToken cancellationToken) =>
            Execute("Delete", async () => await _underlyingInstance.Delete<T>(path, bearerToken, cancellationToken).ConfigureAwait(false), cancellationToken);

        /// <inheritdoc />
        public Task Delete(string path, string bearerToken, CancellationToken cancellationToken) =>
            Delete<object>(path, bearerToken, cancellationToken);

        /// <inheritdoc />
        public Task<T> Get<T>(string path, string bearerToken, CancellationToken cancellationToken) =>
            Execute("Get", async () => await _underlyingInstance.Get<T>(path, bearerToken, cancellationToken).ConfigureAwait(false), cancellationToken);

        /// <inheritdoc />
        public Task<TResponse> Patch<TRequest, TResponse>(string path, string bearerToken, TRequest payload, Guid? idempotencyKey, CancellationToken cancellationToken) =>
            Execute("Patch", async () => await _underlyingInstance.Patch<TRequest, TResponse>(path, bearerToken, payload, idempotencyKey, cancellationToken).ConfigureAwait(false), cancellationToken);

        /// <inheritdoc />
        public Task<TResponse> Post<TRequest, TResponse>(string path, string bearerToken, TRequest payload, Guid? idempotencyKey, CancellationToken cancellationToken) =>
            Execute("Post", async () => await _underlyingInstance.Post<TRequest, TResponse>(path, bearerToken, payload, idempotencyKey, cancellationToken).ConfigureAwait(false), cancellationToken);

        /// <inheritdoc />
        public Task<TResponse> PostInline<TResponse>(string path, string bearerToken, IDictionary<string, string> additionalHeaders, string filename, Stream payload, CancellationToken cancellationToken) =>
            Execute("Post", async () => await _underlyingInstance.PostInline<TResponse>(path, bearerToken, additionalHeaders, filename, payload, cancellationToken).ConfigureAwait(false), cancellationToken);

        /// <inheritdoc />
        public Task GetToStream(string path, string bearerToken, Stream target, CancellationToken cancellationToken) =>
            Execute<object>("Get", async () => { await _underlyingInstance.GetToStream(path, bearerToken, target, cancellationToken).ConfigureAwait(false); return null; }, cancellationToken);
    }
}
