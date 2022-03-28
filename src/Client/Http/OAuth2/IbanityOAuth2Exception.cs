using System.Net;

namespace Ibanity.Apis.Client.Http.OAuth2
{
    public class IbanityOAuth2Exception : IbanityRequestException
    {
        public IbanityOAuth2Exception(string requestId, HttpStatusCode statusCode, JsonApi.Error error) :
            base(requestId, statusCode, error)
        { }
    }
}
