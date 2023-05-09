using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.CodaboxConnect.Models
{
    /// <summary>
    /// This resource allows an Accounting Software to search for documents of clients of an Accounting Office. Documents can be searched by type, for one or multiple clients. Additionally, it is possible to filter documents within a given period of time. This resource supports pagination.
    /// </summary>
    [DataContract]
    public class DocumentSearch
    {
        /// <summary>
        /// The type of the documents to search. Valid values are: &lt;code&gt;creditCardStatement, purchaseInvoice, salesInvoice, payrollStatement, bankAccountStatement&lt;/code&gt;.
        /// </summary>
        /// <value>The type of the documents to search. Valid values are: &lt;code&gt;creditCardStatement, purchaseInvoice, salesInvoice, payrollStatement, bankAccountStatement&lt;/code&gt;.</value>
        [DataMember(Name = "documentType", EmitDefaultValue = true)]
        public string DocumentType { get; set; }

        /// <summary>
        /// &lt;i&gt;(Optional)&lt;/i&gt; Start of the document period scope, a date-time in the &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; format with offset and zone.  If not specified, all documents from the past will be returned (up to 2 years max). It is advised to fill this based on the &#39;to&#39; date used in the previous call. For more information refer to the &lt;a href&#x3D;&#39;products#accounting-documents&#39;&gt;documents&lt;/a&gt; workflow.
        /// </summary>
        /// <value>&lt;i&gt;(Optional)&lt;/i&gt; Start of the document period scope, a date-time in the &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; format with offset and zone.  If not specified, all documents from the past will be returned (up to 2 years max). It is advised to fill this based on the &#39;to&#39; date used in the previous call. For more information refer to the &lt;a href&#x3D;&#39;products#accounting-documents&#39;&gt;documents&lt;/a&gt; workflow.</value>
        [DataMember(Name = "from", EmitDefaultValue = true)]
        public DateTimeOffset? From { get; set; }

        /// <summary>
        /// &lt;i&gt;(Optional)&lt;/i&gt; End of the document period scope, a date-time in the &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; format with offset and zone. Must be equal to or later than from. If not specified, the current date is used.
        /// </summary>
        /// <value>&lt;i&gt;(Optional)&lt;/i&gt; End of the document period scope, a date-time in the &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; format with offset and zone. Must be equal to or later than from. If not specified, the current date is used.</value>
        [DataMember(Name = "to", EmitDefaultValue = true)]
        public DateTimeOffset? To { get; set; }
    }

    /// <inheritdoc />
    [DataContract]
    public class DocumentSearchResponse : DocumentSearch, IIdentified<Guid>
    {
        /// <inheritdoc />
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// Resource identifiers of the clients used to search for documents. Must contain at least one resource identifier and no more than 5000.
        /// </summary>
        public string[] Clients { get; set; }

        /// <summary>
        /// Resource identifiers of the documents returned by the search.
        /// </summary>
        public IDocument[] Documents { get; set; }
    }

    /// <summary>
    /// Links to the clients and documents.
    /// </summary>
    [DataContract]
    public class DocumentSearchRelationships
    {
        /// <summary>
        /// Resource identifiers of the clients used to search for documents. Must contain at least one resource identifier and no more than 5000.
        /// </summary>
        [DataMember(Name = "clients", EmitDefaultValue = false)]
        public JsonApi.Relationships Clients { get; set; }
    }

    /// <inheritdoc />
    [DataContract]
    public class DocumentSearchRelationshipsResponse : DocumentSearchRelationships
    {
        /// <summary>
        /// Resource identifiers of the documents returned by the search.
        /// </summary>
        [DataMember(Name = "documents", EmitDefaultValue = false)]
        public JsonApi.Relationships Documents { get; set; }
    }

    /// <summary>
    /// Included resources.
    /// </summary>
    [DataContract]
    public class DocumentSearchIncluded
    {
        /// <summary>
        /// Resource identifiers of the documents returned by the search.
        /// </summary>
        [DataMember(Name = "documents", EmitDefaultValue = false)]
        public Data<Document<string>, object, DocumentRelationships, object>[] Documents { get; set; }
    }

    /// <summary>
    /// Full document search resource.
    /// </summary>
    public class DocumentSearchFullResponse : JsonApi.Resource<DocumentSearchResponse, JsonApi.CollectionMeta<JsonApi.CursorBasedPaging>, DocumentSearchRelationshipsResponse, object>
    {
        /// <summary>
        /// Resource identifiers of the documents returned by the search.
        /// </summary>
        [DataMember(Name = "included", EmitDefaultValue = false)]
        public DocumentSearchIncluded Included { get; set; }
    }
}
