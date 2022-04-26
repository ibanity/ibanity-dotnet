using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Webhooks.Jwt.Models;

namespace Ibanity.Apis.Client.Webhooks.Jwt
{
    /// <inheritdoc />
    public class Rs512Verifier : IVerifier
    {
        private readonly IParser _parser;
        private readonly IJwksService _jwksService;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="parser">JWT parser</param>
        /// <param name="jwksService">JWKS client</param>
        public Rs512Verifier(IParser parser, IJwksService jwksService)
        {
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
            _jwksService = jwksService ?? throw new ArgumentNullException(nameof(jwksService));
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
                throw new InvalidSignatureException("Hash did not match expected value");

            return new Token
            {
                Header = header,
                Payload = _parser.GetPayload<IbanityPayload>(token)
            };
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
