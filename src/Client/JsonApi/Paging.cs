using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    /// <summary>
    /// Cursor-based paging.
    /// </summary>
    [DataContract]
    public class CursorBasedPaging
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

    /// <summary>
    /// Offset-based paging.
    /// </summary>
    [DataContract]
    public class OffsetBasedPaging
    {
        /// <summary>
        /// Start position of the results by giving the number of records to be skipped.
        /// </summary>
        [DataMember(Name = "offset", EmitDefaultValue = false)]
        public long? Offset { get; set; }

        /// <summary>
        /// Number of total resources in the requested scope.
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public long? Total { get; set; }
    }
}
