using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    [DataContract]
    public class Data
    {
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
    }

    [DataContract]
    public class Data<TAttributes, TMeta, TRelationships> : Data
    {
        [DataMember(Name = "attributes", EmitDefaultValue = false)]
        public TAttributes Attributes { get; set; }

        [DataMember(Name = "meta", EmitDefaultValue = false)]
        public TMeta Meta { get; set; }

        [DataMember(Name = "relationships", EmitDefaultValue = false)]
        public TRelationships Relationships { get; set; }
    }
}
