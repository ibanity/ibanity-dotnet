using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.eInvoicing
{
    /// <summary>
    /// Payload attributes delivered whenever an eInvoicing is received.
    /// </summary>
    public class Attributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }
}
