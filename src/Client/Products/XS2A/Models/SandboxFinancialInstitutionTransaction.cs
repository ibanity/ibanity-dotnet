using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// <p>This is an object representing a financial institution transaction, a fake transaction on a fake account you can create for test purposes.</p>
    /// <p>In addition to the regular transaction API calls, there are sandbox-specific endpoints available to manage your sandbox data.</p>
    /// <p>From this object, you can follow the link to its related financial institution account</p>
    /// </summary>
    [DataContract]
    public class SandboxFinancialInstitutionTransaction
    {
        /// <summary>
        /// Date representing the moment the financial institution transaction is considered effective
        /// </summary>
        /// <value>Date representing the moment the financial institution transaction is considered effective</value>
        [DataMember(Name = "valueDate", IsRequired = true, EmitDefaultValue = true)]
        public DateTimeOffset? ValueDate { get; set; }

        /// <summary>
        /// Date representing the moment the financial institution transaction has been recorded
        /// </summary>
        /// <value>Date representing the moment the financial institution transaction has been recorded</value>
        [DataMember(Name = "executionDate", IsRequired = true, EmitDefaultValue = true)]
        public DateTimeOffset? ExecutionDate { get; set; }

        /// <summary>
        /// Amount of the financial institution transaction. Can be positive or negative
        /// </summary>
        /// <value>Amount of the financial institution transaction. Can be positive or negative</value>
        [DataMember(Name = "amount", IsRequired = true, EmitDefaultValue = true)]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Currency of the financial institution transaction, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_4217&#39;&gt;ISO4217&lt;/a&gt; format
        /// </summary>
        /// <value>Currency of the financial institution transaction, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_4217&#39;&gt;ISO4217&lt;/a&gt; format</value>
        [DataMember(Name = "currency", IsRequired = true, EmitDefaultValue = true)]
        public string Currency { get; set; }

        /// <summary>
        /// Legal name of the counterpart. Can only be updated if it was previously not provided (blank).
        /// </summary>
        /// <value>Legal name of the counterpart. Can only be updated if it was previously not provided (blank).</value>
        [DataMember(Name = "counterpartName", EmitDefaultValue = false)]
        public string CounterpartName { get; set; }

        /// <summary>
        /// Number representing the counterpart&#39;s account
        /// </summary>
        /// <value>Number representing the counterpart&#39;s account</value>
        [DataMember(Name = "counterpartReference", EmitDefaultValue = false)]
        public string CounterpartReference { get; set; }

        /// <summary>
        /// Description of the financial institution transaction
        /// </summary>
        /// <value>Description of the financial institution transaction</value>
        [DataMember(Name = "description", IsRequired = true, EmitDefaultValue = true)]
        public string Description { get; set; }

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
        [DataMember(Name = "remittanceInformationType", IsRequired = true, EmitDefaultValue = true)]
        public string RemittanceInformationType { get; set; }

        /// <summary>
        /// Identifier assigned by the initiating party to identify the transaction. This identification is passed on, unchanged, throughout the entire end-to-end chain.
        /// </summary>
        /// <value>Identifier assigned by the initiating party to identify the transaction. This identification is passed on, unchanged, throughout the entire end-to-end chain.</value>
        [DataMember(Name = "endToEndId", EmitDefaultValue = false)]
        public string EndToEndId { get; set; }

        /// <summary>
        /// Purpose code, based on &lt;a href&#x3D;&#39;https://www.iso20022.org/&#39;&gt;ISO 20022&lt;/a&gt;
        /// </summary>
        /// <value>Purpose code, based on &lt;a href&#x3D;&#39;https://www.iso20022.org/&#39;&gt;ISO 20022&lt;/a&gt;</value>
        [DataMember(Name = "purposeCode", EmitDefaultValue = false)]
        public string PurposeCode { get; set; }

        /// <summary>
        /// Unique reference of the mandate which is signed between the remitter and the debtor
        /// </summary>
        /// <value>Unique reference of the mandate which is signed between the remitter and the debtor</value>
        [DataMember(Name = "mandateId", EmitDefaultValue = false)]
        public string MandateId { get; set; }

        /// <summary>
        /// Identification of the creditor, e.g. a SEPA Creditor ID.
        /// </summary>
        /// <value>Identification of the creditor, e.g. a SEPA Creditor ID.</value>
        [DataMember(Name = "creditorId", EmitDefaultValue = false)]
        public string CreditorId { get; set; }

        /// <summary>
        /// Additional transaction-related information provided from the financial institution to the customer
        /// </summary>
        /// <value>Additional transaction-related information provided from the financial institution to the customer</value>
        [DataMember(Name = "additionalInformation", EmitDefaultValue = false)]
        public string AdditionalInformation { get; set; }

        /// <summary>
        /// Bank transaction code prorietary to the financial institution. Content will vary per financial institution
        /// </summary>
        /// <value>Bank transaction code prorietary to the financial institution. Content will vary per financial institution</value>
        [DataMember(Name = "proprietaryBankTransactionCode", EmitDefaultValue = false)]
        public string ProprietaryBankTransactionCode { get; set; }

        /// <summary>
        /// Bank transaction code, based on &lt;a href&#x3D;&#39;https://www.iso20022.org/catalogue-messages/additional-content-messages/external-code-sets&#39;&gt;ISO 20022&lt;/a&gt;
        /// </summary>
        /// <value>Bank transaction code, based on &lt;a href&#x3D;&#39;https://www.iso20022.org/catalogue-messages/additional-content-messages/external-code-sets&#39;&gt;ISO 20022&lt;/a&gt;</value>
        [DataMember(Name = "bankTransactionCode", EmitDefaultValue = false)]
        public string BankTransactionCode { get; set; }
    }

    /// <inheritdoc />
    public class SandboxFinancialInstitutionTransactionResponse : SandboxFinancialInstitutionTransaction, IIdentified<Guid>
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <summary>
        /// Reference for card related to the transaction (if any). For example the last 4 digits of the card number.
        /// </summary>
        /// <value>Reference for card related to the transaction (if any). For example the last 4 digits of the card number.</value>
        [DataMember(Name = "cardReference", EmitDefaultValue = false)]
        public string CardReference { get; set; }

        /// <summary>
        /// Type of card reference (can be &lt;code&gt;PAN&lt;/code&gt; or &lt;code&gt;MASKEDPAN&lt;/code&gt;)
        /// </summary>
        /// <value>Type of card reference (can be &lt;code&gt;PAN&lt;/code&gt; or &lt;code&gt;MASKEDPAN&lt;/code&gt;)</value>
        [DataMember(Name = "cardReferenceType", EmitDefaultValue = false)]
        public string CardReferenceType { get; set; }

        /// <summary>
        /// When this financial institution transaction was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this financial institution transaction was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// A fee that was withheld from this transaction at the financial institution
        /// </summary>
        /// <value>A fee that was withheld from this transaction at the financial institution</value>
        [DataMember(Name = "fee", EmitDefaultValue = false)]
        public decimal Fee { get; set; }

        /// <summary>
        /// When this financial institution transaction was last synchronized successfully. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this financial institution transaction was last synchronized successfully. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "updatedAt", EmitDefaultValue = false)]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
