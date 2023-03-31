using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// <p>This is an object representing a financial institution holding, a fake holding on a fake securities account you can create for test purposes.</p>
    /// <p>In addition to the regular holding API calls, there are sandbox-specific endpoints available to manage your sandbox data.</p>
    /// <p>From this object, you can follow the link to its related financial institution account</p>
    /// </summary>
    [DataContract]
    public class SandboxFinancialInstitutionHolding
    {
        /// <summary>
        /// Name of the financial institution holding
        /// </summary>
        /// <value>Name of the financial institution holding</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Type of holding. Can be &lt;code&gt;SICAV&lt;/code&gt;, &lt;code&gt;STOCK&lt;/code&gt; or &lt;code&gt;OTHER&lt;/code&gt;
        /// </summary>
        /// <value>Type of holding. Can be &lt;code&gt;SICAV&lt;/code&gt;, &lt;code&gt;STOCK&lt;/code&gt; or &lt;code&gt;OTHER&lt;/code&gt;</value>
        [DataMember(Name = "subtype", EmitDefaultValue = false)]
        public string Subtype { get; set; }

        /// <summary>
        /// The reference of the holding
        /// </summary>
        /// <value>The reference of the holding</value>
        [DataMember(Name = "reference", EmitDefaultValue = false)]
        public string Reference { get; set; }

        /// <summary>
        /// Type of holding reference (such as &lt;code&gt;ISIN&lt;/code&gt;)
        /// </summary>
        /// <value>Type of holding reference (such as &lt;code&gt;ISIN&lt;/code&gt;)</value>
        [DataMember(Name = "reference_type", EmitDefaultValue = false)]
        public string ReferenceType { get; set; }

        /// <summary>
        /// Number of the financial institution holding
        /// </summary>
        /// <value>Number of the financial institution holding</value>
        [DataMember(Name = "quantity", EmitDefaultValue = false)]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The date of the last valuation
        /// </summary>
        /// <value>The date of the last valuation</value>
        [DataMember(Name = "lastValuationDate", EmitDefaultValue = false)]
        public DateTimeOffset LastValuationDate { get; set; }

        /// <summary>
        /// The currency of the last valuation
        /// </summary>
        /// <value>The currency of the last valuation</value>
        [DataMember(Name = "lastValuationCurrency", EmitDefaultValue = false)]
        public string LastValuationCurrency { get; set; }

        /// <summary>
        /// Market value of 1 quantity
        /// </summary>
        /// <value>Market value of 1 quantity</value>
        [DataMember(Name = "lastValuation", EmitDefaultValue = false)]
        public decimal LastValuation { get; set; }

        /// <summary>
        /// The total value of the financial institution holding
        /// </summary>
        /// <value>The total value of the financial institution holding</value>
        [DataMember(Name = "totalValuation", EmitDefaultValue = false)]
        public decimal TotalValuation { get; set; }

        /// <summary>
        /// The currency of the total valuation
        /// </summary>
        /// <value>The currency of the total valuation</value>
        [DataMember(Name = "totalValuationCurrency", EmitDefaultValue = false)]
        public string TotalValuationCurrency { get; set; }
    }

    /// <inheritdoc />
    [DataContract]
    public class SandboxFinancialInstitutionHoldingResponse : SandboxFinancialInstitutionHolding, IIdentified<Guid>
    {
        /// <inheritdoc />
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// When this financial institution holding was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this financial institution holding was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// When this financial institution holding was last synchronized successfully. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this financial institution holding was last synchronized successfully. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "updatedAt", EmitDefaultValue = false)]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
