using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    [DataContract]
    public class Links
    {
        [DataMember(Name = "next", EmitDefaultValue = false)]
        public string? Next { get; set; }
    }
}
