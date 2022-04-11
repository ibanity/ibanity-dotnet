using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    /// <summary>
    /// Collection links (next, first, ...).
    /// </summary>
    [DataContract]
    public class CollectionLinks
    {
        /// <summary>
        /// Next page.
        /// </summary>
        [DataMember(Name = "next", EmitDefaultValue = false)]
        public string Next { get; set; }
    }
}
