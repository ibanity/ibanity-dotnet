using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.IsabelConnect.Models
{
    /// <summary>
    /// <p>This is an object representing an intraday account transaction. This object will give you the details of the intraday transaction, including its amount and execution date.</p>
    /// <p>At the end of the day, intraday transactions will be converted to transactions by the financial institution. The transactions will never be available as transactions and intraday transactions at the same time.</p>
    /// <p>Important: The ID of the intraday transaction will NOT be the same as the ID of the corresponding <see cref="Transaction" />.</p>
    /// </summary>
    [DataContract]
    public class IntradayTransaction
    {
        /// <summary>
        /// Additional transaction-related information provided from the financial institution to the customer
        /// </summary>
        /// <value>Additional transaction-related information provided from the financial institution to the customer</value>
        [DataMember(Name = "additionalInformation", EmitDefaultValue = false)]
        public string AdditionalInformation { get; set; }

        /// <summary>
        /// Amount of the intraday transaction. Can be positive or negative
        /// </summary>
        /// <value>Amount of the intraday transaction. Can be positive or negative</value>
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
        /// Date representing the moment the intraday transaction has been recorded
        /// </summary>
        /// <value>Date representing the moment the intraday transaction has been recorded</value>
        [DataMember(Name = "executionDate", EmitDefaultValue = false)]
        public string ExecutionDateString
        {
            get => ExecutionDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => ExecutionDate = DateTimeOffset.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Date representing the moment the intraday transaction has been recorded
        /// </summary>
        /// <value>Date representing the moment the intraday transaction has been recorded</value>
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
        /// Type of remittance information, can be &lt;code&gt;structured&lt;/code&gt; or &lt;code&gt;unstructured&lt;/code&gt;
        /// </summary>
        /// <value>Type of remittance information, can be &lt;code&gt;structured&lt;/code&gt; or &lt;code&gt;unstructured&lt;/code&gt;</value>
        [DataMember(Name = "remittanceInformationType", EmitDefaultValue = false)]
        public string RemittanceInformationType { get; set; }

        /// <summary>
        /// Current status of the account information access request. Possible values are &lt;code&gt;received&lt;/code&gt;, &lt;code&gt;started&lt;/code&gt;, &lt;code&gt;rejected&lt;/code&gt;, &lt;code&gt;succeeded&lt;/code&gt;, &lt;code&gt;partially-succeeded&lt;/code&gt;, and &lt;code&gt;failed&lt;/code&gt;. &lt;a href&#x3D;&#39;/xs2a/products#aiar-status-codes&#39;&gt;See status definitions.&lt;/a&gt;
        /// </summary>
        /// <value>Current status of the account information access request. Possible values are &lt;code&gt;received&lt;/code&gt;, &lt;code&gt;started&lt;/code&gt;, &lt;code&gt;rejected&lt;/code&gt;, &lt;code&gt;succeeded&lt;/code&gt;, &lt;code&gt;partially-succeeded&lt;/code&gt;, and &lt;code&gt;failed&lt;/code&gt;. &lt;a href&#x3D;&#39;/xs2a/products#aiar-status-codes&#39;&gt;See status definitions.&lt;/a&gt;</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Date representing the moment the intraday transaction is considered effective
        /// </summary>
        /// <value>Date representing the moment the intraday transaction is considered effective</value>
        [DataMember(Name = "valueDate", EmitDefaultValue = false)]
        public string ValueDateString
        {
            get => ValueDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => ValueDate = DateTimeOffset.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Date representing the moment the intraday transaction is considered effective
        /// </summary>
        /// <value>Date representing the moment the intraday transaction is considered effective</value>
        public DateTimeOffset ValueDate { get; set; }
    }
}
