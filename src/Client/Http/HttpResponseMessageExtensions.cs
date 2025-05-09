using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Client.Http
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
        /// <param name="logger">Logger used to log error</param>
        /// <returns>The instance received in argument</returns>
        public static async Task<HttpResponseMessage> ThrowOnFailure(this HttpResponseMessage @this, ISerializer<string> serializer, ILogger logger)
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
                    : await content.ReadAsStringAsync().ConfigureAwait(false);

            var requestId = @this.Headers.TryGetValues(RequestIdHeader, out var values) ? values.SingleOrDefault() : null;
            var statusCode = @this.StatusCode;

            JsonApi.Error errors;
            try
            {
                errors = string.IsNullOrWhiteSpace(body)
                    ? null
                    : serializer.Deserialize<JsonApi.Error>(body);
            }
            catch (Exception e)
            {
                logger.Error($"Can't deserialize error: {e.Message}", e);
                errors = null;
            }

            var exception = (int)@this.StatusCode < 500
                ? (IbanityRequestException)(new IbanityClientException(requestId, statusCode, errors))
                : new IbanityServerException(requestId, statusCode, errors);

            logger.Error(exception.Message, exception);
            throw exception;
        }
    }
}
