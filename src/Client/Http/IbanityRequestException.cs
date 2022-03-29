using System.Linq;
using System.Net;
using Ibanity.Apis.Client.JsonApi;

namespace Ibanity.Apis.Client.Http
{
    public abstract class IbanityRequestException : IbanityException
    {
        public IbanityRequestException(string requestId, HttpStatusCode statusCode, JsonApi.Error error) :
            base($"Request '{requestId}' failed ({statusCode:D} {statusCode:G}): " + Format(error))
        {
            RequestId = requestId;
            StatusCode = statusCode;
            Error = error;
        }

        private static string Format(Error error)
        {
            if (error?.Errors?.Any() ?? false)
                return string.Join(" - ", error.Errors.Select(e => $"{e.Code} ({e.Detail})"));

            return "Unspecified";
        }

        public string RequestId { get; }
        public HttpStatusCode StatusCode { get; }
        public Error Error { get; }
    }

    public class IbanityClientException : IbanityRequestException
    {
        public IbanityClientException(string requestId, HttpStatusCode statusCode, JsonApi.Error error) :
            base(requestId, statusCode, error)
        { }
    }

    public class IbanityServerException : IbanityRequestException
    {
        public IbanityServerException(string requestId, HttpStatusCode statusCode, JsonApi.Error error) :
            base(requestId, statusCode, error)
        { }
    }
}
