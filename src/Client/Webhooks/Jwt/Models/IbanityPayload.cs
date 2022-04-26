using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Jwt.Models
{
    /// <inheritdoc />
    [DataContract]
    public class IbanityPayload : Payload
    {
        /// <summary>
        /// Message digest.
        /// </summary>
        [DataMember(Name = "digest", EmitDefaultValue = false)]
        public string Digest { get; set; }
    }
}
