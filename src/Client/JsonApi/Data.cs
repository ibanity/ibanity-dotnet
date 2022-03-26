using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    [DataContract]
    public class Data<T> where T : class
    {
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string? Type { get; set; }

        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string? Id { get; set; }

        [DataMember(Name = "attributes", EmitDefaultValue = false)]
        public T? Attributes { get; set; }
    }
}
