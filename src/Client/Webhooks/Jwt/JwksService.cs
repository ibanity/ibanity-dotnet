using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Http;

namespace Ibanity.Apis.Client.Webhooks.Jwt
{
    /// <inheritdoc />
    public class JwksService : IJwksService
    {
        private readonly IApiClient _apiClient;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="apiClient">Generic API client</param>
        public JwksService(IApiClient apiClient) =>
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));

        /// <inheritdoc />
        public async Task<RSA> GetPublicKey(string keyId, CancellationToken? cancellationToken)
        {
            var response = await _apiClient.Get<JsonWebKeySet>("webhooks/keys", null, cancellationToken ?? CancellationToken.None).ConfigureAwait(false);

            var jwk = response.Keys.
                Single(k =>
                    k.Usage == "sig" &&
                    k.Status == "ACTIVE" &&
                    k.Id == keyId);

            var key = RSA.Create();
            key.ImportParameters(new RSAParameters
            {
                Modulus = GetBytes(jwk.Modulus),
                Exponent = GetBytes(jwk.Exponent)
            });

            return key;
        }

        private static byte[] GetBytes(string base64) =>
            Convert.FromBase64String(base64.Replace('-', '+').Replace('_', '/'));
    }

    /// <summary>
    /// Get public key from JSON Web Key Set endpoint.
    /// </summary>
    public interface IJwksService
    {
        /// <summary>
        /// Get public keys from authorization server.
        /// </summary>
        /// <returns>RSA keys collection</returns>
        Task<RSA> GetPublicKey(string keyId, CancellationToken? cancellationToken = null);
    }
}
