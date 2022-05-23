using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.IsabelConnect.Models
{
    /// <summary>
    /// <p>This is an object representing an account transaction. This object will give you the details of the financial transaction, including its amount and execution date.</p>
    /// <p>Unlike an intraday transaction, this is an end-of-day object which will not change.</p>
    /// </summary>
    [DataContract]
    public class Transaction
    {
        /// <summary>
        /// Additional transaction-related information provided from the financial institution to the customer
        /// </summary>
        /// <value>Additional transaction-related information provided from the financial institution to the customer</value>
        [DataMember(Name = "additionalInformation", EmitDefaultValue = false)]
        public string AdditionalInformation { get; set; }

        /// <summary>
        /// Amount of the transaction. Can be positive or negative
        /// </summary>
        /// <value>Amount of the transaction. Can be positive or negative</value>
        [DataMember(Name = "amount", EmitDefaultValue = false)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Number representing the counterpart&#39;s account
        /// </summary>
        /// <value>Number representing the counterpart&#39;s account</value>
        [DataMember(Name = "counterpartAccountReference", EmitDefaultValue = false)]
        public string CounterpartAccountReference { get; set; }

        /// <summary>
        /// BIC for the counterpart&#39;s financial institution
        /// </summary>
        /// <value>BIC for the counterpart&#39;s financial institution</value>
        [DataMember(Name = "counterpartFinancialInstitutionBic", EmitDefaultValue = false)]
        public string CounterpartFinancialInstitutionBic { get; set; }

        /// <summary>
        /// Legal name of the counterpart
        /// </summary>
        /// <value>Legal name of the counterpart</value>
        [DataMember(Name = "counterpartName", EmitDefaultValue = false)]
        public string CounterpartName { get; set; }

        /// <summary>
        /// Unique identification assigned by the initiating party to unambiguously identify the transaction. This identification is passed on, unchanged, throughout the entire end-to-end chain.
        /// </summary>
        /// <value>Unique identification assigned by the initiating party to unambiguously identify the transaction. This identification is passed on, unchanged, throughout the entire end-to-end chain.</value>
        [DataMember(Name = "endToEndId", EmitDefaultValue = false)]
        public string EndToEndId { get; set; }

        /// <summary>
        /// Date representing the moment the transaction has been recorded
        /// </summary>
        /// <value>Date representing the moment the transaction has been recorded</value>
        [DataMember(Name = "executionDate", EmitDefaultValue = false)]
        public string ExecutionDateString
        {
            get => ExecutionDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => ExecutionDate = DateTimeOffset.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Date representing the moment the transaction has been recorded
        /// </summary>
        /// <value>Date representing the moment the transaction has been recorded</value>
        public DateTimeOffset ExecutionDate { get; set; }

        /// <summary>
        /// Financial institution&#39;s reference for the transaction
        /// </summary>
        /// <value>Financial institution&#39;s reference for the transaction</value>
        [DataMember(Name = "internalId", EmitDefaultValue = false)]
        public string InternalId { get; set; }

        /// <summary>
        /// Content of the remittance information (aka communication)
        /// </summary>
        /// <value>Content of the remittance information (aka communication)</value>
        [DataMember(Name = "remittanceInformation", EmitDefaultValue = false)]
        public string RemittanceInformation { get; set; }

        /// <summary>
        /// Type of remittance information. Can be &lt;code&gt;structured-be&lt;/code&gt;, &lt;code&gt;structured-eu&lt;/code&gt;, or &lt;code&gt;unstructured&lt;/code&gt;
        /// </summary>
        /// <value>Type of remittance information. Can be &lt;code&gt;structured-be&lt;/code&gt;, &lt;code&gt;structured-eu&lt;/code&gt;, or &lt;code&gt;unstructured&lt;/code&gt;</value>
        [DataMember(Name = "remittanceInformationType", EmitDefaultValue = false)]
        public string RemittanceInformationType { get; set; }

        /// <summary>
        /// Current status of the transaction. Possible values are &lt;code&gt;booked&lt;/code&gt;, &lt;code&gt;information&lt;/code&gt;, or &lt;code&gt;pending&lt;/code&gt;
        /// </summary>
        /// <value>Current status of the transaction. Possible values are &lt;code&gt;booked&lt;/code&gt;, &lt;code&gt;information&lt;/code&gt;, or &lt;code&gt;pending&lt;/code&gt;</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Date representing the moment the transaction is considered effective
        /// </summary>
        /// <value>Date representing the moment the transaction is considered effective</value>
        [DataMember(Name = "valueDate", EmitDefaultValue = false)]
        public string ValueDateString
        {
            get => ValueDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => ValueDate = DateTimeOffset.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Date representing the moment the transaction is considered effective
        /// </summary>
        /// <value>Date representing the moment the transaction is considered effective</value>
        public DateTimeOffset ValueDate { get; set; }
    }
}
