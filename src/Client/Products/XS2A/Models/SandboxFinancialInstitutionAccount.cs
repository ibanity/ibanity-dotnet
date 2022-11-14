using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// <p>This is an object representing a financial institution account, a fake account you create for test purposes.</p>
    /// <p>In addition to the regular account API calls, there are sandbox-specific endpoints available to manage your sandbox data.</p>
    /// <p>A financial institution account belongs to a financial institution user and financial institution and can have many associated financial institution transactions.</p>
    /// </summary>
    [DataContract]
    public class SandboxFinancialInstitutionAccount
    {
        /// <summary>
        /// Type of financial institution account. Can be &lt;code&gt;checking&lt;/code&gt;, &lt;code&gt;savings&lt;/code&gt;, &lt;code&gt;securities&lt;/code&gt;, &lt;code&gt;card&lt;/code&gt; or &lt;code&gt;psp&lt;/code&gt;
        /// </summary>
        /// <value>Type of financial institution account. Can be &lt;code&gt;checking&lt;/code&gt;, &lt;code&gt;savings&lt;/code&gt;, &lt;code&gt;securities&lt;/code&gt;, &lt;code&gt;card&lt;/code&gt; or &lt;code&gt;psp&lt;/code&gt;</value>
        [DataMember(Name = "subtype", IsRequired = true, EmitDefaultValue = true)]
        public string Subtype { get; set; }

        /// <summary>
        /// Financial institution&#39;s internal reference for this financial institution account
        /// </summary>
        /// <value>Financial institution&#39;s internal reference for this financial institution account</value>
        [DataMember(Name = "reference", IsRequired = true, EmitDefaultValue = true)]
        public string Reference { get; set; }

        /// <summary>
        /// Type of financial institution reference (can be &lt;code&gt;IBAN&lt;/code&gt;, &lt;code&gt;BBAN&lt;/code&gt;, &lt;code&gt;email&lt;/code&gt;, &lt;code&gt;PAN&lt;/code&gt;, &lt;code&gt;MASKEDPAN&lt;/code&gt; or &lt;code&gt;MSISDN&lt;/code&gt;)
        /// </summary>
        /// <value>Type of financial institution reference (can be &lt;code&gt;IBAN&lt;/code&gt;, &lt;code&gt;BBAN&lt;/code&gt;, &lt;code&gt;email&lt;/code&gt;, &lt;code&gt;PAN&lt;/code&gt;, &lt;code&gt;MASKEDPAN&lt;/code&gt; or &lt;code&gt;MSISDN&lt;/code&gt;)</value>
        [DataMember(Name = "referenceType", IsRequired = true, EmitDefaultValue = true)]
        public string ReferenceType { get; set; }

        /// <summary>
        /// Description of the financial institution account
        /// </summary>
        /// <value>Description of the financial institution account</value>
        [DataMember(Name = "description", IsRequired = true, EmitDefaultValue = true)]
        public string Description { get; set; }

        /// <summary>
        /// Currency of the financial institution account, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_4217&#39;&gt;ISO4217&lt;/a&gt; format
        /// </summary>
        /// <value>Currency of the financial institution account, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_4217&#39;&gt;ISO4217&lt;/a&gt; format</value>
        [DataMember(Name = "currency", IsRequired = true, EmitDefaultValue = true)]
        public string Currency { get; set; }

        /// <summary>
        /// Name of the account product
        /// </summary>
        /// <value>Name of the account product</value>
        [DataMember(Name = "product", EmitDefaultValue = false)]
        public string Product { get; set; }

        /// <summary>
        /// Name of the account holder
        /// </summary>
        /// <value>Name of the account holder</value>
        [DataMember(Name = "holderName", EmitDefaultValue = false)]
        public string HolderName { get; set; }
    }

    /// <inheritdoc />
    public class SandboxFinancialInstitutionAccountResponse : SandboxFinancialInstitutionAccount, IIdentified<Guid>
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <summary>
        /// Amount of financial institution account funds that can be accessed immediately
        /// </summary>
        /// <value>Amount of financial institution account funds that can be accessed immediately</value>
        [DataMember(Name = "availableBalance", EmitDefaultValue = false)]
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// When the available balance was changed for the last time. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When the available balance was changed for the last time. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "availableBalanceChangedAt", EmitDefaultValue = false)]
        public DateTimeOffset AvailableBalanceChangedAt { get; set; }

        /// <summary>
        /// Reference date of the available balance. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>Reference date of the available balance. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "availableBalanceReferenceDate", EmitDefaultValue = false)]
        public DateTimeOffset AvailableBalanceReferenceDate { get; set; }

        /// <summary>
        /// When this financial institution account was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this financial institution account was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Total funds currently in the financial institution account
        /// </summary>
        /// <value>Total funds currently in the financial institution account</value>
        [DataMember(Name = "currentBalance", EmitDefaultValue = false)]
        public decimal CurrentBalance { get; set; }

        /// <summary>
        /// When the current balance was changed for the last time. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When the current balance was changed for the last time. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "currentBalanceChangedAt", EmitDefaultValue = false)]
        public DateTimeOffset CurrentBalanceChangedAt { get; set; }

        /// <summary>
        /// Reference date of the current balance. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>Reference date of the current balance. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "currentBalanceReferenceDate", EmitDefaultValue = false)]
        public DateTimeOffset CurrentBalanceReferenceDate { get; set; }

        /// <summary>
        /// When this financial institution account was last synchronized successfully. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this financial institution account was last synchronized successfully. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "updatedAt", EmitDefaultValue = false)]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
