using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.CodaboxConnect.Models
{
    /// <summary>
    /// Resource identifiers of the documents returned by the search.
    /// </summary>
    public class Document<TId> : Identified<TId>, IDocument
    {
        /// <inheritdoc/>
        public string Type { get; set; }

        /// <inheritdoc/>
        string IDocument.Id => Id.ToString();
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
}
