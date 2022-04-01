using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Http
{
    public static class HttpResponseMessageExtensions
    {
        private const string RequestIdHeader = "ibanity-request-id";

        public static async Task<HttpResponseMessage> ThrowOnFailure(this HttpResponseMessage @this, ISerializer<string> serializer)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            if (serializer is null)
                throw new ArgumentNullException(nameof(serializer));

            if (@this.IsSuccessStatusCode)
                return @this;

            string body;
            using (var content = @this.Content)
                body = await content?.ReadAsStringAsync();

            var requestId = @this.Headers.TryGetValues(RequestIdHeader, out var values) ? values.SingleOrDefault() : null;
            var statusCode = @this.StatusCode;

            JsonApi.Error errors;
            try
            {
                errors = serializer.Deserialize<JsonApi.Error>(body);
            }
            catch
            {
                errors = null;
            }

            if ((int)@this.StatusCode < 500)
                throw new IbanityClientException(requestId, statusCode, errors);
            else
                throw new IbanityServerException(requestId, statusCode, errors);
        }
    }
}