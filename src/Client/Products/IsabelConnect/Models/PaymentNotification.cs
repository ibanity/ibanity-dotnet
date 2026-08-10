using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.IsabelConnect.Models
{
    /// <summary>
    /// This is an object representing a payment notification.
    /// </summary>
    [DataContract]
    public class PaymentNotification : Identified<string>
    {
        /// <summary>
        /// Type of the payment notification (e.g. <c>payment.status.updated</c>)
        /// </summary>
        [DataMember(Name = "notificationType", EmitDefaultValue = false)]
        public string NotificationType { get; set; }

        /// <summary>
        /// When the notification was created, in ISO8601 format
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public string CreatedAt { get; set; }

        /// <inheritdoc />
        public override string ToString() => $"PaymentNotification {Id}";
    }
}
