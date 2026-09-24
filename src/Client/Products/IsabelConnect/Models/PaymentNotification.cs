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
        /// Identifier of the Isabel notification.
        /// </summary>
        [DataMember(Name = "NotificationId", EmitDefaultValue = false)]
        public string NotificationId { get; set; }

        /// <summary>
        /// When the notification was created, in ISO8601 format
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public string CreatedAt { get; set; }

        /// <summary>
        /// ID of the bulk payment this notification relates to
        /// </summary>
        public string PaymentId { get; set; }

        /// <inheritdoc />
        public override string ToString() => $"PaymentNotification {Id}";
    }

    /// <summary>
    /// Link to the bulk payment this notification relates to.
    /// </summary>
    [DataContract]
    public class PaymentNotificationRelationships
    {
        /// <summary>
        /// Link to the bulk payment this notification relates to.
        /// </summary>
        [DataMember(Name = "payment", EmitDefaultValue = false)]
        public JsonApi.Relationship Payment { get; set; }
    }
}
