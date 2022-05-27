using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    /// <summary>
    /// Collection meta.
    /// </summary>
    [DataContract]
    public class CollectionMeta<TPaging>
    {
        /// <summary>
        /// Paging information.
        /// </summary>
        [DataMember(Name = "paging", EmitDefaultValue = false)]
        public TPaging Paging { get; set; }
    }
}
