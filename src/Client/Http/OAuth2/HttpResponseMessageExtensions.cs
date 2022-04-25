using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Http.OAuth2
{
    /// <summary>
    /// Add a few methods to <see cref="HttpResponseMessage" />.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        private const string RequestIdHeader = "ibanity-request-id";

        /// <summary>
        /// <para>Checks status code and throws an exception if an error occurred.</para>
        /// <para>The error payload will be contained inside the exception.</para>
        /// </summary>
        /// <param name="this"><see cref="HttpResponseMessage" /> instance</param>
        /// <param name="serializer">To-string serializer</param>
        /// <returns>The instance received in argument</returns>
        public static async Task<HttpResponseMessage> ThrowOnOAuth2Failure(this HttpResponseMessage @this, ISerializer<string> serializer)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            if (serializer is null)
                throw new ArgumentNullException(nameof(serializer));

            if (@this.IsSuccessStatusCode)
                return @this;

            string body;
            using (var content = @this.Content)
                body = content == null
                    ? null
                    : await content.ReadAsStringAsync();

            var requestId = @this.Headers.GetValues(RequestIdHeader).SingleOrDefault();
            var statusCode = @this.StatusCode;

            var errors = string.IsNullOrWhiteSpace(body)
                ? null
                : serializer.Deserialize<OAuth2Error>(body);

            throw new IbanityOAuth2Exception(requestId, statusCode, errors?.ToJsonApi());
        }
    }
}
