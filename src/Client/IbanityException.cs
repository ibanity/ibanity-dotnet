using System;
using System.Net;
using Ibanity.Apis.Client.JsonApi;

namespace Ibanity.Apis.Client
{
    public class IbanityException : ApplicationException
    {
        public IbanityException(string message) : base(message) { }
    }

    public abstract class IbanityRequestException : IbanityException
    {
        public IbanityRequestException(string requestId, HttpStatusCode statusCode, JsonApi.Error error) :
            base($"Request '{requestId}' failed: {statusCode:G}")
        {
            RequestId = requestId;
            StatusCode = statusCode;
            Error = error;
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
