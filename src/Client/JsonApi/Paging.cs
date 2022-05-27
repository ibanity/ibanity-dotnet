using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    /// <summary>
    /// Paging.
    /// </summary>
    [DataContract]
    public class Paging
    {
        /// <summary>
        /// Page size.
        /// </summary>
        [DataMember(Name = "limit", EmitDefaultValue = false)]
        public long? Limit { get; set; }

        /// <summary>
        /// Previous item.
        /// </summary>
        [DataMember(Name = "before", EmitDefaultValue = false)]
        public Guid? Before { get; set; }

        /// <summary>
        /// Next item.
        /// </summary>
        [DataMember(Name = "after", EmitDefaultValue = false)]
        public Guid? After { get; set; }
    }
}
