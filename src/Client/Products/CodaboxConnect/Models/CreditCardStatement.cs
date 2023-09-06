using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.CodaboxConnect.Models
{
    /// <summary>
    /// This resource allows an Accounting Software to retrieve a payroll statement for a client of an accounting office.
    /// </summary>
    [DataContract]
    public class CreditCardStatement : Document<string>
    {
        /// <summary>
        /// The name of the bank issuing the credit card statement.
        /// </summary>
        /// <value>The name of the bank issuing the credit card statement.</value>
        [DataMember(Name = "bankName", EmitDefaultValue = false)]
        public string BankName { get; set; }

        /// <summary>
        /// End of the transaction period for which the statement was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec.
        /// </summary>
        /// <value>End of the transaction period for which the statement was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec.</value>
        [DataMember(Name = "billingPeriod", EmitDefaultValue = false)]
        public DateTimeOffset BillingPeriod { get; set; }

        /// <summary>
        /// The reference of the credit card owner.
        /// </summary>
        /// <value>The reference of the credit card owner.</value>
        [DataMember(Name = "clientReference", EmitDefaultValue = false)]
        public string ClientReference { get; set; }

        /// <summary>
        /// When this credit card statement was received by CodaBox. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec.
        /// </summary>
        /// <value>When this credit card statement was received by CodaBox. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec.</value>
        [DataMember(Name = "receivedAt", EmitDefaultValue = false)]
        public DateTimeOffset ReceivedAt { get; set; }
    }
}
