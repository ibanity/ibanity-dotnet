using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.IsabelConnect.Models
{
    /// <summary>
    /// This is an object representing a balance related to a customer's account.
    /// </summary>
    [DataContract]
    public class Balance
    {
        /// <summary>
        /// Build a new instance.
        /// </summary>
        public Balance() { }

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="from">Source instance</param>
        /// <remarks>Copy constructor.</remarks>
        public Balance(Balance from)
        {
            if (from is null)
                throw new ArgumentNullException(nameof(from));

            Amount = from.Amount;
            Datetime = from.Datetime;
            Subtype = from.Subtype;
        }

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

    /// <summary>
    /// Technical-only object, prefer <see cref="Balance" /> usage.
    /// </summary>
    public class BalanceWithFakeId : Balance, IIdentified<string>
    {
        /// <summary>
        /// Fake field.
        /// </summary>
        public string Id { get; set; }
    }
}
