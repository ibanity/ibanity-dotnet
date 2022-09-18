using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.CodaboxConnect.Models
{
    /// <summary>
    /// This resource allows an Accounting Software to retrieve a bank account statement for a client of an accounting office.
    /// </summary>
    [DataContract]
    public class BankAccountStatement : Identified<Guid>
    {
        /// <summary>
        /// When this bank account statement was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this bank account statement was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Bank account number of the bank statement.
        /// </summary>
        /// <value>Bank account number of the bank statement.</value>
        [DataMember(Name = "iban", EmitDefaultValue = false)]
        public string Iban { get; set; }
    }
}
