using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.CodaboxConnect.Models
{
    /// <summary>
    /// This resource allows a Software Partner to create a new Supplier.
    /// </summary>
    [DataContract]
    public class AccountingOfficeConsent
    {
        /// <summary>
        /// The company number of the accounting office.
        /// </summary>
        /// <value>The company number of the accounting office.</value>
        [DataMember(Name = "accountingOfficeCompanyNumber", EmitDefaultValue = true)]
        public string AccountingOfficeCompanyNumber { get; set; }
    }

    /// <inheritdoc />
    [DataContract]
    public class NewAccountingOfficeConsent : AccountingOfficeConsent
    {
        /// <summary>
        /// Indicates the URI that the accounting office representative will be redirected to once the consent has been confirmed (or rejected).
        /// </summary>
        /// <value>Indicates the URI that the accounting office representative will be redirected to once the consent has been confirmed (or rejected).</value>
        [DataMember(Name = "redirectUri", EmitDefaultValue = true)]
        public string RedirectUri { get; set; }
    }

    /// <inheritdoc />
    [DataContract]
    public class AccountingOfficeConsentResponse : AccountingOfficeConsent, IIdentified<Guid>
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <summary>
        /// The id of the accounting office.
        /// </summary>
        /// <value>The id of the accounting office.</value>
        [DataMember(Name = "accountingOfficeId", EmitDefaultValue = false)]
        public Guid AccountingOfficeId { get; set; }

        /// <summary>
        /// Indicates the URI that the accounting office representative will need to go to in order to confirm (or reject) the consent.
        /// </summary>
        /// <value>Indicates the URI that the accounting office representative will need to go to in order to confirm (or reject) the consent.</value>
        [DataMember(Name = "confirmationUri", EmitDefaultValue = false)]
        public string ConfirmationUri { get; set; }

        /// <summary>
        /// When this consent was requested. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec.
        /// </summary>
        /// <value>When this consent was requested. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec.</value>
        [DataMember(Name = "requestedAt", EmitDefaultValue = false)]
        public DateTimeOffset RequestedAt { get; set; }

        /// <summary>
        /// The status of the confirmation process of this consent, for more information see &lt;a href&#x3D;&#39;https://documentation.development.ibanity.net/codabox-connect/products#consent&#39;&gt;Consents&lt;/a&gt;.&lt;ul&gt;&lt;li&gt;&lt;code&gt;unconfirmed&lt;/code&gt; The consent was successfully created at CodaBox.&lt;/li&gt;&lt;li&gt;&lt;code&gt;confirmed&lt;/code&gt; The consent has been confirmed by the accounting office representative.&lt;/li&gt;&lt;li&gt;&lt;code&gt;denied&lt;/code&gt; The consent has been denied by the accounting office representative.&lt;/li&gt;&lt;/ul&gt;
        /// </summary>
        /// <value>The status of the confirmation process of this consent, for more information see &lt;a href&#x3D;&#39;https://documentation.development.ibanity.net/codabox-connect/products#consent&#39;&gt;Consents&lt;/a&gt;.&lt;ul&gt;&lt;li&gt;&lt;code&gt;unconfirmed&lt;/code&gt; The consent was successfully created at CodaBox.&lt;/li&gt;&lt;li&gt;&lt;code&gt;confirmed&lt;/code&gt; The consent has been confirmed by the accounting office representative.&lt;/li&gt;&lt;li&gt;&lt;code&gt;denied&lt;/code&gt; The consent has been denied by the accounting office representative.&lt;/li&gt;&lt;/ul&gt;</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }
    }
}
