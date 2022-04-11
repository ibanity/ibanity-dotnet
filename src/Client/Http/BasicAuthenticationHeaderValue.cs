using System;
using System.Net.Http.Headers;
using System.Text;

namespace Ibanity.Apis.Client.Http
{
    /// <summary>
    /// Authentication header with user and password as base64 string.
    /// </summary>
    public class BasicAuthenticationHeaderValue : AuthenticationHeaderValue
    {
        private static readonly Encoding Encoding = Encoding.ASCII;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="username">Username to encode</param>
        /// <param name="password">Password to encode</param>
        public BasicAuthenticationHeaderValue(string username, string password)
            : base("Basic", Convert.ToBase64String(Encoding.GetBytes($"{username}:{password}")))
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException($"'{nameof(username)}' cannot be null or whitespace.", nameof(username));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException($"'{nameof(password)}' cannot be null or whitespace.", nameof(password));
        }
    }
}
