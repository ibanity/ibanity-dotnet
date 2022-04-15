using System.Runtime.Serialization;
using Ibanity.Apis.Client.JsonApi;

namespace Ibanity.Apis.Client.Webhooks.Models
{
    /// <summary>
    /// Webhooks body.
    /// </summary>
    /// <typeparam name="TAttributes">Resource attribute type</typeparam>
    /// <typeparam name="TRelationships">Resource relationships type</typeparam>
    [DataContract]
    public abstract class Payload<TAttributes, TRelationships> : Data
    {
        /// <summary>
        /// Attributes of the current resource.
        /// </summary>
        [DataMember(Name = "attributes", EmitDefaultValue = false)]
        public TAttributes Attributes { get; set; }

        /// <summary>
        /// Related resource relationships.
        /// </summary>
        [DataMember(Name = "relationships", EmitDefaultValue = false)]
        public TRelationships Relationships { get; set; }
    }
}
