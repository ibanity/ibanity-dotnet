using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models
{
    /// <summary>
    /// Webhook relationship.
    /// </summary>
    public class Relationship
    {
        /// <summary>
        /// Relationship object.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public JsonApi.Data Data { get; set; }
    }
}
