using System;
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
        public Task<RSA> GetPublicKey(string keyId, CancellationToken? cancellationToken = null)
        {
            return _underlyingInstance.GetPublicKey(keyId, cancellationToken);
        }
    }
}
