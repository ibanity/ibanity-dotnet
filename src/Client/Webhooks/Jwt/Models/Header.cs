using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Jwt.Models
{
    /// <summary>
    /// Token header.
    /// </summary>
    [DataContract]
    public class Header
    {
        /// <summary>
        /// Signature algorithm.
        /// </summary>
        [DataMember(Name = "alg", EmitDefaultValue = false)]
        public string Algorithm { get; set; }

        /// <summary>
        /// Key ID.
        /// </summary>
        [DataMember(Name = "kid", EmitDefaultValue = false)]
        public string KeyId { get; set; }
    }
}
