using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Http.OAuth2
{
    public static class HttpResponseMessageExtensions
    {
        private const string RequestIdHeader = "ibanity-request-id";

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
                body = await content?.ReadAsStringAsync();

            var requestId = @this.Headers.GetValues(RequestIdHeader).SingleOrDefault();
            var statusCode = @this.StatusCode;
            var errors = serializer.Deserialize<OAuth2Error>(body);

            throw new IbanityOAuth2Exception(requestId, statusCode, errors.ToJsonApi());
        }
    }
}
