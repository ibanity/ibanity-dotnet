using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    [DataContract]
    public class Meta
    {
        [DataMember(Name = "paging", EmitDefaultValue = false)]
        public Paging Paging { get; set; }
    }
}
