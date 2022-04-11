using System.Linq;
using System.Net;
using Ibanity.Apis.Client.JsonApi;

namespace Ibanity.Apis.Client.Http
{
    /// <summary>
    /// Exception occuring when a request is sent to Ibanity.
    /// </summary>
    public abstract class IbanityRequestException : IbanityException
    {
        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="requestId">Ibanity API Request ID</param>
        /// <param name="statusCode">Received HTTP status code</param>
        /// <param name="error">Error details</param>
        public IbanityRequestException(string requestId, HttpStatusCode statusCode, JsonApi.Error error) :
            base($"Request {requestId} failed ({statusCode:D} {statusCode:G}): " + Format(error))
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

        /// <summary>
        /// Ibanity API Request ID
        /// </summary>
        /// <remarks>Providing this identifier with your support request will ensure a faster resolution of your issue.</remarks>
        public string RequestId { get; }

        /// <summary>
        /// Received HTTP status code
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Error details
        /// </summary>
        public Error Error { get; }
    }

    /// <inheritdoc />
    /// <remarks>Related to a 4xx HTTP error that could be solved client-side</remarks>
    public class IbanityClientException : IbanityRequestException
    {
        /// <inheritdoc />
        public IbanityClientException(string requestId, HttpStatusCode statusCode, JsonApi.Error error) :
            base(requestId, statusCode, error)
        { }
    }

    /// <inheritdoc />
    /// <remarks>Related to a 5xx HTTP error that occured server-side</remarks>
    public class IbanityServerException : IbanityRequestException
    {
        /// <inheritdoc />
        public IbanityServerException(string requestId, HttpStatusCode statusCode, JsonApi.Error error) :
            base(requestId, statusCode, error)
        { }
    }
}
