using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.eInvoicing.Models
{
    /// <summary>
    /// This resource allows a Software Partner to create a new Supplier.
    /// </summary>
    [DataContract]
    public class Supplier
    {
        /// <summary>
        /// The city where the supplier is located.
        /// </summary>
        /// <value>The city where the supplier is located.</value>
        [DataMember(Name = "city", EmitDefaultValue = false)]
        public string City { get; set; }

        /// <summary>
        /// The company number of the supplier.
        /// </summary>
        /// <value>The company number of the supplier.</value>
        [DataMember(Name = "companyNumber", EmitDefaultValue = false)]
        public string CompanyNumber { get; set; }

        /// <summary>
        /// A person or department for CodaBox to contact in case of problems.
        /// </summary>
        /// <value>A person or department for CodaBox to contact in case of problems.</value>
        [DataMember(Name = "contactEmail", EmitDefaultValue = false)]
        public string ContactEmail { get; set; }

        /// <summary>
        /// The country where the supplier is located.
        /// </summary>
        /// <value>The country where the supplier is located.</value>
        [DataMember(Name = "country", EmitDefaultValue = false)]
        public string Country { get; set; }

        /// <summary>
        /// When this supplier was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this supplier was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The contact email of the supplier.
        /// </summary>
        /// <value>The contact email of the supplier.</value>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        /// The homepage of the website of the supplier.
        /// </summary>
        /// <value>The homepage of the website of the supplier.</value>
        [DataMember(Name = "homepage", EmitDefaultValue = false)]
        public string Homepage { get; set; }

        /// <summary>
        /// An array of objects representing the IBANs of the supplier.&lt;ul&gt;&lt;li&gt;&lt;code&gt;id&lt;/code&gt;An uuid of the IBAN number of the supplier.&lt;/li&gt;&lt;li&gt;&lt;code&gt;value&lt;/code&gt;The supplier IBAN of the supplier.&lt;/li&gt;&lt;/ul&gt;
        /// </summary>
        /// <value>An array of objects representing the IBANs of the supplier.&lt;ul&gt;&lt;li&gt;&lt;code&gt;id&lt;/code&gt;An uuid of the IBAN number of the supplier.&lt;/li&gt;&lt;li&gt;&lt;code&gt;value&lt;/code&gt;The supplier IBAN of the supplier.&lt;/li&gt;&lt;/ul&gt;</value>
        [DataMember(Name = "ibans", EmitDefaultValue = false)]
        public List<SupplierIban> Ibans { get; set; }

        /// <summary>
        /// An array of objects representing the company names of the supplier.&lt;ul&gt;&lt;li&gt;&lt;code&gt;id&lt;/code&gt;An uuid of the company name of the supplier.&lt;/li&gt;&lt;li&gt;&lt;code&gt;value&lt;/code&gt;The company name of the supplier.&lt;/li&gt;&lt;/ul&gt;
        /// </summary>
        /// <value>An array of objects representing the company names of the supplier.&lt;ul&gt;&lt;li&gt;&lt;code&gt;id&lt;/code&gt;An uuid of the company name of the supplier.&lt;/li&gt;&lt;li&gt;&lt;code&gt;value&lt;/code&gt;The company name of the supplier.&lt;/li&gt;&lt;/ul&gt;</value>
        [DataMember(Name = "names", EmitDefaultValue = false)]
        public List<SupplierName> Names { get; set; }

        /// <summary>
        /// The status of the onboarding process of this supplier, for more information see &lt;a href&#x3D;\&quot;https://documentation.ibanity.com/einvoicing/products#suppliers\&quot;&gt;Suppliers&lt;/a&gt;.&lt;ul&gt;&lt;li&gt;&lt;code&gt;created&lt;/code&gt;The supplier was successfully created at CodaBox.&lt;/li&gt;&lt;li&gt;&lt;code&gt;approved&lt;/code&gt;The supplier information was validated by CodaBox and the supplier will be onboarded on the einvoicing network(s).&lt;/li&gt;&lt;li&gt;&lt;code&gt;rejected&lt;/code&gt;There was a problem with the supplier information and CodaBox could not accept the supplier.&lt;/li&gt;&lt;li&gt;&lt;code&gt;onboarded&lt;/code&gt;The supplier is now fully onboarded, you can now start sending documents for this supplier.&lt;/li&gt;&lt;li&gt;&lt;code&gt;offboarded&lt;/code&gt;The supplier was successfully offboarded, you can no longer send documents for this supplier.&lt;/li&gt;&lt;/ul&gt;
        /// </summary>
        /// <value>The status of the onboarding process of this supplier, for more information see &lt;a href&#x3D;\&quot;https://documentation.ibanity.com/einvoicing/products#suppliers\&quot;&gt;Suppliers&lt;/a&gt;.&lt;ul&gt;&lt;li&gt;&lt;code&gt;created&lt;/code&gt;The supplier was successfully created at CodaBox.&lt;/li&gt;&lt;li&gt;&lt;code&gt;approved&lt;/code&gt;The supplier information was validated by CodaBox and the supplier will be onboarded on the einvoicing network(s).&lt;/li&gt;&lt;li&gt;&lt;code&gt;rejected&lt;/code&gt;There was a problem with the supplier information and CodaBox could not accept the supplier.&lt;/li&gt;&lt;li&gt;&lt;code&gt;onboarded&lt;/code&gt;The supplier is now fully onboarded, you can now start sending documents for this supplier.&lt;/li&gt;&lt;li&gt;&lt;code&gt;offboarded&lt;/code&gt;The supplier was successfully offboarded, you can no longer send documents for this supplier.&lt;/li&gt;&lt;/ul&gt;</value>
        [DataMember(Name = "onboardingStatus", EmitDefaultValue = false)]
        public string OnboardingStatus { get; set; }

        /// <summary>
        /// The phonenumber of the supplier.
        /// </summary>
        /// <value>The phonenumber of the supplier.</value>
        [DataMember(Name = "phoneNumber", EmitDefaultValue = false)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The street name of the address where the supplier is located.
        /// </summary>
        /// <value>The street name of the address where the supplier is located.</value>
        [DataMember(Name = "street", EmitDefaultValue = false)]
        public string Street { get; set; }

        /// <summary>
        /// The street number of the address where the supplier is located.
        /// </summary>
        /// <value>The street number of the address where the supplier is located.</value>
        [DataMember(Name = "streetNumber", EmitDefaultValue = false)]
        public string StreetNumber { get; set; }

        /// <summary>
        /// &lt;i&gt;(Optional)&lt;/i&gt; The support email address of the supplier. Can be used in case this differs from the general email.
        /// </summary>
        /// <value>&lt;i&gt;(Optional)&lt;/i&gt; The support email address of the supplier. Can be used in case this differs from the general email.</value>
        [DataMember(Name = "supportEmail", EmitDefaultValue = false)]
        public string SupportEmail { get; set; }

        /// <summary>
        /// &lt;i&gt;(Optional)&lt;/i&gt; The support phone number of the supplier.
        /// </summary>
        /// <value>&lt;i&gt;(Optional)&lt;/i&gt; The support phone number of the supplier.</value>
        [DataMember(Name = "supportPhone", EmitDefaultValue = false)]
        public string SupportPhone { get; set; }

        /// <summary>
        /// &lt;i&gt;(Optional)&lt;/i&gt; The support URL of the supplier.
        /// </summary>
        /// <value>&lt;i&gt;(Optional)&lt;/i&gt; The support URL of the supplier.</value>
        [DataMember(Name = "supportUrl", EmitDefaultValue = false)]
        public string SupportUrl { get; set; }

        /// <summary>
        /// The zipcode of the city where the supplier is located.
        /// </summary>
        /// <value>The zipcode of the city where the supplier is located.</value>
        [DataMember(Name = "zip", EmitDefaultValue = false)]
        public string Zip { get; set; }
    }

    /// <summary>
    /// IBAN of the supplier.
    /// </summary>
    [DataContract]
    public class SupplierIban
    {
        /// <summary>
        /// An uuid of the IBAN number of the supplier.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// The supplier IBAN of the supplier.
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    /// <summary>
    /// Name of the supplier.
    /// </summary>
    [DataContract]
    public class SupplierName
    {
        /// <summary>
        /// An uuid of the company name of the supplier.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// The company name of the supplier.
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }
}
