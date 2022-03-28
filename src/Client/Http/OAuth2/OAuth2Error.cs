using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Http.OAuth2
{
    [DataContract]
    public class OAuth2Error
    {
        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "error_description")]
        public string Description { get; set; }

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
