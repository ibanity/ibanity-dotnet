using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.eInvoicing
{
    /// <summary>
    /// A webhook payload delivered whenever the technical status of a Peppol inbound document business status changes.
    /// </summary>
    public class NewPeppolInboundDocumentBusinessStatusTechnicalStatus : JsonApi.Data, IWebhookEvent
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
        /// Unique identifier of the associated supplier.
        /// </summary>
        [DataMember(Name = "businessStatusId", EmitDefaultValue = false)]
        public Guid BusinessStatusId { get; set; }

        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// A webhook payload delivered whenever the technical status of a Peppol inbound document business status changes.
    /// </summary>
    public class NestedNewPeppolInboundDocumentBusinessStatusTechnicalStatus : PayloadData<Attributes, NewPeppolInboundDocumentBusinessStatusTechnicalStatusRelationships>
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new NewPeppolInboundDocumentBusinessStatusTechnicalStatus
            {
                Id = Id,
                Type = Type,
                DocumentId = Guid.Parse(Relationships.Document.Data.Id),
                SupplierId = Guid.Parse(Relationships.Supplier.Data.Id),
                BusinessStatusId = Guid.Parse(Relationships.BusinessStatus.Data.Id),
                CreatedAt = Attributes.CreatedAt
            };
    }

    /// <summary>
    /// Payload relationships delivered whenever the technical status of a Peppol inbound document business status changes.
    /// </summary>
    public class NewPeppolInboundDocumentBusinessStatusTechnicalStatusRelationships : DocumentRelationships
    {
        /// <summary>
        /// A business status reference.
        /// </summary>
        [DataMember(Name = "businessStatus", EmitDefaultValue = false)]
        public Relationship BusinessStatus { get; set; }
    }
}
