using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Ibanity.Apis.Client.Crypto
{
    public class RsaSsaPssSignature : ISignature
    {
        private static readonly Encoding Encoding = Encoding.UTF8;

        private readonly X509Certificate2 _certificate;

        public RsaSsaPssSignature(X509Certificate2 certificate) =>
            _certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));

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

    public interface ISignature
    {
        string Sign(string value);
    }
}
