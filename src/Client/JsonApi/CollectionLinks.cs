using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    [DataContract]
    public class CollectionLinks
    {
        [DataMember(Name = "next", EmitDefaultValue = false)]
        public string Next { get; set; }
    }
}
