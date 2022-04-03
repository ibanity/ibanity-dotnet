using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    [DataContract]
    public class Collection<T>
    {
        [DataMember(Name = "meta", EmitDefaultValue = false)]
        public CollectionMeta Meta { get; set; }

        [DataMember(Name = "links", EmitDefaultValue = false)]
        public Links Links { get; set; }

        [DataMember(Name = "data", EmitDefaultValue = false)]
        public List<Data<T>> Data { get; set; } = new List<Data<T>>();
    }
}
