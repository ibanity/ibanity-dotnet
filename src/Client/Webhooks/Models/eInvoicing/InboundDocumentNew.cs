using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.eInvoicing
{
    /// <summary>
    /// A webhook payload delivered whenever there is a new document available.
    /// </summary>
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
    public class InboundDocumentNew : JsonApi.Data, IWebhookEvent
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
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
    /// A webhook payload delivered whenever there is a new document available.
    /// </summary>
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
    public class NestedInboundDocumentNew : PayloadData<InboundDocumentNewAttributes, InboundDocumentNewRelationships>
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new InboundDocumentNew
            {
                Id = Id,
                Type = Type,
                DocumentId = Guid.Parse(Relationships.Document.Data.Id),
                SupplierId = Guid.Parse(Relationships.Supplier.Data.Id),
                CreatedAt = Attributes.CreatedAt
            };
    }

    /// <summary>
    /// Payload attributes delivered whenever there is a new document available.
    /// </summary>
    public class InboundDocumentNewAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// Payload relationships delivered whenever there is a new document available.
    /// </summary>
    public class InboundDocumentNewRelationships
    {
        /// <summary>
        /// A Peppol Inbound Document reference.
        /// </summary>
        [DataMember(Name = "document", EmitDefaultValue = false)]
        public Relationship Document { get; set; }

        /// <summary>
        /// A Supplier reference.
        /// </summary>
        [DataMember(Name = "supplier", EmitDefaultValue = false)]
        public Relationship Supplier { get; set; }
    }
}
