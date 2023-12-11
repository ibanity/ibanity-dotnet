using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.IsabelConnect.Models
{
    /// <summary>
    /// References and associated currency of the corresponding accounts
    /// </summary>
    [DataContract]
    public class AccountReferencesAndCurrencies
    {
        /// <summary>
        /// Reference of the corresponding account
        /// </summary>
        /// <value>Reference of the corresponding account</value>
        [DataMember(Name = "accountReference", EmitDefaultValue = false)]
        public string AccountReference { get; set; }

        /// <summary>
        /// Currency of the corresponding account
        /// </summary>
        /// <value>Currency of the corresponding account</value>
        [DataMember(Name = "currency", EmitDefaultValue = false)]
        public string Currency { get; set; }
    }
}
