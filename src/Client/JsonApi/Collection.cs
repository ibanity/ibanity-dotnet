using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    [DataContract]
    public class Collection<TAttributes, TMeta, TRelationships>
    {
        [DataMember(Name = "meta", EmitDefaultValue = false)]
        public CollectionMeta Meta { get; set; }

        [DataMember(Name = "links", EmitDefaultValue = false)]
        public CollectionLinks Links { get; set; }

        [DataMember(Name = "data", EmitDefaultValue = false)]
        public List<Data<TAttributes, TMeta, TRelationships>> Data { get; set; } = new List<Data<TAttributes, TMeta, TRelationships>>();
    }
}
