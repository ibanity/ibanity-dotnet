using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.eInvoicing.Models
{
    /// <summary>
    /// Peppol Inbound Document Business Status
    /// </summary>
    [DataContract]
    public class PeppolInboundDocumentBusinessStatus
    {
        /// <summary>
        /// &lt;p&gt;The code of this business status.&lt;/p&gt;&lt;p&gt;Possible values&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;acknowledged&lt;/code&gt; The document has been presented to the supplier and is ready for processing.&lt;/li&gt;&lt;li&gt;&lt;code&gt;accepted&lt;/code&gt; the document has been accepted by the supplier.&lt;/li&gt;&lt;li&gt;&lt;code&gt;rejected&lt;/code&gt; The document has been rejected by the supplier.&lt;/li&gt;&lt;/ul&gt;
        /// </summary>
        /// <value>&lt;p&gt;The code of this business status.&lt;/p&gt;&lt;p&gt;Possible values&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;acknowledged&lt;/code&gt; The document has been presented to the supplier and is ready for processing.&lt;/li&gt;&lt;li&gt;&lt;code&gt;accepted&lt;/code&gt; the document has been accepted by the supplier.&lt;/li&gt;&lt;li&gt;&lt;code&gt;rejected&lt;/code&gt; The document has been rejected by the supplier.&lt;/li&gt;&lt;/ul&gt;</value>
        [DataMember(Name = "code", EmitDefaultValue = true)]
        public string Code { get; set; }
    }

    /// <inheritdoc cref="PeppolInboundDocumentBusinessStatus" />
    public class PeppolInboundDocumentBusinessStatusResponse : PeppolInboundDocumentBusinessStatus, IIdentified<Guid>
    {
        /// <inheritdoc />
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// When this peppol inbound document business status was created. Formatted according to ISO8601 spec.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// &lt;p&gt;The technical status of the business status.&lt;/p&gt;&lt;p&gt;Possible values&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;sending&lt;/code&gt; The business status is accepted and an IMR will be sent to the sender of the document.&lt;/li&gt;&lt;li&gt;&lt;code&gt;sent&lt;/code&gt; The IMR is sent to the Access Point of the sender of the document.&lt;/li&gt;&lt;li&gt;&lt;code&gt;acknowledged&lt;/code&gt; The Access Point of the sender of the document has acknowledged the IMR.&lt;/li&gt;&lt;li&gt;&lt;code&gt;accepted&lt;/code&gt; The Access Point of the sender of the document has accepted the IMR.&lt;/li&gt;&lt;li&gt;&lt;code&gt;rejected&lt;/code&gt; The Access Point of the sender of the document has rejected the IMR due to syntactic or semantic issues.&lt;/li&gt;&lt;li&gt;&lt;code&gt;send-error&lt;/code&gt; The IMR could not be sent to the sender of the document, you will receive an error object containing a code and a message explaining what went wrong (see above).&lt;/li&gt;&lt;/ul&gt;&lt;p&gt;&lt;code&gt;acknowledged&lt;/code&gt;, &lt;code&gt;accepted&lt;/code&gt;, &lt;code&gt;rejected&lt;/code&gt; and &lt;code&gt;send-error&lt;/code&gt; are final technical statuses of a business status.&lt;/p&gt;
        /// </summary>
        [DataMember(Name = "technicalStatus", EmitDefaultValue = false)]
        public string TechnicalStatus { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        [DataMember(Name = "errorCode", EmitDefaultValue = false)]
        public string ErrorCode { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        [DataMember(Name = "errorMessage", EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }
    }
}
