using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.eInvoicing
{
    /// <summary>
    /// Payload relationships containing supplier and document references.
    /// </summary>
    public class DocumentRelationships : SupplierRelationships
    {
        /// <summary>
        /// A Peppol Inbound Document reference.
        /// </summary>
        [DataMember(Name = "document", EmitDefaultValue = false)]
        public Relationship Document { get; set; }
    }
}
