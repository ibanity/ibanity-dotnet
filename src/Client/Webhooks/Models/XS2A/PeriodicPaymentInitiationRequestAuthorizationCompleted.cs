using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.XS2A
{
    /// <summary>
    /// A webhook payload delivered whenever a periodic payment initiation request authorization completes.
    /// </summary>
    public class PeriodicPaymentInitiationRequestAuthorizationCompleted : JsonApi.Data, IWebhookEvent
    {
        /// <summary>
        /// Unique identifier of the associated periodic payment initiation request.
        /// </summary>
        [DataMember(Name = "periodicPaymentInitiationRequestId", EmitDefaultValue = false)]
        public Guid PeriodicPaymentInitiationRequestId { get; set; }

        /// <summary>
        /// Current status of the periodic payment initiation request.
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
    /// A webhook payload delivered whenever a periodic payment initiation request is completed.
    /// </summary>
    public class NestedPeriodicPaymentInitiationRequestAuthorizationCompleted : PayloadData<PeriodicPaymentInitiationRequestAuthorizationCompletedAttributes, PeriodicPaymentInitiationRequestAuthorizationCompletedRelationships>
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new PeriodicPaymentInitiationRequestAuthorizationCompleted
            {
                Id = Id,
                Type = Type,
                PeriodicPaymentInitiationRequestId = Guid.Parse(Relationships.PeriodicPaymentInitiationRequest.Data.Id),
                Status = Attributes.Status,
                CreatedAt = Attributes.CreatedAt
            };
    }

    /// <summary>
    /// Payload attributes delivered whenever a periodic payment initiation request is completed.
    /// </summary>
    public class PeriodicPaymentInitiationRequestAuthorizationCompletedAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Current status of the periodic payment initiation request.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }
    }

    /// <summary>
    /// Payload relationships delivered whenever a periodic payment initiation request is completed.
    /// </summary>
    public class PeriodicPaymentInitiationRequestAuthorizationCompletedRelationships
    {

        /// <summary>
        /// Details about the associated periodic payment initiation request.
        /// </summary>
        [DataMember(Name = "periodicPaymentInitiationRequest", EmitDefaultValue = false)]
        public Relationship PeriodicPaymentInitiationRequest { get; set; }
    }
}
