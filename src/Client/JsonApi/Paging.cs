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

    /// <summary>
    /// Page-based paging.
    /// </summary>
    [DataContract]
    public class PageBasedPaging
    {
        /// <summary>
        /// Index of the results page.
        /// </summary>
        [DataMember(Name = "number", EmitDefaultValue = false)]
        public long? Number { get; set; }

        /// <summary>
        /// Number of returned resources.
        /// </summary>
        [DataMember(Name = "size", EmitDefaultValue = false)]
        public int? Size { get; set; }

        /// <summary>
        /// Number of total resources in the requested scope.
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public long? Total { get; set; }

        /// <summary>
        /// Number of total resources in the requested scope.
        /// </summary>
        [DataMember(Name = "totalEntries", EmitDefaultValue = false)]
        public long? TotalEntries { get; set; }

        /// <summary>
        /// Number of total pages in the requested scope.
        /// </summary>
        [DataMember(Name = "totalPages", EmitDefaultValue = false)]
        public long? TotalPages { get; set; }
    }
}
