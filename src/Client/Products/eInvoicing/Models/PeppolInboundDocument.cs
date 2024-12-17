using System;
using System.Globalization;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.eInvoicing.Models
{
    /// <summary>
    /// Peppol Inbound Document
    /// </summary>
    [DataContract]
    public class PeppolInboundDocument : Identified<Guid>
    {
        /// <summary>
        /// When this peppol document was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this peppol document was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public string CreatedAtString { get; set; }

        /// <summary>
        /// When this peppol inbound document was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this peppol inbound document was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        public DateTimeOffset CreatedAt =>
            DateTimeOffset.TryParse(CreatedAtString, out var createdDate)
                ? createdDate
                : DateTimeOffset.ParseExact(CreatedAtString.Replace(" 00:00", "+00:00"), "yyyy-MM-ddTHH:mm:ss.fffffffzzz", CultureInfo.InvariantCulture);

        /// <summary>
        /// This is a unique identifier used within the Peppol network. In case of an issue this can be used in communication with the sending party.
        /// </summary>
        [DataMember(Name = "transmissionId", EmitDefaultValue = true)]
        public string TransmissionId { get; set; }

        /// <summary>
        /// ID of the supplier that this document belongs to.
        /// </summary>
        public Guid SupplierId { get; set; }
    }

    /// <summary>
    /// Link to the supplier.
    /// </summary>
    [DataContract]
    public class PeppolInboundDocumentRelationships
    {
        /// <summary>
        /// Link to the supplier.
        /// </summary>
        [DataMember(Name = "supplier", EmitDefaultValue = false)]
        public JsonApi.Relationship Supplier { get; set; }
    }
}
