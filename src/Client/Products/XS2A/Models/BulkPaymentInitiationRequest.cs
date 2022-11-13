using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// This is an object representing a bulk payment initiation request. When you want to request the initiation of payments on behalf of one of your customers, you can create one to start the authorization flow.
    /// </summary>
    [DataContract]
    public class BulkPaymentInitiationRequest
    {
        /// <summary>
        /// URI that your customer will be redirected to at the end of the authorization flow. HTTPS is required for live applications.
        /// </summary>
        /// <value>URI that your customer will be redirected to at the end of the authorization flow. HTTPS is required for live applications.</value>
        [DataMember(Name = "redirectUri", EmitDefaultValue = false)]
        public string RedirectUri { get; set; }

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
        /// Specify the batch booking preference. Request this bulk payment to be listed as one global transaction, or one transaction per payment instruction. The financial institution may ignore this parameter
        /// </summary>
        /// <value>Specify the batch booking preference. Request this bulk payment to be listed as one global transaction, or one transaction per payment instruction. The financial institution may ignore this parameter</value>
        [DataMember(Name = "batchBookingPreferred", EmitDefaultValue = false)]
        public bool BatchBookingPreferred { get; set; }

        /// <summary>
        /// Your internal reference to the explicit consent provided by your consumer. You should store this consent reference in case of dispute.
        /// </summary>
        /// <value>Your internal reference to the explicit consent provided by your consumer. You should store this consent reference in case of dispute.</value>
        [DataMember(Name = "consentReference", EmitDefaultValue = false)]
        public Guid ConsentReference { get; set; }

        /// <summary>
        /// Type of payment transfer. Will always be &lt;code&gt;sepa-credit-transfer&lt;/code&gt;
        /// </summary>
        /// <value>Type of payment transfer. Will always be &lt;code&gt;sepa-credit-transfer&lt;/code&gt;</value>
        [DataMember(Name = "productType", EmitDefaultValue = false)]
        public string ProductType { get; set; }

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
        /// When set to true, the returned &lt;code&gt;redirectUri&lt;/code&gt; will be the one of the financial institution, avoiding an extra hop on Ibanity for the customer. It also fixes app-to-app redirects on IOS. Defaults to &lt;code&gt;false&lt;/code&gt; to ensure backward compatibility.
        /// </summary>
        /// <value>When set to true, the returned &lt;code&gt;redirectUri&lt;/code&gt; will be the one of the financial institution, avoiding an extra hop on Ibanity for the customer. It also fixes app-to-app redirects on IOS. Defaults to &lt;code&gt;false&lt;/code&gt; to ensure backward compatibility.</value>
        [DataMember(Name = "allowFinancialInstitutionRedirectUri", EmitDefaultValue = false)]
        public bool AllowFinancialInstitutionRedirectUri { get; set; }

        /// <summary>
        /// When set to true, TPP managed authorization flow will be enabled. The financial institution will redirect your customer to the &lt;code&gt;redirectUri&lt;/code&gt; avoiding an extra hop on Ibanity. By default, the XS2A managed authorization flow is used. If set to &lt;code&gt;true&lt;/code&gt;, a &lt;code&gt;state&lt;/code&gt; must be provided.
        /// </summary>
        /// <value>When set to true, TPP managed authorization flow will be enabled. The financial institution will redirect your customer to the &lt;code&gt;redirectUri&lt;/code&gt; avoiding an extra hop on Ibanity. By default, the XS2A managed authorization flow is used. If set to &lt;code&gt;true&lt;/code&gt;, a &lt;code&gt;state&lt;/code&gt; must be provided.</value>
        [DataMember(Name = "skipIbanityCompletionCallback", EmitDefaultValue = false)]
        public bool SkipIbanityCompletionCallback { get; set; }

        /// <summary>
        /// &lt;p&gt;List of payment attribute objects to be included in the bulk payment.&lt;/p&gt;&lt;p&gt;Required attributes are &lt;code&gt;currency&lt;/code&gt;, &lt;code&gt;amount&lt;/code&gt;, &lt;code&gt;creditorName&lt;/code&gt;, &lt;code&gt;creditorAccountReference&lt;/code&gt;, &lt;code&gt;creditorAccountReferenceType&lt;/code&gt; and &lt;code&gt;endToEndId&lt;/code&gt;.&lt;/p&gt;&lt;p&gt;Optional attributes are &lt;code&gt;remittanceInformation&lt;/code&gt;, &lt;code&gt;remittanceInformationType&lt;/code&gt;, &lt;code&gt;creditorAgent&lt;/code&gt;, and &lt;code&gt;creditorAgentType&lt;/code&gt;.&lt;/p&gt;&lt;p&gt;For more information see the &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#create-payment-initiation-request-attributes&#39;&gt;create payment initiation request attributes&lt;/a&gt;&lt;/p&gt;
        /// </summary>
        /// <value>&lt;p&gt;List of payment attribute objects to be included in the bulk payment.&lt;/p&gt;&lt;p&gt;Required attributes are &lt;code&gt;currency&lt;/code&gt;, &lt;code&gt;amount&lt;/code&gt;, &lt;code&gt;creditorName&lt;/code&gt;, &lt;code&gt;creditorAccountReference&lt;/code&gt;, &lt;code&gt;creditorAccountReferenceType&lt;/code&gt; and &lt;code&gt;endToEndId&lt;/code&gt;.&lt;/p&gt;&lt;p&gt;Optional attributes are &lt;code&gt;remittanceInformation&lt;/code&gt;, &lt;code&gt;remittanceInformationType&lt;/code&gt;, &lt;code&gt;creditorAgent&lt;/code&gt;, and &lt;code&gt;creditorAgentType&lt;/code&gt;.&lt;/p&gt;&lt;p&gt;For more information see the &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#create-payment-initiation-request-attributes&#39;&gt;create payment initiation request attributes&lt;/a&gt;&lt;/p&gt;</value>
        [DataMember(Name = "payments", EmitDefaultValue = false)]
        public List<PaymentInitiationRequest> Payments { get; set; }
    }

    /// <summary>
    /// This is an object representing a resource batch-synchronization. This object will give you the details of the batch-synchronization.
    /// </summary>
    [DataContract]
    public class BulkPaymentInitiationRequestResponse : BulkPaymentInitiationRequest, IIdentified<Guid>
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <summary>
        /// Status of the bulk payment initiation request. &lt;a href&#x3D;&#39;/isabel-connect/products#bulk-payment-statuses&#39;&gt;See possible statuses&lt;/a&gt;.
        /// </summary>
        /// <value>Status of the bulk payment initiation request. &lt;a href&#x3D;&#39;/isabel-connect/products#bulk-payment-statuses&#39;&gt;See possible statuses&lt;/a&gt;.</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Details about the reason why the payment was refused by the financial institution.
        /// </summary>
        /// <value>Details about the reason why the payment was refused by the financial institution.</value>
        [DataMember(Name = "statusReason", EmitDefaultValue = false)]
        public Object StatusReason { get; set; }
    }
}
