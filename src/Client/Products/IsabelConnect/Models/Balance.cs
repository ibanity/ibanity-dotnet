using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.IsabelConnect.Models
{
    /// <summary>
    /// This is an object representing a balance related to a customer's account.
    /// </summary>
    [DataContract]
    public class Balance
    {
        /// <summary>
        /// Amount of the balance. Can be positive or negative
        /// </summary>
        /// <value>Amount of the balance. Can be positive or negative</value>
        [DataMember(Name = "amount", EmitDefaultValue = false)]
        public decimal Amount { get; set; }

        /// <summary>
        /// When this balance was effective
        /// </summary>
        /// <value>When this balance was effective</value>
        [DataMember(Name = "datetime", EmitDefaultValue = false)]
        public DateTimeOffset Datetime { get; set; }

        /// <summary>
        /// Type of balance. The possible values are &lt;code&gt;CLBD&lt;/code&gt; (Closing Balance), &lt;code&gt;CLAV&lt;/code&gt; (Closing Available Balance), &lt;code&gt;ITBD&lt;/code&gt; (Intraday Balance), &lt;code&gt;ITAV&lt;/code&gt; (Intraday Available Balance), or &lt;code&gt;INFO&lt;/code&gt; if derived from the previous statement.
        /// </summary>
        /// <value>Type of balance. The possible values are &lt;code&gt;CLBD&lt;/code&gt; (Closing Balance), &lt;code&gt;CLAV&lt;/code&gt; (Closing Available Balance), &lt;code&gt;ITBD&lt;/code&gt; (Intraday Balance), &lt;code&gt;ITAV&lt;/code&gt; (Intraday Available Balance), or &lt;code&gt;INFO&lt;/code&gt; if derived from the previous statement.</value>
        [DataMember(Name = "subtype", EmitDefaultValue = false)]
        public string Subtype { get; set; }
    }
}
