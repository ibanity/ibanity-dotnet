using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

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
    }

    /// <inheritdoc cref="PeppolCustomerSearch" />
    [DataContract]
    public class PeppolCustomerSearchResponse : PeppolCustomerSearch, IIdentified<Guid>
    {
        /// <summary>
        /// Supported document formats
        /// </summary>
        [DataMember(Name = "supportedDocumentFormats", EmitDefaultValue = false)]
        public List<SupportedDocumentFormat> SupportedDocumentFormats { get; set; }

        /// <inheritdoc />
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Supported document format
    /// </summary>
    [DataContract]
    public class SupportedDocumentFormat
    {
        /// <summary>
        /// The root namespace of the document.
        /// </summary>
        [DataMember(Name = "rootNamespace", EmitDefaultValue = false)]
        public string RootNamespace { get; set; }

        /// <summary>
        /// Type of document, Invoice or Credit Note.
        /// </summary>
        [DataMember(Name = "localName", EmitDefaultValue = false)]
        public string LocalName { get; set; }

        /// <summary>
        /// An identification of the specification containing the total set of rules regarding semantic content, cardinalities and business rules to which the data contained in the instance document conforms.
        /// </summary>
        [DataMember(Name = "customizationId", EmitDefaultValue = false)]
        public string CustomizationId { get; set; }

        /// <summary>
        /// Version of the UBL.
        /// </summary>
        [DataMember(Name = "ublVersionId", EmitDefaultValue = false)]
        public string UblVersionId { get; set; }

        /// <summary>
        /// Identifies the business process context in which the transaction appears, to enable the Buyer to process the Invoice in an appropriate way.
        /// </summary>
        [DataMember(Name = "profileId", EmitDefaultValue = false)]
        public string ProfileId { get; set; }
    }
}
