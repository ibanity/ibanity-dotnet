using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.eInvoicing
{
    /// <summary>
    /// A webhook payload delivered whenever the status of a Peppol outbound document business status changes.
    /// </summary>
    public class NewPeppolOutboundDocumentBusinessStatus : JsonApi.Data, IWebhookEvent
    {
        /// <summary>
        /// Unique identifier of the associated document.
        /// </summary>
        [DataMember(Name = "documentId", EmitDefaultValue = false)]
        public Guid DocumentId { get; set; }

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
    /// A webhook payload delivered whenever the status of a Peppol outbound document business status changes.
    /// </summary>
    public class NestedNewPeppolOutboundDocumentBusinessStatus : PayloadData<Attributes, DocumentRelationships>
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new NewPeppolOutboundDocumentBusinessStatus
            {
                Id = Id,
                Type = Type,
                DocumentId = Guid.Parse(Relationships.Document.Data.Id),
                SupplierId = Guid.Parse(Relationships.Supplier.Data.Id),
                CreatedAt = Attributes.CreatedAt
            };
    }
}
