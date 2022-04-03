using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Models
{
    [DataContract]
    public class BulkPayment
    {
        /// <summary>
        /// Your identifier for the bulk payment, displayed to the user in the Ponto dashboard
        /// </summary>
        /// <value>Your identifier for the bulk payment, displayed to the user in the Ponto dashboard</value>
        [DataMember(Name = "reference", IsRequired = true, EmitDefaultValue = false)]
        public string Reference { get; set; }

        /// <summary>
        /// A date in the future when the payment is requested to be executed. The availability of this feature depends on each financial institution. See &lt;a href&#x3D;&#39;https://documentation.ibanity.com/ponto-connect/api#financial-institution-attributes&#39;&gt;financial institution attributes&lt;/a&gt;
        /// </summary>
        /// <value>A date in the future when the payment is requested to be executed. The availability of this feature depends on each financial institution. See &lt;a href&#x3D;&#39;https://documentation.ibanity.com/ponto-connect/api#financial-institution-attributes&#39;&gt;financial institution attributes&lt;/a&gt;</value>
        [DataMember(Name = "requestedExecutionDate", EmitDefaultValue = false)]
        public DateTimeOffset RequestedExecutionDate { get; set; }

        /// <summary>
        /// &lt;p&gt;Indicates whether the bulk payment transactions should be grouped into one booking entry by the financial institution instead of individual transactions.&lt;/p&gt;&lt;p&gt;This is a preference that may or may not be taken into account by the financial institution based on availability and the customer&#39;s bulk payment contract.&lt;/p&gt;&lt;p&gt;Defaults to &lt;code&gt;false&lt;/code&gt;&lt;/p&gt;
        /// </summary>
        /// <value>&lt;p&gt;Indicates whether the bulk payment transactions should be grouped into one booking entry by the financial institution instead of individual transactions.&lt;/p&gt;&lt;p&gt;This is a preference that may or may not be taken into account by the financial institution based on availability and the customer&#39;s bulk payment contract.&lt;/p&gt;&lt;p&gt;Defaults to &lt;code&gt;false&lt;/code&gt;&lt;/p&gt;</value>
        [DataMember(Name = "batchBookingPreferred", EmitDefaultValue = true)]
        public bool BatchBookingPreferred { get; set; }
    }

    public abstract class BulkPaymentRequest
    {
        /// <summary>
        /// URI that your user will be redirected to at the end of the authorization flow.&lt;/a&gt;
        /// </summary>
        /// <value>URI that your user will be redirected to at the end of the authorization flow.&lt;/a&gt;</value>
        [DataMember(Name = "redirectUri", EmitDefaultValue = false)]
        public string RedirectUri { get; set; }

        /// <summary>
        /// &lt;p&gt;List of payment attribute objects to be included in the bulk payment.&lt;/p&gt;&lt;p&gt;Required attributes are &lt;code&gt;currency&lt;/code&gt; (currently must be &lt;code&gt;EUR&lt;/code&gt;), &lt;code&gt;amount&lt;/code&gt;, &lt;code&gt;creditorName&lt;/code&gt;, &lt;code&gt;creditorAccountReference&lt;/code&gt;, and &lt;code&gt;creditorAccountReferenceType&lt;/code&gt;.&lt;/p&gt;&lt;p&gt;Optional attributes are &lt;code&gt;remittanceInformation&lt;/code&gt;, &lt;code&gt;remittanceInformationType&lt;/code&gt;, &lt;code&gt;creditorAgent&lt;/code&gt;, and &lt;code&gt;creditorAgentType&lt;/code&gt;.&lt;/p&gt;&lt;p&gt;For more information see the &lt;a href&#x3D;&#39;https://documentation.ibanity.com/ponto-connect/api#create-payment-attributes&#39;&gt;create payment attributes&lt;/a&gt;&lt;/p&gt;
        /// </summary>
        /// <value>&lt;p&gt;List of payment attribute objects to be included in the bulk payment.&lt;/p&gt;&lt;p&gt;Required attributes are &lt;code&gt;currency&lt;/code&gt; (currently must be &lt;code&gt;EUR&lt;/code&gt;), &lt;code&gt;amount&lt;/code&gt;, &lt;code&gt;creditorName&lt;/code&gt;, &lt;code&gt;creditorAccountReference&lt;/code&gt;, and &lt;code&gt;creditorAccountReferenceType&lt;/code&gt;.&lt;/p&gt;&lt;p&gt;Optional attributes are &lt;code&gt;remittanceInformation&lt;/code&gt;, &lt;code&gt;remittanceInformationType&lt;/code&gt;, &lt;code&gt;creditorAgent&lt;/code&gt;, and &lt;code&gt;creditorAgentType&lt;/code&gt;.&lt;/p&gt;&lt;p&gt;For more information see the &lt;a href&#x3D;&#39;https://documentation.ibanity.com/ponto-connect/api#create-payment-attributes&#39;&gt;create payment attributes&lt;/a&gt;&lt;/p&gt;</value>
        [DataMember(Name = "payments", IsRequired = true, EmitDefaultValue = false)]
        public List<Payment> Payments { get; set; }
    }

    public class BulkPaymentResponse : BulkPayment, IIdentified<Guid>
    {
        /// <summary>
        /// Current status of the bulk payment.
        /// </summary>
        /// <value>Current status of the bulk payment.</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// URI to redirect to from your customer frontend to conduct the authorization flow.
        /// </summary>
        /// <value>URI to redirect to from your customer frontend to conduct the authorization flow.</value>
        [DataMember(Name = "redirectUri", EmitDefaultValue = false)]
        public string RedirectUri { get; set; }

        /// <summary>
        /// URI to redirect to from your customer frontend to conduct the authorization flow.
        /// </summary>
        /// <value>URI to redirect to from your customer frontend to conduct the authorization flow.</value>
        public Uri Redirect => string.IsNullOrWhiteSpace(RedirectUri)
            ? null
            : new Uri(RedirectUri);

        public Guid Id { get; set; }
    }
}
