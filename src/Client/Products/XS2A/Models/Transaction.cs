using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// <p>This is an object representing an account transaction. This object will give you the details of the financial transaction, including its amount and description.</p>
    /// <p>From this object, you can link back to its account.</p>
    /// <p>The transaction API endpoints are customer specific and therefore can only be accessed by providing the corresponding customer access token.</p>
    /// </summary>
    [DataContract]
    public class Transaction : Identified<Guid>
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
        /// Bank transaction code, based on &lt;a href&#x3D;&#39;https://www.iso20022.org/catalogue-messages/additional-content-messages/external-code-sets&#39;&gt;ISO 20022&lt;/a&gt;
        /// </summary>
        /// <value>Bank transaction code, based on &lt;a href&#x3D;&#39;https://www.iso20022.org/catalogue-messages/additional-content-messages/external-code-sets&#39;&gt;ISO 20022&lt;/a&gt;</value>
        [DataMember(Name = "bankTransactionCode", EmitDefaultValue = false)]
        public string BankTransactionCode { get; set; }

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
        /// Legal name of the counterpart
        /// </summary>
        /// <value>Legal name of the counterpart</value>
        [DataMember(Name = "counterpartName", EmitDefaultValue = false)]
        public string CounterpartName { get; set; }

        /// <summary>
        /// Number representing the counterpart&#39;s account
        /// </summary>
        /// <value>Number representing the counterpart&#39;s account</value>
        [DataMember(Name = "counterpartReference", EmitDefaultValue = false)]
        public string CounterpartReference { get; set; }

        /// <summary>
        /// When this transaction was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this transaction was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Identification of the creditor, e.g. a SEPA Creditor ID.
        /// </summary>
        /// <value>Identification of the creditor, e.g. a SEPA Creditor ID.</value>
        [DataMember(Name = "creditorId", EmitDefaultValue = false)]
        public string CreditorId { get; set; }

        /// <summary>
        /// Currency of the transaction, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_4217&#39;&gt;ISO4217&lt;/a&gt; format
        /// </summary>
        /// <value>Currency of the transaction, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_4217&#39;&gt;ISO4217&lt;/a&gt; format</value>
        [DataMember(Name = "currency", EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        /// Description of the transaction
        /// </summary>
        /// <value>Description of the transaction</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// A Base64-encoded SHA-256 digest of the transaction payload from the financial institution. This may NOT be unique if the exact same transaction happens on the same day, on the same account.
        /// </summary>
        /// <value>A Base64-encoded SHA-256 digest of the transaction payload from the financial institution. This may NOT be unique if the exact same transaction happens on the same day, on the same account.</value>
        [DataMember(Name = "digest", EmitDefaultValue = false)]
        public string Digest { get; set; }

        /// <summary>
        /// Identifier assigned by the initiating party to identify the transaction. This identification is passed on, unchanged, throughout the entire end-to-end chain.
        /// </summary>
        /// <value>Identifier assigned by the initiating party to identify the transaction. This identification is passed on, unchanged, throughout the entire end-to-end chain.</value>
        [DataMember(Name = "endToEndId", EmitDefaultValue = false)]
        public string EndToEndId { get; set; }

        /// <summary>
        /// Date representing the moment the transaction has been recorded
        /// </summary>
        /// <value>Date representing the moment the transaction has been recorded</value>
        [DataMember(Name = "executionDate", EmitDefaultValue = false)]
        public DateTimeOffset ExecutionDate { get; set; }

        /// <summary>
        /// A fee that was withheld from this transaction at the financial institution
        /// </summary>
        /// <value>A fee that was withheld from this transaction at the financial institution</value>
        [DataMember(Name = "fee", EmitDefaultValue = false)]
        public decimal? Fee { get; set; }

        /// <summary>
        /// Internal resource reference given by the financial institution
        /// </summary>
        /// <value>Internal resource reference given by the financial institution</value>
        [DataMember(Name = "internalReference", EmitDefaultValue = false)]
        public string InternalReference { get; set; }

        /// <summary>
        /// Unique reference of the mandate which is signed between the remitter and the debtor
        /// </summary>
        /// <value>Unique reference of the mandate which is signed between the remitter and the debtor</value>
        [DataMember(Name = "mandateId", EmitDefaultValue = false)]
        public string MandateId { get; set; }

        /// <summary>
        /// Bank transaction code prorietary to the financial institution. Content will vary per financial institution
        /// </summary>
        /// <value>Bank transaction code prorietary to the financial institution. Content will vary per financial institution</value>
        [DataMember(Name = "proprietaryBankTransactionCode", EmitDefaultValue = false)]
        public string ProprietaryBankTransactionCode { get; set; }

        /// <summary>
        /// Purpose code, based on &lt;a href&#x3D;&#39;https://www.iso20022.org/&#39;&gt;ISO 20022&lt;/a&gt;
        /// </summary>
        /// <value>Purpose code, based on &lt;a href&#x3D;&#39;https://www.iso20022.org/&#39;&gt;ISO 20022&lt;/a&gt;</value>
        [DataMember(Name = "purposeCode", EmitDefaultValue = false)]
        public string PurposeCode { get; set; }

        /// <summary>
        /// Content of the remittance information (aka communication)
        /// </summary>
        /// <value>Content of the remittance information (aka communication)</value>
        [DataMember(Name = "remittanceInformation", EmitDefaultValue = false)]
        public string RemittanceInformation { get; set; }

        /// <summary>
        /// Type of remittance information. Can be &lt;code&gt;structured&lt;/code&gt; or &lt;code&gt;unstructured&lt;/code&gt;
        /// </summary>
        /// <value>Type of remittance information. Can be &lt;code&gt;structured&lt;/code&gt; or &lt;code&gt;unstructured&lt;/code&gt;</value>
        [DataMember(Name = "remittanceInformationType", EmitDefaultValue = false)]
        public string RemittanceInformationType { get; set; }

        /// <summary>
        /// When this transaction was last synchronized successfully. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this transaction was last synchronized successfully. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "updatedAt", EmitDefaultValue = false)]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Date representing the moment the transaction is considered effective
        /// </summary>
        /// <value>Date representing the moment the transaction is considered effective</value>
        [DataMember(Name = "valueDate", EmitDefaultValue = false)]
        public DateTimeOffset ValueDate { get; set; }

        /// <summary>
        /// ID of the account that this transaction belongs to.
        /// </summary>
        public Guid AccountId { get; set; }
    }

    /// <summary>
    /// Link to the account that this transaction belongs to.
    /// </summary>
    [DataContract]
    public class TransactionRelationships
    {
        /// <summary>
        /// Link to the account that this transaction belongs to.
        /// </summary>
        [DataMember(Name = "account", EmitDefaultValue = false)]
        public JsonApi.Relationship Account { get; set; }
    }
}
