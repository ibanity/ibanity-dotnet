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
    public class NestedPeppolRegistrationsUpdated : PayloadData<Attributes, SupplierRelationships>
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
}
