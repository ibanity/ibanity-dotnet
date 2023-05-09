using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.CodaboxConnect.Models
{
    /// <summary>
    /// Resource identifiers of the documents returned by the search.
    /// </summary>
    [DataContract]
    public class Document<TId> : Identified<TId>, IDocument
    {
        /// <inheritdoc/>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <inheritdoc/>
        string IDocument.Id => Id.ToString();

        /// <summary>
        /// Document's owner
        /// </summary>
        [DataMember(Name = "client", EmitDefaultValue = false)]
        public string Client { get; set; }
    }

    /// <summary>
    /// Resource identifiers of the documents returned by the search.
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// The type of the document. Valid values are: creditCardStatement, purchaseInvoice, salesInvoice, payrollStatement, bankAccountStatement.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// The identifier of the document.
        /// </summary>
        string Id { get; }
    }

    /// <summary>
    /// Link to the client that this document belongs to.
    /// </summary>
    [DataContract]
    public class DocumentRelationships
    {
        /// <summary>
        /// Link to the document's owner.
        /// </summary>
        [DataMember(Name = "client", EmitDefaultValue = false)]
        public JsonApi.Relationship Client { get; set; }
    }
}
