using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    [DataContract]
    public class Resource<TAttributes, TMeta, TRelationships, TLinks>
    {
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Data<TAttributes, TMeta, TRelationships, TLinks> Data { get; set; }
    }
}
