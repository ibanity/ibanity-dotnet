using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.XS2A
{
    /// <summary>
    /// A webhook payload delivered whenever a bulk payment initiation request status is updated.
    /// </summary>
    public class BulkPaymentInitiationRequestStatusUpdated : JsonApi.Data, IWebhookEvent
    {
        /// <summary>
        /// Unique identifier of the associated bulk payment initiation request.
        /// </summary>
        [DataMember(Name = "bulkPaymentInitiationRequestId", EmitDefaultValue = false)]
        public Guid BulkPaymentInitiationRequestId { get; set; }

        /// <summary>
        /// Current status of the bulk payment initiation request.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// A webhook payload delivered whenever a bulk payment initiation request is completed.
    /// </summary>
    public class NestedBulkPaymentInitiationRequestStatusUpdated : PayloadData<BulkPaymentInitiationRequestStatusUpdatedAttributes, BulkPaymentInitiationRequestStatusUpdatedRelationships>
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new BulkPaymentInitiationRequestStatusUpdated
            {
                Id = Id,
                Type = Type,
                BulkPaymentInitiationRequestId = Guid.Parse(Relationships.BulkPaymentInitiationRequest.Data.Id),
                Status = Attributes.Status,
                CreatedAt = Attributes.CreatedAt
            };
    }

    /// <summary>
    /// Payload attributes delivered whenever a bulk payment initiation request is completed.
    /// </summary>
    public class BulkPaymentInitiationRequestStatusUpdatedAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Current status of the bulk payment initiation request.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }
    }

    /// <summary>
    /// Payload relationships delivered whenever a bulk payment initiation request is completed.
    /// </summary>
    public class BulkPaymentInitiationRequestStatusUpdatedRelationships

        /// <summary>
        /// Details about the associated bulk payment initiation request.
        /// </summary>
        [DataMember(Name = "bulkPaymentInitiationRequest", EmitDefaultValue = false)]
        public Relationship BulkPaymentInitiationRequest { get; set; }
    }
}
