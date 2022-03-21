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
            _certificate = certificate;

        public string Sign(string value)
        {
            var bytes = Encoding.GetBytes(value);

            using var privateKey = _certificate.GetRSAPrivateKey();
            var signature = privateKey.SignData(bytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);

            return Convert.ToBase64String(signature);
        }
    }

    public interface ISignature
    {
        string Sign(string value);
    }
}
