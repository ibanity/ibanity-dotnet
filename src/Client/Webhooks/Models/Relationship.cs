using System.Runtime.Serialization;
using Ibanity.Apis.Client.JsonApi;

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
        public Data Data { get; set; }
    }
}
