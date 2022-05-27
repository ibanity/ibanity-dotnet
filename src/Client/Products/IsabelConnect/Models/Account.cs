using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.IsabelConnect.Models
{
    /// <summary>
    /// <p>This is an object representing a customer account. This object will provide details about the account, including the reference and the currency.</p>
    /// <p>An account has related transactions and balances.</p>
    /// <p>The account API endpoints are customer specific and therefore can only be accessed by providing the corresponding access token.</p>
    /// </summary>
    [DataContract]
    public class Account : Identified<Guid>
    {
        /// <summary>
        /// Country of the account, same as the country of the financial institution where the account is held
        /// </summary>
        /// <value>Country of the account, same as the country of the financial institution where the account is held</value>
        [DataMember(Name = "country", EmitDefaultValue = false)]
        public string Country { get; set; }

        /// <summary>
        /// Currency of the account, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_4217&#39;&gt;ISO4217&lt;/a&gt; format
        /// </summary>
        /// <value>Currency of the account, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_4217&#39;&gt;ISO4217&lt;/a&gt; format</value>
        [DataMember(Name = "currency", EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        /// Description of the account
        /// </summary>
        /// <value>Description of the account</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// BIC for the account&#39;s financial institution
        /// </summary>
        /// <value>BIC for the account&#39;s financial institution</value>
        [DataMember(Name = "financialInstitutionBic", EmitDefaultValue = false)]
        public string FinancialInstitutionBic { get; set; }

        /// <summary>
        /// Address of the  account holder
        /// </summary>
        /// <value>Address of the  account holder</value>
        [DataMember(Name = "holderAddress", EmitDefaultValue = false)]
        public string HolderAddress { get; set; }

        /// <summary>
        /// Country of the account holder&#39;s address
        /// </summary>
        /// <value>Country of the account holder&#39;s address</value>
        [DataMember(Name = "holderAddressCountry", EmitDefaultValue = false)]
        public string HolderAddressCountry { get; set; }

        /// <summary>
        /// Name of the account holder
        /// </summary>
        /// <value>Name of the account holder</value>
        [DataMember(Name = "holderName", EmitDefaultValue = false)]
        public string HolderName { get; set; }

        /// <summary>
        /// Financial institution&#39;s internal reference for this account
        /// </summary>
        /// <value>Financial institution&#39;s internal reference for this account</value>
        [DataMember(Name = "reference", EmitDefaultValue = false)]
        public string Reference { get; set; }

        /// <summary>
        /// Type of financial institution reference (either &lt;code&gt;IBAN&lt;/code&gt; or &lt;code&gt;unknown&lt;/code&gt;)
        /// </summary>
        /// <value>Type of financial institution reference (either &lt;code&gt;IBAN&lt;/code&gt; or &lt;code&gt;unknown&lt;/code&gt;)</value>
        [DataMember(Name = "referenceType", EmitDefaultValue = false)]
        public string ReferenceType { get; set; }
    }
}
