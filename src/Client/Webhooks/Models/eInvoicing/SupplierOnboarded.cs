using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.eInvoicing
{
    /// <summary>
    /// A webhook payload delivered whenever the supplier is onboarded, i.e., its status is updated to ONBOARDED.
    /// </summary>
    public class SupplierOnboarded : JsonApi.Data, IWebhookEvent
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
    /// A webhook payload delivered whenever the supplier is onboarded, i.e., its status is updated to ONBOARDED.
    /// </summary>
    public class NestedSupplierOnboarded : PayloadData<Attributes, SupplierRelationships>
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new SupplierOnboarded
            {
                Id = Id,
                Type = Type,
                SupplierId = Guid.Parse(Relationships.Supplier.Data.Id),
                CreatedAt = Attributes.CreatedAt
            };
    }
}
