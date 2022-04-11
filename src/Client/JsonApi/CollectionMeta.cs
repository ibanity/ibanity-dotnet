using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    /// <summary>
    /// Collection meta.
    /// </summary>
    [DataContract]
    public class CollectionMeta
    {
        /// <summary>
        /// Paging information.
        /// </summary>
        [DataMember(Name = "paging", EmitDefaultValue = false)]
        public Paging Paging { get; set; }
    }
}
