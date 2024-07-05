using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.eInvoicing
{
    /// <summary>
    /// A webhook payload delivered whenever a peppol registration is updated to registered or registration-failed.
    /// </summary>
    public class PeppolRegistrationsUpdated : JsonApi.Data, IWebhookEvent
    {
        /// <summary>
        /// Unique identifier of the associated supplier.
        /// </summary>
        [DataMember(Name = "supplierId", EmitDefaultValue = false)]
        public Guid SupplierId { get; set; }

        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// A webhook payload delivered whenever a peppol registration is updated to registered or registration-failed.
    /// </summary>
    public class NestedPeppolRegistrationsUpdated : PayloadData<PeppolRegistrationsUpdatedAttributes, PeppolRegistrationsUpdatedRelationships>
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new PeppolRegistrationsUpdated
            {
                Id = Id,
                Type = Type,
                SupplierId = Guid.Parse(Relationships.Supplier.Data.Id),
                CreatedAt = Attributes.CreatedAt
            };
    }

    /// <summary>
    /// Payload attributes delivered whenever a peppol registration is updated to registered or registration-failed.
    /// </summary>
    public class PeppolRegistrationsUpdatedAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// Payload relationships delivered whenever a peppol registration is updated to registered or registration-failed.
    /// </summary>
    public class PeppolRegistrationsUpdatedRelationships
    {
        /// <summary>
        /// A Supplier reference.
        /// </summary>
        [DataMember(Name = "supplier", EmitDefaultValue = false)]
        public Relationship Supplier { get; set; }
    }
}
