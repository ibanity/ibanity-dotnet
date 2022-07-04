using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.eInvoicing.Models
{
    /// <summary>
    /// <p>This endpoint allows you to search for a customer on the PEPPOL network and the document types it supports. Based on the response you know:</p>
    /// <p>- whether the customer is available on Peppol and is capable to receive documents over Peppol</p>
    /// <p>- which UBL document types the customer is capable to receive</p>
    /// <p>A list of test receivers can be found in <see href="https://documentation.ibanity.com/einvoicing/products#development-resources">Development Resources</see></p>
    /// </summary>
    [DataContract]
    public class PeppolCustomerSearch
    {
        /// <summary>
        /// &lt;p&gt;Customer reference identifier of the customer.&lt;/p&gt;&lt;p&gt;The &lt;code&gt;customerReference&lt;/code&gt; should be of type Electronic Address Scheme (EAS), for more information see &lt;a href&#x3D;\&quot;http://docs.peppol.eu/poacc/billing/3.0/codelist/eas/\&quot;&gt;http://docs.peppol.eu/poacc/billing/3.0/codelist/eas/&lt;/a&gt;&lt;/p&gt;&lt;p&gt;Belgian participants are registered with the Belgian company number, for which identifier 0208 can be used. Optionally, the customer can be registered with their VAT number, for which identifier 9925 can be used.&lt;/p&gt;
        /// </summary>
        /// <value>&lt;p&gt;Customer reference identifier of the customer.&lt;/p&gt;&lt;p&gt;The &lt;code&gt;customerReference&lt;/code&gt; should be of type Electronic Address Scheme (EAS), for more information see &lt;a href&#x3D;\&quot;http://docs.peppol.eu/poacc/billing/3.0/codelist/eas/\&quot;&gt;http://docs.peppol.eu/poacc/billing/3.0/codelist/eas/&lt;/a&gt;&lt;/p&gt;&lt;p&gt;Belgian participants are registered with the Belgian company number, for which identifier 0208 can be used. Optionally, the customer can be registered with their VAT number, for which identifier 9925 can be used.&lt;/p&gt;</value>
        [DataMember(Name = "customerReference", EmitDefaultValue = false)]
        public string CustomerReference { get; set; }

        /// <summary>
        /// &lt;p&gt;This object contains 5 different attributes: &lt;ul&gt;&lt;li&gt;&lt;code&gt;rootNamespace&lt;/code&gt; The root namespace of the document.&lt;/li&gt;&lt;li&gt;&lt;code&gt;localName&lt;/code&gt; Type of document, Invoice or Credit Note.&lt;/li&gt;&lt;li&gt;&lt;code&gt;customizationId&lt;/code&gt; An identification of the specification containing the total set of rules regarding semantic content, cardinalities and business rules to which the data contained in the instance document conforms.&lt;/li&gt;&lt;li&gt;&lt;code&gt;ublVersionId&lt;/code&gt; Version of the UBL.&lt;/li&gt;&lt;li&gt;&lt;code&gt;profileId&lt;/code&gt; Identifies the business process context in which the transaction appears, to enable the Buyer to process the Invoice in an appropriate way.&lt;/li&gt;&lt;/ul&gt;&lt;/p&gt;
        /// </summary>
        /// <value>&lt;p&gt;This object contains 5 different attributes: &lt;ul&gt;&lt;li&gt;&lt;code&gt;rootNamespace&lt;/code&gt; The root namespace of the document.&lt;/li&gt;&lt;li&gt;&lt;code&gt;localName&lt;/code&gt; Type of document, Invoice or Credit Note.&lt;/li&gt;&lt;li&gt;&lt;code&gt;customizationId&lt;/code&gt; An identification of the specification containing the total set of rules regarding semantic content, cardinalities and business rules to which the data contained in the instance document conforms.&lt;/li&gt;&lt;li&gt;&lt;code&gt;ublVersionId&lt;/code&gt; Version of the UBL.&lt;/li&gt;&lt;li&gt;&lt;code&gt;profileId&lt;/code&gt; Identifies the business process context in which the transaction appears, to enable the Buyer to process the Invoice in an appropriate way.&lt;/li&gt;&lt;/ul&gt;&lt;/p&gt;</value>
        [DataMember(Name = "supportedDocumentFormats", EmitDefaultValue = false)]
        public List<Object> SupportedDocumentFormats { get; set; }
    }
}
