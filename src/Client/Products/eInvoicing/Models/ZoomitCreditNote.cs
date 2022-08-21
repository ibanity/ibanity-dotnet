using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.eInvoicing.Models
{
    /// <summary>
    /// <p>This is an object representing the credit note that can be sent by a supplier. This document is always an UBL in BIS 3 format with additional validations.</p>
    /// <p>CodaBox expects the following format for Zoomit credit notes: <see href="http://docs.peppol.eu/poacc/billing/3.0/">Peppol BIS 3.0</see></p>
    /// <p>CodaBox will verify the compliance of the UBL with XSD and schematron rules (you can find the CodaBox schematron rules <see href="https://documentation.ibanity.com/einvoicing/ZOOMIT-EN16931-UBL.sch">here</see>)</p>
    /// <p>In order to send a credit note to Zoomit, some additional fields are required</p>
    /// </summary>
    [DataContract]
    public class ZoomitCreditNote : Identified<Guid>
    {
        /// <summary>
        /// When this zoomit credit note was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this zoomit credit note was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// &lt;p&gt;The status of the credit note.&lt;/p&gt;&lt;p&gt;Possible values&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;created&lt;/code&gt; The credit note was successfully received by CodaBox and will be processed.&lt;/li&gt;&lt;li&gt;&lt;code&gt;sending&lt;/code&gt; The credit note is valid and will be sent to the customer.&lt;/li&gt;&lt;li&gt;&lt;code&gt;sent&lt;/code&gt; The credit note is available for the customer in Zoomit. In this case you receive a transactionId.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid&lt;/code&gt; The credit note is not valid, you will receive an error object containing a code and a message explaining what went wrong (see below).&lt;/li&gt;&lt;li&gt;&lt;code&gt;send-error&lt;/code&gt; The credit note could not be sent to the customer, you will receive an error object containing a code and a message explaining what went wrong (see below).    &lt;/li&gt;&lt;/ul&gt;&lt;p&gt;In case of an unspecified error or an issue with the receiving access point, you can try to resend the credit note. CodaBox will not automatically resend the credit note, send-error is a final state of a document.&lt;/p&gt;
        /// </summary>
        /// <value>&lt;p&gt;The status of the credit note.&lt;/p&gt;&lt;p&gt;Possible values&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;created&lt;/code&gt; The credit note was successfully received by CodaBox and will be processed.&lt;/li&gt;&lt;li&gt;&lt;code&gt;sending&lt;/code&gt; The credit note is valid and will be sent to the customer.&lt;/li&gt;&lt;li&gt;&lt;code&gt;sent&lt;/code&gt; The credit note is available for the customer in Zoomit. In this case you receive a transactionId.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid&lt;/code&gt; The credit note is not valid, you will receive an error object containing a code and a message explaining what went wrong (see below).&lt;/li&gt;&lt;li&gt;&lt;code&gt;send-error&lt;/code&gt; The credit note could not be sent to the customer, you will receive an error object containing a code and a message explaining what went wrong (see below).    &lt;/li&gt;&lt;/ul&gt;&lt;p&gt;In case of an unspecified error or an issue with the receiving access point, you can try to resend the credit note. CodaBox will not automatically resend the credit note, send-error is a final state of a document.&lt;/p&gt;</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Identifier for the associated transaction
        /// </summary>
        /// <value>Identifier for the associated transaction</value>
        [DataMember(Name = "transactionId", EmitDefaultValue = false)]
        public Guid TransactionId { get; set; }
    }
}
