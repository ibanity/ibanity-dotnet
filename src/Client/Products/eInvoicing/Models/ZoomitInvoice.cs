using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.eInvoicing.Models
{
    /// <summary>
    /// <p>This is an object representing the invoice that can be sent by a supplier. This document is always an UBL in BIS 3 format with additional validations.</p>
    /// <p>CodaBox expects the following format for Zoomit invoices: <see href="http://docs.peppol.eu/poacc/billing/3.0/">Peppol BIS 3.0</see></p>
    /// <p>CodaBox will verify the compliance of the UBL with XSD and schematron rules (you can find the CodaBox schematron rules <see href="https://documentation.ibanity.com/einvoicing/ZOOMIT-EN16931-UBL.sch">here</see>)</p>
    /// <p>In order to send an invoice to Zoomit, some additional fields are required</p>
    /// </summary>
    [DataContract]
    public class ZoomitInvoice : Identified<Guid>
    {
        /// <summary>
        /// When this zoomit invoice was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this zoomit invoice was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// This is the reachability status of a customer.
        /// </summary>
        /// <value>This is the reachability status of a customer.</value>
        [DataMember(Name = "customerStatus", EmitDefaultValue = false)]
        public string CustomerStatus { get; set; }

        /// <summary>
        /// &lt;p&gt;Provides extra information to help you identify what went wrong.&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;invalid-malicious&lt;/code&gt; The integrity of the document could not be verified.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid-format&lt;/code&gt; The format of the document is not supported or the document is not a credit note.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid-xsd&lt;/code&gt; The XSD validation failed for the document.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid-schematron&lt;/code&gt; The schematron validation failed for the document.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid-identifiers&lt;/code&gt; The supplier data in the document could not be verified.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid-size&lt;/code&gt; The size of the document exceeds 100MB.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid-type&lt;/code&gt; The document is not an xml.&lt;/li&gt;&lt;li&gt;&lt;code&gt;error-customer-not-registered&lt;/code&gt; The receiver is not reachable on Zoomit.&lt;/li&gt;&lt;li&gt;&lt;code&gt;error-general&lt;/code&gt;Unspecified error.&lt;/li&gt;&lt;/ul&gt;
        /// </summary>
        /// <value>&lt;p&gt;Provides extra information to help you identify what went wrong.&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;invalid-malicious&lt;/code&gt; The integrity of the document could not be verified.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid-format&lt;/code&gt; The format of the document is not supported or the document is not a credit note.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid-xsd&lt;/code&gt; The XSD validation failed for the document.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid-schematron&lt;/code&gt; The schematron validation failed for the document.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid-identifiers&lt;/code&gt; The supplier data in the document could not be verified.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid-size&lt;/code&gt; The size of the document exceeds 100MB.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid-type&lt;/code&gt; The document is not an xml.&lt;/li&gt;&lt;li&gt;&lt;code&gt;error-customer-not-registered&lt;/code&gt; The receiver is not reachable on Zoomit.&lt;/li&gt;&lt;li&gt;&lt;code&gt;error-general&lt;/code&gt;Unspecified error.&lt;/li&gt;&lt;/ul&gt;</value>
        [DataMember(Name = "errors", EmitDefaultValue = false)]
        public Object Errors { get; set; }

        /// <summary>
        /// &lt;p&gt;The status of the invoice.&lt;/p&gt;&lt;p&gt;Possible values&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;created&lt;/code&gt; The invoice was successfully received by CodaBox and will be processed.&lt;/li&gt;&lt;li&gt;&lt;code&gt;sending&lt;/code&gt; The invoice is valid and will be sent to the customer.&lt;/li&gt;&lt;li&gt;&lt;code&gt;sent&lt;/code&gt; The invoice note is available for the customer in Zoomit. In this case you receive a transactionId.&lt;/li&gt;&lt;li&gt;&lt;code&gt;paid&lt;/code&gt; The user initiated the payment through Zoomit.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid&lt;/code&gt; The invoice is not valid, you will receive an error object containing a code and a message explaining what went wrong (see below).&lt;/li&gt;&lt;li&gt;&lt;code&gt;send-error&lt;/code&gt; The invoice could not be sent to the customer, you will receive an error object containing a code and a message explaining what went wrong (see below).    &lt;/li&gt;&lt;/ul&gt;&lt;p&gt;In case of an unspecified error or an issue with the receiving access point, you can try to resend the invoice. CodaBox will not automatically resend the invoice, send-error is a final state of a document.&lt;/p&gt;
        /// </summary>
        /// <value>&lt;p&gt;The status of the invoice.&lt;/p&gt;&lt;p&gt;Possible values&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;created&lt;/code&gt; The invoice was successfully received by CodaBox and will be processed.&lt;/li&gt;&lt;li&gt;&lt;code&gt;sending&lt;/code&gt; The invoice is valid and will be sent to the customer.&lt;/li&gt;&lt;li&gt;&lt;code&gt;sent&lt;/code&gt; The invoice note is available for the customer in Zoomit. In this case you receive a transactionId.&lt;/li&gt;&lt;li&gt;&lt;code&gt;paid&lt;/code&gt; The user initiated the payment through Zoomit.&lt;/li&gt;&lt;li&gt;&lt;code&gt;invalid&lt;/code&gt; The invoice is not valid, you will receive an error object containing a code and a message explaining what went wrong (see below).&lt;/li&gt;&lt;li&gt;&lt;code&gt;send-error&lt;/code&gt; The invoice could not be sent to the customer, you will receive an error object containing a code and a message explaining what went wrong (see below).    &lt;/li&gt;&lt;/ul&gt;&lt;p&gt;In case of an unspecified error or an issue with the receiving access point, you can try to resend the invoice. CodaBox will not automatically resend the invoice, send-error is a final state of a document.&lt;/p&gt;</value>
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
