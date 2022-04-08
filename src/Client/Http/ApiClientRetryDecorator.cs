using System;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Client.Http
{
    public class ApiClientRetryDecorator : IApiClient
    {
        private readonly ILogger _logger;
        private readonly IApiClient _underlyingInstance;
        private readonly int _count;
        private readonly TimeSpan _baseDelay;

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
                    return await action();
                }
                catch (IbanityClientException)
                {
                    throw; // no need to retry a faulty request
                }
                catch (Exception e) when (i < _count)
                {
                    var delay = TimeSpan.FromSeconds(Math.Pow(_baseDelay.TotalSeconds, i + 1));
                    _logger.Warn($"{operation} failed (try {i + 1} of {_count}), retrying in {delay.TotalSeconds:F2} seconds", e);
                    await Task.Delay(delay, cancellationToken);
                }
        }

        public Task<T> Delete<T>(string path, string bearerToken, CancellationToken cancellationToken) =>
            Execute<T>("Delete", async () => await Delete<T>(path, bearerToken, cancellationToken), cancellationToken);

        public Task Delete(string path, string bearerToken, CancellationToken cancellationToken) =>
            Delete<object>(path, bearerToken, cancellationToken);

        public Task<T> Get<T>(string path, string bearerToken, CancellationToken cancellationToken) =>
            Execute<T>("Get", async () => await Get<T>(path, bearerToken, cancellationToken), cancellationToken);

        public Task<TResponse> Patch<TRequest, TResponse>(string path, string bearerToken, TRequest payload, Guid idempotencyKey, CancellationToken cancellationToken) =>
            Execute<TResponse>("Patch", async () => await Patch<TRequest, TResponse>(path, bearerToken, payload, idempotencyKey, cancellationToken), cancellationToken);

        public Task<TResponse> Post<TRequest, TResponse>(string path, string bearerToken, TRequest payload, Guid idempotencyKey, CancellationToken cancellationToken) =>
            Execute<TResponse>("Post", async () => await Post<TRequest, TResponse>(path, bearerToken, payload, idempotencyKey, cancellationToken), cancellationToken);
    }
}
