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
        /// Link to the first page.
        /// </summary>
        [DataMember(Name = "first", EmitDefaultValue = false)]
        public string First { get; set; }

        /// <summary>
        /// Link to the previous page.
        /// </summary>
        [DataMember(Name = "prev", EmitDefaultValue = false)]
        public string Previous { get; set; }

        /// <summary>
        /// Link to the next page.
        /// </summary>
        [DataMember(Name = "next", EmitDefaultValue = false)]
        public string Next { get; set; }

        /// <summary>
        /// Link to the last page.
        /// </summary>
        [DataMember(Name = "last", EmitDefaultValue = false)]
        public string Last { get; set; }
    }
}
