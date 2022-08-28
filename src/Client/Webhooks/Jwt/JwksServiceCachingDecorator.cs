using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Webhooks.Jwt
{
    /// <summary>
    /// Wrapper around the actual service, caching results for some time.
    /// </summary>
    public class JwksServiceCachingDecorator : IJwksService
    {
        private readonly ConcurrentDictionary<string, CacheEntry> _cache = new ConcurrentDictionary<string, CacheEntry>();

        private struct CacheEntry
        {
            public DateTimeOffset InsertedAt { get; set; }
            public RSA Value { get; set; }
        }

        private readonly IJwksService _underlyingInstance;
        private readonly IClock _clock;
        private readonly TimeSpan _ttl;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="underlyingInstance">Actual service</param>
        /// <param name="clock">Returns current date and time</param>
        /// <param name="ttl">Time to live</param>
        public JwksServiceCachingDecorator(IJwksService underlyingInstance, IClock clock, TimeSpan ttl)
        {
            _underlyingInstance = underlyingInstance ?? throw new ArgumentNullException(nameof(underlyingInstance));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
            _ttl = ttl;
        }

        /// <inheritdoc />
        public async Task<RSA> GetPublicKey(string keyId, CancellationToken? cancellationToken = null)
        {
            var threshold = _clock.Now - _ttl;
            var toRemove = _cache.
                Where(kvp => kvp.Value.InsertedAt < threshold).
                Select(kvp => kvp.Key).
                ToArray();

            foreach (var id in toRemove)
                _cache.TryRemove(id, out _);

            if (_cache.TryGetValue(keyId, out var entry))
                return entry.Value;

            entry = new CacheEntry
            {
                InsertedAt = _clock.Now,
                Value = await _underlyingInstance.GetPublicKey(keyId, cancellationToken).ConfigureAwait(false)
            };

            _cache[keyId] = entry;

            return entry.Value;
        }
    }
}
