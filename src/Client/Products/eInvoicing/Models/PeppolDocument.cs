using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.eInvoicing.Models
{
    /// <summary>
    /// Peppol document
    /// </summary>
    [DataContract]
    public class PeppolDocument
    {
        /// <summary>
        /// When this peppol document was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this peppol document was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// &lt;p&gt;The status of the document.&lt;/p&gt;&lt;p&gt;Possible values&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;created&lt;/code&gt; The document was successfully received by CodaBox and will be processed.&lt;/li&gt;&lt;li&gt;&lt;code&gt;sending&lt;/code&gt; The document is valid and will be sent to the customer.&lt;/li&gt;&lt;li&gt;&lt;code&gt;sent&lt;/code&gt; The document is available for the customer in Zoomit. In this case you receive a transactionId.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid&lt;/code&gt; The document is not valid, you will receive an error object containing a code and a message explaining what went wrong (see below).&lt;/li&gt;&lt;li&gt;&lt;code&gt;send-error&lt;/code&gt; The document could not be sent to the customer, you will receive an error object containing a code and a message explaining what went wrong (see below).    &lt;/li&gt;&lt;/ul&gt;&lt;p&gt;In case of an unspecified error or an issue with the receiving access point, you can try to resend the the document. CodaBox will not automatically resend the the document, send-error is a final state of a document.&lt;/p&gt;
        /// </summary>
        /// <value>&lt;p&gt;The status of the document.&lt;/p&gt;&lt;p&gt;Possible values&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;created&lt;/code&gt; The document was successfully received by CodaBox and will be processed.&lt;/li&gt;&lt;li&gt;&lt;code&gt;sending&lt;/code&gt; The document is valid and will be sent to the customer.&lt;/li&gt;&lt;li&gt;&lt;code&gt;sent&lt;/code&gt; The document is available for the customer in Zoomit. In this case you receive a transactionId.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid&lt;/code&gt; The document is not valid, you will receive an error object containing a code and a message explaining what went wrong (see below).&lt;/li&gt;&lt;li&gt;&lt;code&gt;send-error&lt;/code&gt; The document could not be sent to the customer, you will receive an error object containing a code and a message explaining what went wrong (see below).    &lt;/li&gt;&lt;/ul&gt;&lt;p&gt;In case of an unspecified error or an issue with the receiving access point, you can try to resend the the document. CodaBox will not automatically resend the the document, send-error is a final state of a document.&lt;/p&gt;</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }
    }
}
