using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    [DataContract]
    public class Resource<T>
    {
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Data<T> Data { get; set; }
    }
}
