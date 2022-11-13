using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    /// <summary>
    /// Resource relationship.
    /// </summary>
    [DataContract]
    public class BaseRelationship
    {
        /// <summary>
        /// Links to other resources.
        /// </summary>
        [DataMember(Name = "links", EmitDefaultValue = false)]
        public Dictionary<string, string> Links { get; set; }
    }

    /// <summary>
    /// Resource relationship.
    /// </summary>
    [DataContract]
    public class Relationship : BaseRelationship
    {
        /// <summary>
        /// Linked resource.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Data Data { get; set; }
    }

    /// <summary>
    /// Resource relationship.
    /// </summary>
    [DataContract]
    public class Relationship<TAttributes> : BaseRelationship
    {
        /// <summary>
        /// Linked resource.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Data<TAttributes, object, object, object> Data { get; set; }
    }

    /// <summary>
    /// Resource relationships.
    /// </summary>
    [DataContract]
    public class Relationships
    {
        /// <summary>
        /// Linked resources.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Data[] Data { get; set; }
    }
}
