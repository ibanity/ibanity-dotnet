using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    /// <summary>
    /// JSON:API collection.
    /// </summary>
    /// <typeparam name="TAttributes">Items attribute type</typeparam>
    /// <typeparam name="TMeta">Items meta type</typeparam>
    /// <typeparam name="TRelationships">Items relationships type</typeparam>
    /// <typeparam name="TLinks">Items links type</typeparam>
    [DataContract]
#pragma warning disable CA1711 // Keep 'Collection' name as specified in JSON:API
    public class Collection<TAttributes, TMeta, TRelationships, TLinks>
#pragma warning restore CA1711
    {
        /// <summary>
        /// Meta.
        /// </summary>
        [DataMember(Name = "meta", EmitDefaultValue = false)]
        public CollectionMeta Meta { get; set; }

        /// <summary>
        /// Links.
        /// </summary>
        [DataMember(Name = "links", EmitDefaultValue = false)]
        public CollectionLinks Links { get; set; }

        /// <summary>
        /// Items list.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public List<Data<TAttributes, TMeta, TRelationships, TLinks>> Data { get; set; } =
            new List<Data<TAttributes, TMeta, TRelationships, TLinks>>();
    }
}
