using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// <p>This is an object representing an account information access request. When you want to access the account information of one of your customers, you have to create one to start the authorization flow.</p>
    /// <p>When creating the account information access request, you will receive the redirect link in the response. Your customer should be redirected to this url to begin the authorization process. At the end of the flow, they will be returned to the redirect uri that you defined.</p>
    /// <p>If the access request is not authorized (for example when the customer cancels the flow), an error query parameter will be added to the redirect uri. The possible values of this parameter are access_denied and unsupported_multi_currency_account.</p>
    /// <p>When authorizing account access by a financial institution user (in the sandbox), you should use 123456 as the digipass response. You can also use the Ibanity Sandbox Authorization Portal CLI to automate this authorization.</p>
    /// </summary>
    [DataContract]
    public class AccountInformationAccessRequest
    {
        /// <summary>
        /// &lt;p&gt;An array of the references for the accounts to be accessed. The minimum and maximum permitted number of account references can be found in the &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#financial-institution-attributes&#39;&gt;financial institution attributes&lt;/a&gt;. For some financial institutions the accounts can be chosen later, during the authorization process.&lt;/p&gt;
        /// </summary>
        /// <value>&lt;p&gt;An array of the references for the accounts to be accessed. The minimum and maximum permitted number of account references can be found in the &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#financial-institution-attributes&#39;&gt;financial institution attributes&lt;/a&gt;. For some financial institutions the accounts can be chosen later, during the authorization process.&lt;/p&gt;</value>
        [DataMember(Name = "requestedAccountReferences", EmitDefaultValue = false)]
        public List<string> RequestedAccountReferences { get; set; }
    }

    /// <inheritdoc />
    [DataContract]
    public class AccountInformationAccessRequestRequest : AccountInformationAccessRequest
    {
        /// <summary>
        /// URI that your customer will be redirected to at the end of the authorization flow. HTTPS is required for live applications.
        /// </summary>
        /// <value>URI that your customer will be redirected to at the end of the authorization flow. HTTPS is required for live applications.</value>
        [DataMember(Name = "redirectUri", IsRequired = true, EmitDefaultValue = true)]
        public string RedirectUri { get; set; }

        /// <summary>
        /// Your internal reference to the explicit consent provided by your consumer. You should store this consent reference in case of dispute.
        /// </summary>
        /// <value>Your internal reference to the explicit consent provided by your consumer. You should store this consent reference in case of dispute.</value>
        [DataMember(Name = "consentReference", IsRequired = true, EmitDefaultValue = true)]
        public string ConsentReference { get; set; }

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
        public bool? AllowFinancialInstitutionRedirectUri { get; set; }

        /// <summary>
        /// Allow support for multicurrency accounts. Defaults to &lt;code&gt;false&lt;/code&gt;
        /// </summary>
        /// <value>Allow support for multicurrency accounts. Defaults to &lt;code&gt;false&lt;/code&gt;</value>
        [DataMember(Name = "allowMulticurrencyAccounts", EmitDefaultValue = true)]
        public bool? AllowMulticurrencyAccounts { get; set; }

        /// <summary>
        /// When set to true, TPP managed authorization flow will be enabled. The financial institution will redirect your customer to the &lt;code&gt;redirectUri&lt;/code&gt; avoiding an extra hop on Ibanity. By default, the XS2A managed authorization flow is used. If set to &lt;code&gt;true&lt;/code&gt;, a &lt;code&gt;state&lt;/code&gt; must be provided.
        /// </summary>
        /// <value>When set to true, TPP managed authorization flow will be enabled. The financial institution will redirect your customer to the &lt;code&gt;redirectUri&lt;/code&gt; avoiding an extra hop on Ibanity. By default, the XS2A managed authorization flow is used. If set to &lt;code&gt;true&lt;/code&gt;, a &lt;code&gt;state&lt;/code&gt; must be provided.</value>
        [DataMember(Name = "skipIbanityCompletionCallback", EmitDefaultValue = true)]
        public bool? SkipIbanityCompletionCallback { get; set; }

        /// <summary>
        /// The state you want to append to the redirectUri at the end of the authorization flow.
        /// </summary>
        /// <value>The state you want to append to the redirectUri at the end of the authorization flow.</value>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public string State { get; set; }

        /// <summary>
        /// Identifier the customer uses to log into the financial institution&#39;s online portal. Required for some financial institutions as indicated in their &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#financial-institution-attributes&#39;&gt;attributes&lt;/a&gt;.
        /// </summary>
        /// <value>Identifier the customer uses to log into the financial institution&#39;s online portal. Required for some financial institutions as indicated in their &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#financial-institution-attributes&#39;&gt;attributes&lt;/a&gt;.</value>
        [DataMember(Name = "financialInstitutionCustomerReference", EmitDefaultValue = false)]
        public string FinancialInstitutionCustomerReference { get; set; }

        /// <summary>
        /// Allowed type of accounts that will be accepted in the account information access request. Can be &lt;code&gt;checking&lt;/code&gt;, &lt;code&gt;savings&lt;/code&gt;, &lt;code&gt;securities&lt;/code&gt;, &lt;code&gt;card&lt;/code&gt; or &lt;code&gt;psp&lt;/code&gt;. When not set, it defaults to checking accounts.
        /// </summary>
        /// <value>Allowed type of accounts that will be accepted in the account information access request. Can be &lt;code&gt;checking&lt;/code&gt;, &lt;code&gt;savings&lt;/code&gt;, &lt;code&gt;securities&lt;/code&gt;, &lt;code&gt;card&lt;/code&gt; or &lt;code&gt;psp&lt;/code&gt;. When not set, it defaults to checking accounts.</value>
        [DataMember(Name = "allowedAccountSubtypes", EmitDefaultValue = false)]
        public List<string> AllowedAccountSubtypes { get; set; }
    }

    /// <inheritdoc />
    [DataContract]
    public class AccountInformationAccessRequestResponse : AccountInformationAccessRequest, IIdentified<Guid>
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <summary>
        /// Details of any errors that could have occurred during initial synchronization, due to invalid authorization or technical failure. &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#sync&#39;&gt;See possible errors&lt;/a&gt;
        /// </summary>
        /// <value>Details of any errors that could have occurred during initial synchronization, due to invalid authorization or technical failure. &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#sync&#39;&gt;See possible errors&lt;/a&gt;</value>
        [DataMember(Name = "errors", EmitDefaultValue = false)]
        public Object Errors { get; set; }

        /// <summary>
        /// Current status of the account information access request. Possible values are &lt;code&gt;received&lt;/code&gt;, &lt;code&gt;started&lt;/code&gt;, &lt;code&gt;rejected&lt;/code&gt;, &lt;code&gt;succeeded&lt;/code&gt;, &lt;code&gt;partially-succeeded&lt;/code&gt;, and &lt;code&gt;failed&lt;/code&gt;. &lt;a href&#x3D;&#39;/xs2a/products#aiar-status-codes&#39;&gt;See status definitions.&lt;/a&gt;
        /// </summary>
        /// <value>Current status of the account information access request. Possible values are &lt;code&gt;received&lt;/code&gt;, &lt;code&gt;started&lt;/code&gt;, &lt;code&gt;rejected&lt;/code&gt;, &lt;code&gt;succeeded&lt;/code&gt;, &lt;code&gt;partially-succeeded&lt;/code&gt;, and &lt;code&gt;failed&lt;/code&gt;. &lt;a href&#x3D;&#39;/xs2a/products#aiar-status-codes&#39;&gt;See status definitions.&lt;/a&gt;</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// URI to redirect to from your customer frontend to conduct the authorization flow
        /// </summary>
        [DataMember(Name = "redirectUri", EmitDefaultValue = false)]
        public Uri Redirect { get; set; }
    }

    /// <summary>
    /// URI to redirect to from your customer frontend to conduct the authorization flow
    /// </summary>
    [DataContract]
    public class AccountInformationAccessRequestLinks
    {
        /// <summary>
        /// URI to redirect to from your customer frontend to conduct the authorization flow.
        /// </summary>
        [DataMember(Name = "redirect", EmitDefaultValue = false)]
        public string RedirectString { get; set; }

        /// <summary>
        /// URI to redirect to from your customer frontend to conduct the authorization flow.
        /// </summary>
        public Uri Redirect => string.IsNullOrWhiteSpace(RedirectString)
            ? null
            : new Uri(RedirectString);
    }

    /// <summary>
    /// URI to redirect to from your customer frontend to conduct the authorization flow
    /// </summary>
    [DataContract]
    public class AccountInformationAccessRequestMeta
    {
        /// <summary>
        /// Optional number of days to fetch past transactions. Default is 90
        /// </summary>
        [DataMember(Name = "requestedPastTransactionDays", EmitDefaultValue = false)]
        public int? RequestedPastTransactionDays { get; set; }
    }
}
