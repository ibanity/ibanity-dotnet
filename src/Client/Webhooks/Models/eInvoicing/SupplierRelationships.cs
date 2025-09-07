using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.eInvoicing
{
    /// <summary>
    /// Payload relationships containing a supplier reference.
    /// </summary>
    public class SupplierRelationships
    {
        /// <summary>
        /// A Supplier reference.
        /// </summary>
        [DataMember(Name = "supplier", EmitDefaultValue = false)]
        public Relationship Supplier { get; set; }
    }
}
