using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Ibanity.Apis.Client.Crypto
{
    /// <inheritdoc />
    public class Sha512Digest : IDigest
    {
        private const string Prefix = "SHA-512=";
        private static readonly Encoding Encoding = Encoding.UTF8;

        /// <inheritdoc />
        public string Compute(Stream value)
        {
            byte[] digest;
            using (var algorithm = new SHA512Managed())
                digest = value == null
                    ? algorithm.ComputeHash(Encoding.GetBytes(string.Empty))
                    : algorithm.ComputeHash(value);

            return Prefix + Convert.ToBase64String(digest);
        }
    }

    /// <summary>
    /// Compute digests from string values.
    /// </summary>
    public interface IDigest
    {
        /// <summary>
        /// Compute a digest from a string.
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The digest as base64 string</returns>
        string Compute(Stream value);
    }
}
