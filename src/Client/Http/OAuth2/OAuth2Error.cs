using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Http.OAuth2
{
    /// <summary>
    /// Payload received on authentication errors
    /// </summary>
    [DataContract]
    public class OAuth2Error
    {
        /// <summary>
        /// Error code.
        /// </summary>
        [DataMember(Name = "error")]
        public string Error { get; set; }

        /// <summary>
        /// Error description.
        /// </summary>
        [DataMember(Name = "error_description")]
        public string Description { get; set; }

        /// <summary>
        /// Convert to standard error format.
        /// </summary>
        /// <returns>JSON:API error</returns>
        public JsonApi.Error ToJsonApi() => new JsonApi.Error
        {
            Errors = new List<JsonApi.ErrorItem>
            {
                new JsonApi.ErrorItem
                {
                    Code = Error,
                    Detail = Description
                }
            }
        };
    }
}
