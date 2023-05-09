using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.CodaboxConnect.Models
{
    /// <summary>
    /// This resource allows an Accounting Software to retrieve a payroll statement for a client of an accounting office.
    /// </summary>
    [DataContract]
    public class SalesInvoice : Document<Guid>
    {
        /// <summary>
        /// When this sales invoice was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this sales invoice was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Id of the invoicing software of the sales invoice
        /// </summary>
        /// <value>Id of the invoicing software of the sales invoice</value>
        [DataMember(Name = "invoicingSoftware", EmitDefaultValue = false)]
        public string InvoicingSoftware { get; set; }
    }
}
