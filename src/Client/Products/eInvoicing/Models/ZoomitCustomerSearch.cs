using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.eInvoicing.Models
{
    /// <summary>
    /// <p>This endpoint allows you to search for a customer on the Zoomit network. Based on the response you know whether the customer is reachable on Zoomit or not.</p>
    /// <p>A list of test receivers can be found in <see href="https://documentation.ibanity.com/einvoicing/products#development-resources">Development Resources</see></p>
    /// </summary>
    [DataContract]
    public class ZoomitCustomerSearch
    {
        /// <summary>
        /// &lt;p&gt;The reference of the customer (IBAN).&lt;/p&gt;&lt;p&gt;Zoomit participants are registered with their IBAN. &lt;/p&gt;&lt;p&gt;The customerId should be of type Electronic Address Scheme (EAS), for more information see &lt;a href&#x3D;\&quot;http://docs.peppol.eu/poacc/billing/3.0/codelist/eas/\&quot;&gt;http://docs.peppol.eu/poacc/billing/3.0/codelist/eas/&lt;/a&gt;&lt;/p&gt;&lt;p&gt;To search for a customer based on its IBAN you need to use the identifier &lt;code&gt;0193&lt;/code&gt; which is the UBL.BE identifier and the IBAN should start with &lt;code&gt;IBN_&lt;/code&gt; Example: &lt;code&gt;0193:IBN_BE22977000014401&lt;/code&gt;.&lt;/p&gt;
        /// </summary>
        /// <value>&lt;p&gt;The reference of the customer (IBAN).&lt;/p&gt;&lt;p&gt;Zoomit participants are registered with their IBAN. &lt;/p&gt;&lt;p&gt;The customerId should be of type Electronic Address Scheme (EAS), for more information see &lt;a href&#x3D;\&quot;http://docs.peppol.eu/poacc/billing/3.0/codelist/eas/\&quot;&gt;http://docs.peppol.eu/poacc/billing/3.0/codelist/eas/&lt;/a&gt;&lt;/p&gt;&lt;p&gt;To search for a customer based on its IBAN you need to use the identifier &lt;code&gt;0193&lt;/code&gt; which is the UBL.BE identifier and the IBAN should start with &lt;code&gt;IBN_&lt;/code&gt; Example: &lt;code&gt;0193:IBN_BE22977000014401&lt;/code&gt;.&lt;/p&gt;</value>
        [DataMember(Name = "customerReference", EmitDefaultValue = false)]
        public string CustomerReference { get; set; }

        /// <summary>
        /// &lt;p&gt;The status of the customer.&lt;/p&gt;&lt;p&gt;Possible values&lt;/p&gt;&lt;ul&gt;&lt;li&gt;    &lt;code&gt;active&lt;/code&gt; The customer is using Zoomit and wants to receive your documents.&lt;/li&gt;&lt;li&gt;    &lt;code&gt;potential&lt;/code&gt; The customer can be reached on Zoomit, but did not yet confirm to receive your documents in Zoomit. To make sure your customer receives your documents, you should send the documents via Zoomit and an extra channel (eg email) until he accepts to receive your documents in Zoomit only.&lt;/li&gt;&lt;li&gt;    &lt;code&gt;not-reachable&lt;/code&gt; The customer is not available on Zoomit.&lt;/li&gt;&lt;/ul&gt;
        /// </summary>
        /// <value>&lt;p&gt;The status of the customer.&lt;/p&gt;&lt;p&gt;Possible values&lt;/p&gt;&lt;ul&gt;&lt;li&gt;    &lt;code&gt;active&lt;/code&gt; The customer is using Zoomit and wants to receive your documents.&lt;/li&gt;&lt;li&gt;    &lt;code&gt;potential&lt;/code&gt; The customer can be reached on Zoomit, but did not yet confirm to receive your documents in Zoomit. To make sure your customer receives your documents, you should send the documents via Zoomit and an extra channel (eg email) until he accepts to receive your documents in Zoomit only.&lt;/li&gt;&lt;li&gt;    &lt;code&gt;not-reachable&lt;/code&gt; The customer is not available on Zoomit.&lt;/li&gt;&lt;/ul&gt;</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }
    }

    /// <inheritdoc cref="ZoomitCustomerSearch" />
    [DataContract]
    public class ZoomitCustomerSearchResponse : ZoomitCustomerSearch, IIdentified<Guid>
    {
        /// <inheritdoc />
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }
    }
}
