using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    /// <summary>
    /// Resource container.
    /// </summary>
    [DataContract]
    public class Data
    {
        /// <summary>
        /// Resource type.
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// Resource ID.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
    }

    /// <summary>
    /// Resource container.
    /// </summary>
    /// <typeparam name="TAttributes">Resource attribute type</typeparam>
    /// <typeparam name="TMeta">Resource meta type</typeparam>
    /// <typeparam name="TRelationships">Resource relationships type</typeparam>
    /// <typeparam name="TLinks">Resource links type</typeparam>
    [DataContract]
    public class Data<TAttributes, TMeta, TRelationships, TLinks> : Data
    {
        /// <summary>
        /// Resource actual content.
        /// </summary>
        [DataMember(Name = "attributes", EmitDefaultValue = false)]
        public TAttributes Attributes { get; set; }

        /// <summary>
        /// Resource meta.
        /// </summary>
        [DataMember(Name = "meta", EmitDefaultValue = false)]
        public TMeta Meta { get; set; }

        /// <summary>
        /// Resource relationships.
        /// </summary>
        [DataMember(Name = "relationships", EmitDefaultValue = false)]
        public TRelationships Relationships { get; set; }

        /// <summary>
        /// Resource links.
        /// </summary>
        [DataMember(Name = "links", EmitDefaultValue = false)]
        public TLinks Links { get; set; }
    }
}
