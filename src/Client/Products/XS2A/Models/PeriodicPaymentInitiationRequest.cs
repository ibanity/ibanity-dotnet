using System;
using System.Globalization;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// <p>This is an object representing a periodic payment initiation request. When you want to initiate periodic payment from one of your customers, you have to create one to start the authorization flow.</p>
    /// <p>When creating the periodic payment initiation request, you will receive the redirect link in the response. Your customer should be redirected to this url to begin the authorization process. At the end of the flow, they will be returned to the redirect uri that you defined.</p>
    /// <p>If the periodic payment initiation is not authorized (for example when the customer cancels the flow), an error query parameter will be added to the redirect uri with the value rejected.</p>
    /// <p>When authorizing periodic payment initiation from a financial institution user (in the sandbox), you should use 123456 as the digipass response.</p>
    /// </summary>
    [DataContract]
    public class PeriodicPaymentInitiationRequest
    {
        /// <summary>
        /// URI that your customer will be redirected to at the end of the authorization flow. HTTPS is required for live applications.
        /// </summary>
        /// <value>URI that your customer will be redirected to at the end of the authorization flow. HTTPS is required for live applications.</value>
        [DataMember(Name = "redirectUri", EmitDefaultValue = false)]
        public string RedirectUri { get; set; }

        /// <summary>
        /// Your internal reference to the explicit consent provided by your consumer. You should store this consent reference in case of dispute.
        /// </summary>
        /// <value>Your internal reference to the explicit consent provided by your consumer. You should store this consent reference in case of dispute.</value>
        [DataMember(Name = "consentReference", EmitDefaultValue = false)]
        public string ConsentReference { get; set; }

        /// <summary>
        /// Type of periodic payment transfer. Will always be &lt;code&gt;sepa-credit-transfer&lt;/code&gt;
        /// </summary>
        /// <value>Type of periodic payment transfer. Will always be &lt;code&gt;sepa-credit-transfer&lt;/code&gt;</value>
        [DataMember(Name = "productType", EmitDefaultValue = false)]
        public string ProductType { get; set; }

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
        /// A date in the future when the payment is requested to be executed. The availability of this feature depends on each financial institution. See &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#financial-institution-attributes&#39;&gt;financial institution attributes&lt;/a&gt;
        /// </summary>
        /// <value>A date in the future when the payment is requested to be executed. The availability of this feature depends on each financial institution. See &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#financial-institution-attributes&#39;&gt;financial institution attributes&lt;/a&gt;</value>
        [DataMember(Name = "requestedExecutionDate", EmitDefaultValue = false)]
        public string RequestedExecutionDateString
        {
            get => RequestedExecutionDate.HasValue
                ? RequestedExecutionDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                : null;
            set => RequestedExecutionDate = !string.IsNullOrWhiteSpace(value)
                ? (DateTimeOffset?)DateTimeOffset.Parse(value, CultureInfo.InvariantCulture)
                : null;
        }

        /// <summary>
        /// A date in the future when the payment is requested to be executed. The availability of this feature depends on each financial institution. See &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#financial-institution-attributes&#39;&gt;financial institution attributes&lt;/a&gt;
        /// </summary>
        /// <value>A date in the future when the payment is requested to be executed. The availability of this feature depends on each financial institution. See &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#financial-institution-attributes&#39;&gt;financial institution attributes&lt;/a&gt;</value>
        public DateTimeOffset? RequestedExecutionDate { get; set; }

        /// <summary>
        /// Currency of the periodic payment initiation request, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_4217&#39;&gt;ISO4217&lt;/a&gt; format
        /// </summary>
        /// <value>Currency of the periodic payment initiation request, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_4217&#39;&gt;ISO4217&lt;/a&gt; format</value>
        [DataMember(Name = "currency", EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        /// Amount of funds being requested
        /// </summary>
        /// <value>Amount of funds being requested</value>
        [DataMember(Name = "amount", EmitDefaultValue = false)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Name of the paying customer
        /// </summary>
        /// <value>Name of the paying customer</value>
        [DataMember(Name = "debtorName", EmitDefaultValue = false)]
        public string DebtorName { get; set; }

        /// <summary>
        /// Financial institution&#39;s internal reference for the payer&#39;s account
        /// </summary>
        /// <value>Financial institution&#39;s internal reference for the payer&#39;s account</value>
        [DataMember(Name = "debtorAccountReference", EmitDefaultValue = false)]
        public string DebtorAccountReference { get; set; }

        /// <summary>
        /// Type of account reference, such as &lt;code&gt;IBAN&lt;/code&gt;. Mandatory when &lt;code&gt;debtorAccountReference&lt;/code&gt; is provided.
        /// </summary>
        /// <value>Type of account reference, such as &lt;code&gt;IBAN&lt;/code&gt;. Mandatory when &lt;code&gt;debtorAccountReference&lt;/code&gt; is provided.</value>
        [DataMember(Name = "debtorAccountReferenceType", EmitDefaultValue = false)]
        public string DebtorAccountReferenceType { get; set; }

        /// <summary>
        /// Name of the payee
        /// </summary>
        /// <value>Name of the payee</value>
        [DataMember(Name = "creditorName", EmitDefaultValue = false)]
        public string CreditorName { get; set; }

        /// <summary>
        /// Financial institution&#39;s internal reference for the payee&#39;s account
        /// </summary>
        /// <value>Financial institution&#39;s internal reference for the payee&#39;s account</value>
        [DataMember(Name = "creditorAccountReference", EmitDefaultValue = false)]
        public string CreditorAccountReference { get; set; }

        /// <summary>
        /// Type of payee&#39;s account reference, such as &lt;code&gt;IBAN&lt;/code&gt;
        /// </summary>
        /// <value>Type of payee&#39;s account reference, such as &lt;code&gt;IBAN&lt;/code&gt;</value>
        [DataMember(Name = "creditorAccountReferenceType", EmitDefaultValue = false)]
        public string CreditorAccountReferenceType { get; set; }

        /// <summary>
        /// Reference to the financial institution
        /// </summary>
        /// <value>Reference to the financial institution</value>
        [DataMember(Name = "creditorAgent", EmitDefaultValue = false)]
        public string CreditorAgent { get; set; }

        /// <summary>
        /// Type of financial institution reference, such as &lt;code&gt;BIC&lt;/code&gt;
        /// </summary>
        /// <value>Type of financial institution reference, such as &lt;code&gt;BIC&lt;/code&gt;</value>
        [DataMember(Name = "creditorAgentType", EmitDefaultValue = false)]
        public string CreditorAgentType { get; set; }

        /// <summary>
        /// Identifier assigned by the initiating party to identify the transaction. This identification is passed on, unchanged, throughout the entire end-to-end chain and so it has to follow the &lt;a href&#x3D;&#39;https://www.iso20022.org/&#39;&gt;ISO 20022&lt;/a&gt; constraint of being at most 35 characters long. A good way to generate such a unique identifier is to take a UUID and strip the dashes.
        /// </summary>
        /// <value>Identifier assigned by the initiating party to identify the transaction. This identification is passed on, unchanged, throughout the entire end-to-end chain and so it has to follow the &lt;a href&#x3D;&#39;https://www.iso20022.org/&#39;&gt;ISO 20022&lt;/a&gt; constraint of being at most 35 characters long. A good way to generate such a unique identifier is to take a UUID and strip the dashes.</value>
        [DataMember(Name = "endToEndId", EmitDefaultValue = false)]
        public string EndToEndId { get; set; }

        /// <summary>
        /// Represents the language to be used in the authorization interface (&lt;code&gt;\&quot;en\&quot;&lt;/code&gt;, &lt;code&gt;\&quot;nl\&quot;&lt;/code&gt;, or &lt;code&gt;\&quot;fr\&quot;&lt;/code&gt;). The default language is English.
        /// </summary>
        /// <value>Represents the language to be used in the authorization interface (&lt;code&gt;\&quot;en\&quot;&lt;/code&gt;, &lt;code&gt;\&quot;nl\&quot;&lt;/code&gt;, or &lt;code&gt;\&quot;fr\&quot;&lt;/code&gt;). The default language is English.</value>
        [DataMember(Name = "locale", EmitDefaultValue = false)]
        public string Locale { get; set; }

        /// <summary>
        /// Specifies the IP of the customer, to be passed to the financial institution. Required for some financial institutions as indicated in their &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#financial-institution-attributes&#39;&gt;attributes&lt;/a&gt;.
        /// </summary>
        /// <value>Specifies the IP of the customer, to be passed to the financial institution. Required for some financial institutions as indicated in their &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#financial-institution-attributes&#39;&gt;attributes&lt;/a&gt;.</value>
        [DataMember(Name = "customerIpAddress", EmitDefaultValue = false)]
        public string CustomerIpAddress { get; set; }

        /// <summary>
        /// When set to true, the returned &lt;code&gt;redirectUri&lt;/code&gt; will be the one of the financial institution, avoiding an extra hop on Ibanity for the customer. It also fixes app-to-app redirects on IOS. Defaults to &lt;code&gt;false&lt;/code&gt; to ensure backward compatibility.
        /// </summary>
        /// <value>When set to true, the returned &lt;code&gt;redirectUri&lt;/code&gt; will be the one of the financial institution, avoiding an extra hop on Ibanity for the customer. It also fixes app-to-app redirects on IOS. Defaults to &lt;code&gt;false&lt;/code&gt; to ensure backward compatibility.</value>
        [DataMember(Name = "allowFinancialInstitutionRedirectUri", EmitDefaultValue = true)]
        public bool AllowFinancialInstitutionRedirectUri { get; set; }

        /// <summary>
        /// When set to true, TPP managed authorization flow will be enabled. The financial institution will redirect your customer to the &lt;code&gt;redirectUri&lt;/code&gt; avoiding an extra hop on Ibanity. By default, the XS2A managed authorization flow is used. If set to &lt;code&gt;true&lt;/code&gt;, a &lt;code&gt;state&lt;/code&gt; must be provided.
        /// </summary>
        /// <value>When set to true, TPP managed authorization flow will be enabled. The financial institution will redirect your customer to the &lt;code&gt;redirectUri&lt;/code&gt; avoiding an extra hop on Ibanity. By default, the XS2A managed authorization flow is used. If set to &lt;code&gt;true&lt;/code&gt;, a &lt;code&gt;state&lt;/code&gt; must be provided.</value>
        [DataMember(Name = "skipIbanityCompletionCallback", EmitDefaultValue = true)]
        public bool SkipIbanityCompletionCallback { get; set; }

        /// <summary>
        /// The state you want to append to the redirectUri at the end of the authorization flow.
        /// </summary>
        /// <value>The state you want to append to the redirectUri at the end of the authorization flow.</value>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public string State { get; set; }

        /// <summary>
        /// Date of the first execution of the periodic payment
        /// </summary>
        /// <value>Date of the first execution of the periodic payment</value>
        [DataMember(Name = "startDate", EmitDefaultValue = false)]
        public string StartDateString
        {
            get => StartDate.HasValue
                ? StartDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                : null;
            set => StartDate = !string.IsNullOrWhiteSpace(value)
                ? (DateTimeOffset?)DateTimeOffset.Parse(value, CultureInfo.InvariantCulture)
                : null;
        }

        /// <summary>
        /// Date of the first execution of the periodic payment
        /// </summary>
        /// <value>Date of the first execution of the periodic payment</value>
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// Date of the last execution of the periodic payment
        /// </summary>
        /// <value>Date of the last execution of the periodic payment</value>
        [DataMember(Name = "endDate", EmitDefaultValue = false)]
        public string EndDateString
        {
            get => EndDate.HasValue
                ? EndDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                : null;
            set => EndDate = !string.IsNullOrWhiteSpace(value)
                ? (DateTimeOffset?)DateTimeOffset.Parse(value, CultureInfo.InvariantCulture)
                : null;
        }

        /// <summary>
        /// Date of the last execution of the periodic payment
        /// </summary>
        /// <value>Date of the last execution of the periodic payment</value>
        public DateTimeOffset? EndDate { get; set; }

        /// <summary>
        /// Frequency of the periodic payment. Can be &lt;code&gt;weekly&lt;/code&gt;, &lt;code&gt;everyTwoWeeks&lt;/code&gt;, &lt;code&gt;monthly&lt;/code&gt;, &lt;code&gt;everyTwoMonths&lt;/code&gt;, &lt;code&gt;quarterly&lt;/code&gt;, &lt;code&gt;semiannual&lt;/code&gt; or &lt;code&gt;annual&lt;/code&gt;
        /// </summary>
        /// <value>Frequency of the periodic payment. Can be &lt;code&gt;weekly&lt;/code&gt;, &lt;code&gt;everyTwoWeeks&lt;/code&gt;, &lt;code&gt;monthly&lt;/code&gt;, &lt;code&gt;everyTwoMonths&lt;/code&gt;, &lt;code&gt;quarterly&lt;/code&gt;, &lt;code&gt;semiannual&lt;/code&gt; or &lt;code&gt;annual&lt;/code&gt;</value>
        [DataMember(Name = "frequency", EmitDefaultValue = false)]
        public string Frequency { get; set; }

        /// <summary>
        /// Defines what to do if the execution should fall on a closing day. Can be &lt;code&gt;following&lt;/code&gt; to execute on the next opening day, or &lt;code&gt;preceding&lt;/code&gt; to execute on the previous opening day
        /// </summary>
        /// <value>Defines what to do if the execution should fall on a closing day. Can be &lt;code&gt;following&lt;/code&gt; to execute on the next opening day, or &lt;code&gt;preceding&lt;/code&gt; to execute on the previous opening day</value>
        [DataMember(Name = "executionRule", EmitDefaultValue = false)]
        public string ExecutionRule { get; set; }

        /// <summary>
        /// Identifier the customer uses to log into the financial institution&#39;s online portal. Required for some financial institutions as indicated in their &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#financial-institution-attributes&#39;&gt;attributes&lt;/a&gt;.
        /// </summary>
        /// <value>Identifier the customer uses to log into the financial institution&#39;s online portal. Required for some financial institutions as indicated in their &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#financial-institution-attributes&#39;&gt;attributes&lt;/a&gt;.</value>
        [DataMember(Name = "financialInstitutionCustomerReference", EmitDefaultValue = false)]
        public string FinancialInstitutionCustomerReference { get; set; }
    }

    /// <summary>
    /// This is an object representing a resource batch-synchronization. This object will give you the details of the batch-synchronization.
    /// </summary>
    [DataContract]
    public class PeriodicPaymentInitiationRequestResponse : PeriodicPaymentInitiationRequest, IIdentified<Guid>
    {
        /// <inheritdoc />
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// Current status of the periodic payment initiation request. Possible values are &lt;code&gt;accepted-customer-profile&lt;/code&gt;, &lt;code&gt;accepted-settlement-completed&lt;/code&gt;, &lt;code&gt;accepted-settlement-in-progress&lt;/code&gt;, &lt;code&gt;accepted-technical-validation&lt;/code&gt;, &lt;code&gt;accepted-with-change&lt;/code&gt;, &lt;code&gt;accepted-without-posting&lt;/code&gt;, &lt;code&gt;received&lt;/code&gt;, &lt;code&gt;pending&lt;/code&gt;, and &lt;code&gt;rejected&lt;/code&gt;. &lt;a href&#x3D;&#39;/xs2a/products#pir-status-codes&#39;&gt;See status definitions.&lt;/a&gt;
        /// </summary>
        /// <value>Current status of the periodic payment initiation request. Possible values are &lt;code&gt;accepted-customer-profile&lt;/code&gt;, &lt;code&gt;accepted-settlement-completed&lt;/code&gt;, &lt;code&gt;accepted-settlement-in-progress&lt;/code&gt;, &lt;code&gt;accepted-technical-validation&lt;/code&gt;, &lt;code&gt;accepted-with-change&lt;/code&gt;, &lt;code&gt;accepted-without-posting&lt;/code&gt;, &lt;code&gt;received&lt;/code&gt;, &lt;code&gt;pending&lt;/code&gt;, and &lt;code&gt;rejected&lt;/code&gt;. &lt;a href&#x3D;&#39;/xs2a/products#pir-status-codes&#39;&gt;See status definitions.&lt;/a&gt;</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Details about the reason why the periodic payment was refused by the financial institution.
        /// </summary>
        /// <value>Details about the reason why the periodic payment was refused by the financial institution.</value>
        [DataMember(Name = "statusReason", EmitDefaultValue = false)]
        public string StatusReason { get; set; }
    }
}
