using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// <p>This is an object representing an account holding. This object will give you the details of the financial holding</p>
    /// <p>From this object, you can link back to its account.</p>
    /// <p>The Holding API endpoints are customer specific and therefore can only be accessed by providing the corresponding customer access token.</p>
    /// </summary>
    [DataContract]
    public class Holding : Identified<Guid>
    {
        /// <summary>
        /// Market value of 1 quantity
        /// </summary>
        /// <value>Market value of 1 quantity</value>
        [DataMember(Name = "lastValuation", EmitDefaultValue = false)]
        public decimal LastValuation { get; set; }

        /// <summary>
        /// The currency of the last valuation
        /// </summary>
        /// <value>The currency of the last valuation</value>
        [DataMember(Name = "lastValuationCurrency", EmitDefaultValue = false)]
        public string LastValuationCurrency { get; set; }

        /// <summary>
        /// The date of the last valuation
        /// </summary>
        /// <value>The date of the last valuation</value>
        [DataMember(Name = "lastValuationDate", EmitDefaultValue = false)]
        public DateTimeOffset LastValuationDate { get; set; }

        /// <summary>
        /// Name of the holding
        /// </summary>
        /// <value>Name of the holding</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Number of the holding
        /// </summary>
        /// <value>Number of the holding</value>
        [DataMember(Name = "quantity", EmitDefaultValue = false)]
        public decimal Quantity { get; set; }

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
        [DataMember(Name = "referenceType", EmitDefaultValue = false)]
        public string ReferenceType { get; set; }

        /// <summary>
        /// Type of holding. Can be &lt;code&gt;SICAV&lt;/code&gt;, &lt;code&gt;STOCK&lt;/code&gt; or &lt;code&gt;OTHER&lt;/code&gt;
        /// </summary>
        /// <value>Type of holding. Can be &lt;code&gt;SICAV&lt;/code&gt;, &lt;code&gt;STOCK&lt;/code&gt; or &lt;code&gt;OTHER&lt;/code&gt;</value>
        [DataMember(Name = "subtype", EmitDefaultValue = false)]
        public string Subtype { get; set; }

        /// <summary>
        /// The total value of the holding
        /// </summary>
        /// <value>The total value of the holding</value>
        [DataMember(Name = "totalValuation", EmitDefaultValue = false)]
        public decimal TotalValuation { get; set; }

        /// <summary>
        /// The currency of the total valuation
        /// </summary>
        /// <value>The currency of the total valuation</value>
        [DataMember(Name = "totalValuationCurrency", EmitDefaultValue = false)]
        public string TotalValuationCurrency { get; set; }

        /// <summary>
        /// ID of the account that this transaction belongs to.
        /// </summary>
        public Guid AccountId { get; set; }
    }

    /// <summary>
    /// Link to the account that this holding belongs to.
    /// </summary>
    [DataContract]
    public class HoldingRelationships
    {
        /// <summary>
        /// Link to the account that this transaction belongs to.
        /// </summary>
        [DataMember(Name = "account", EmitDefaultValue = false)]
        public JsonApi.Relationship Account { get; set; }
    }
}
