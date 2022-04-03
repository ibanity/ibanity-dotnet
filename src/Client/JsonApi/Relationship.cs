using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    [DataContract]
    public class Relationship
    {
        [DataMember(Name = "links", EmitDefaultValue = false)]
        public Dictionary<string, string> Links { get; set; }

        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Data Data { get; set; }
    }
}
