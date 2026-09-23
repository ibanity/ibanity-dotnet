using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.IsabelConnect
{
    /// <summary>
    /// A webhook payload delivered whenever a payment lifecycle event occurs (BF status change, ISANOT received, or pain.002 received).
    /// </summary>
    public class PaymentStatusUpdated : JsonApi.Data, IWebhookEvent
    {
        /// <summary>
        /// Identifier of the Isabel notification.
        /// </summary>
        [DataMember(Name = "notificationId", EmitDefaultValue = false)]
        public string NotificationId { get; set; }

        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Identifier of the associated bulk payment.
        /// </summary>
        [DataMember(Name = "paymentId", EmitDefaultValue = false)]
        public string PaymentId { get; set; }
    }

    /// <summary>
    /// A webhook payload delivered whenever a payment lifecycle event occurs.
    /// </summary>
    public class NestedPaymentStatusUpdated : PayloadData<PaymentStatusUpdatedAttributes, PaymentStatusUpdatedRelationships>
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new PaymentStatusUpdated
            {
                Id = Id,
                Type = Type,
                NotificationId = Attributes.NotificationId,
                CreatedAt = Attributes.CreatedAt,
                PaymentId = Relationships.Payment.Data.Id
            };
    }

    /// <summary>
    /// Payload attributes delivered whenever a payment lifecycle event occurs.
    /// </summary>
    public class PaymentStatusUpdatedAttributes
    {
        /// <summary>
        /// Identifier of the Isabel notification.
        /// </summary>
        [DataMember(Name = "notificationId", EmitDefaultValue = false)]
        public string NotificationId { get; set; }

        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// Payload relationships delivered whenever a payment lifecycle event occurs.
    /// </summary>
    public class PaymentStatusUpdatedRelationships
    {
        /// <summary>
        /// Details about the associated bulk payment.
        /// </summary>
        [DataMember(Name = "payment", EmitDefaultValue = false)]
        public Relationship Payment { get; set; }
    }
}
