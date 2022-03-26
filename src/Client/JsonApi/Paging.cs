using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    [DataContract]
    public class Paging
    {
        [DataMember(Name = "limit", EmitDefaultValue = false)]
        public long Limit { get; set; }

        [DataMember(Name = "before", EmitDefaultValue = false)]
        public string Before { get; set; }

        [DataMember(Name = "after", EmitDefaultValue = false)]
        public string After { get; set; }
    }
}
