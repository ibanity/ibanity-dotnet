using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.JsonApi
{
    /// <summary>
    /// Single resource.
    /// </summary>
    /// <typeparam name="TAttributes">Resource attribute type</typeparam>
    /// <typeparam name="TMeta">Resource meta type</typeparam>
    /// <typeparam name="TRelationships">Resource relationships type</typeparam>
    /// <typeparam name="TLinks">Resource links type</typeparam>
    [DataContract]
    public class Resource<TAttributes, TMeta, TRelationships, TLinks>
    {
        /// <summary>
        /// Actual item.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Data<TAttributes, TMeta, TRelationships, TLinks> Data { get; set; }

        /// <summary>
        /// Resource metadata.
        /// </summary>
        [DataMember(Name = "meta", EmitDefaultValue = false)]
        public TMeta Meta { get; set; }
    }
}
