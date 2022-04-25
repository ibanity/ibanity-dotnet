using System.Net;

namespace Ibanity.Apis.Client.Http.OAuth2
{
    /// <summary>
    /// Exception occurring when authenticating to Ibanity.
    /// </summary>
    public class IbanityOAuth2Exception : IbanityRequestException
    {
        /// <inheritdoc />
        public IbanityOAuth2Exception(string requestId, HttpStatusCode statusCode, JsonApi.Error error) :
            base(requestId, statusCode, error)
        { }
    }
}
