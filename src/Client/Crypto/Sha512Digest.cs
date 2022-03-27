using System;
using System.Security.Cryptography;
using System.Text;

namespace Ibanity.Apis.Client.Crypto
{
    public class Sha512Digest : IDigest
    {
        private const string Prefix = "SHA-512=";
        private static readonly Encoding Encoding = Encoding.UTF8;

        public string Compute(string value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var bytes = Encoding.GetBytes(value);

            byte[] digest;
            using (var algorithm = new SHA512Managed())
                digest = algorithm.ComputeHash(bytes);

            return Prefix + Convert.ToBase64String(digest);
        }
    }

    public interface IDigest
    {
        string Compute(string value);
    }
}
