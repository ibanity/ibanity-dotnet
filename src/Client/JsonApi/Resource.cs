using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    [DataContract]
    public class Resource<TAttributes, TMeta, TRelationships>
    {
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Data<TAttributes, TMeta, TRelationships> Data { get; set; }
    }
}
