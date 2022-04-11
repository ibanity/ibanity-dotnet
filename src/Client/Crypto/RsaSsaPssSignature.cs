using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Ibanity.Apis.Client.Crypto
{
    /// <inheritdoc />
    public class RsaSsaPssSignature : ISignature
    {
        private static readonly Encoding Encoding = Encoding.UTF8;

        private readonly X509Certificate2 _certificate;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="certificate">The certificate that will be used to sign data</param>
        public RsaSsaPssSignature(X509Certificate2 certificate) =>
            _certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));

        /// <inheritdoc />
        public string Sign(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));

            var bytes = Encoding.GetBytes(value);

            byte[] signature;
            using (var privateKey = _certificate.GetRSAPrivateKey())
                signature = privateKey.SignData(bytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);

            return Convert.ToBase64String(signature);
        }
    }

    /// <summary>
    /// Compute signatures from string values.
    /// </summary>
    public interface ISignature
    {
        /// <summary>
        /// Compute a signature from a string.
        /// </summary>
        /// <param name="value">Value to sign</param>
        /// <returns>The signature as base64 string</returns>
        string Sign(string value);
    }
}
