using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Webhooks.Jwt.Models;

namespace Ibanity.Apis.Client.Webhooks.Jwt
{
    /// <inheritdoc />
    public class Rs512Verifier : IVerifier
    {
        private readonly IParser _parser;
        private readonly IJwksService _jwksService;
        private readonly IClock _clock;
        private readonly TimeSpan _allowedClockSkew;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="parser">JWT parser</param>
        /// <param name="jwksService">JWKS client</param>
        /// <param name="clock">Returns current date and time</param>
        /// <param name="allowedClockSkew">Leniency in date comparisons</param>
        public Rs512Verifier(IParser parser, IJwksService jwksService, IClock clock, TimeSpan allowedClockSkew)
        {
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
            _jwksService = jwksService ?? throw new ArgumentNullException(nameof(jwksService));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
            _allowedClockSkew = allowedClockSkew;
        }

        /// <inheritdoc />
        public async Task<Token> Verify(string token, CancellationToken? cancellationToken)
        {
            var header = _parser.GetHeader(token);

            if (header.Algorithm != "RS512")
                throw new InvalidSignatureException("Only RS512 algorithm is supported but got " + header.Algorithm);

            var publicKey = await _jwksService.GetPublicKey(header.KeyId, cancellationToken);

            byte[] hash;
            using (var sha512 = SHA512.Create())
                hash = sha512.ComputeHash(Encoding.UTF8.GetBytes(_parser.RemoveSignature(token)));

            var rsaDeformatter = new RSAPKCS1SignatureDeformatter(publicKey);
            rsaDeformatter.SetHashAlgorithm("SHA512");

            if (!rsaDeformatter.VerifySignature(hash, _parser.GetSignature(token)))
                throw new InvalidSignatureException("Can't verify signature");

            var payload = _parser.GetPayload<IbanityPayload>(token);

            EnsureClaimsAreValid(payload);

            return new Token
            {
                Header = header,
                Payload = payload
            };
        }

        private void EnsureClaimsAreValid(IbanityPayload payload)
        {
            var now = _clock.Now;

            var notAfter = now + _allowedClockSkew;
            if (payload.IssuedAt > notAfter)
                throw new InvalidSignatureException($"Token issued in the future ({payload.IssuedAt:O} is after {notAfter:O})");

            var notBefore = now - _allowedClockSkew;
            if (payload.Expiration < notBefore)
                throw new InvalidSignatureException($"Expired token ({payload.Expiration:O} is before {notBefore:O})");
        }
    }

    /// <summary>
    /// Verify if JWT token is valid.
    /// </summary>
    public interface IVerifier
    {
        /// <summary>
        /// Verify if JWT token is valid.
        /// </summary>
        /// <param name="token">JWT token</param>
        /// <param name="cancellationToken">Allow to cancel a long-running task</param>
        Task<Token> Verify(string token, CancellationToken? cancellationToken);
    }

    /// <summary>
    /// Parsed JWT token.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Token header.
        /// </summary>
        public Header Header { get; set; }

        /// <summary>
        /// Token payload.
        /// </summary>
        public IbanityPayload Payload { get; set; }
    }
}
