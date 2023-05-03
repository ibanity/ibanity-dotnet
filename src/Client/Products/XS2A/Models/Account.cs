using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// <p>This is an object representing a customer account. This object will provide details about the account, including the balance and the currency.</p>
    /// <p>An account has related transactions and belongs to a financial institution.</p>
    /// <p>The account API endpoints are customer specific and therefore can only be accessed by providing the corresponding customer access token.</p>
    /// </summary>
    [DataContract]
    public class Account
    {
        /// <summary>
        /// When the authorization towards the account is expected to end. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When the authorization towards the account is expected to end. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "authorizationExpirationExpectedAt", EmitDefaultValue = false)]
        public DateTimeOffset? AuthorizationExpirationExpectedAt { get; set; }

        /// <summary>
        /// When the account was authorized for the last time. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When the account was authorized for the last time. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "authorizedAt", EmitDefaultValue = false)]
        public DateTimeOffset AuthorizedAt { get; set; }

        /// <summary>
        /// Amount of account funds that can be accessed immediately
        /// </summary>
        /// <value>Amount of account funds that can be accessed immediately</value>
        [DataMember(Name = "availableBalance", EmitDefaultValue = false)]
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// When the available balance was changed for the last time. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When the available balance was changed for the last time. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "availableBalanceChangedAt", EmitDefaultValue = false)]
        public DateTimeOffset AvailableBalanceChangedAt { get; set; }

        /// <summary>
        /// Reference date of the available balance. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>Reference date of the available balance. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "availableBalanceReferenceDate", EmitDefaultValue = false)]
        public DateTimeOffset AvailableBalanceReferenceDate { get; set; }

        /// <summary>
        /// Last time that a variation (positive or negative) was detected in our system on the available balance. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>Last time that a variation (positive or negative) was detected in our system on the available balance. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "availableBalanceVariationObservedAt", EmitDefaultValue = false)]
        public DateTimeOffset AvailableBalanceVariationObservedAt { get; set; }

        /// <summary>
        /// Currency of the account, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_4217&#39;&gt;ISO4217&lt;/a&gt; format
        /// </summary>
        /// <value>Currency of the account, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_4217&#39;&gt;ISO4217&lt;/a&gt; format</value>
        [DataMember(Name = "currency", EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        /// Total funds currently in the account
        /// </summary>
        /// <value>Total funds currently in the account</value>
        [DataMember(Name = "currentBalance", EmitDefaultValue = false)]
        public decimal CurrentBalance { get; set; }

        /// <summary>
        /// When the current balance was changed for the last time. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When the current balance was changed for the last time. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "currentBalanceChangedAt", EmitDefaultValue = false)]
        public DateTimeOffset CurrentBalanceChangedAt { get; set; }

        /// <summary>
        /// Reference date of the current balance. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>Reference date of the current balance. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "currentBalanceReferenceDate", EmitDefaultValue = false)]
        public DateTimeOffset CurrentBalanceReferenceDate { get; set; }

        /// <summary>
        /// Last time that a variation (positive or negative) was detected in our system on the current balance. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>Last time that a variation (positive or negative) was detected in our system on the current balance. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "currentBalanceVariationObservedAt", EmitDefaultValue = false)]
        public DateTimeOffset CurrentBalanceVariationObservedAt { get; set; }

        /// <summary>
        /// Description of the account
        /// </summary>
        /// <value>Description of the account</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// Name of the account holder
        /// </summary>
        /// <value>Name of the account holder</value>
        [DataMember(Name = "holderName", EmitDefaultValue = false)]
        public string HolderName { get; set; }

        /// <summary>
        /// Internal resource reference given by the financial institution
        /// </summary>
        /// <value>Internal resource reference given by the financial institution</value>
        [DataMember(Name = "internalReference", EmitDefaultValue = false)]
        public Guid InternalReference { get; set; }

        /// <summary>
        /// Name of the account product
        /// </summary>
        /// <value>Name of the account product</value>
        [DataMember(Name = "product", EmitDefaultValue = false)]
        public string Product { get; set; }

        /// <summary>
        /// Financial institution&#39;s internal reference for this account
        /// </summary>
        /// <value>Financial institution&#39;s internal reference for this account</value>
        [DataMember(Name = "reference", EmitDefaultValue = false)]
        public string Reference { get; set; }

        /// <summary>
        /// Type of financial institution reference (can be &lt;code&gt;IBAN&lt;/code&gt;, &lt;code&gt;BBAN&lt;/code&gt;, &lt;code&gt;email&lt;/code&gt;, &lt;code&gt;PAN&lt;/code&gt;, &lt;code&gt;MASKEDPAN&lt;/code&gt; or &lt;code&gt;MSISDN&lt;/code&gt;)
        /// </summary>
        /// <value>Type of financial institution reference (can be &lt;code&gt;IBAN&lt;/code&gt;, &lt;code&gt;BBAN&lt;/code&gt;, &lt;code&gt;email&lt;/code&gt;, &lt;code&gt;PAN&lt;/code&gt;, &lt;code&gt;MASKEDPAN&lt;/code&gt; or &lt;code&gt;MSISDN&lt;/code&gt;)</value>
        [DataMember(Name = "referenceType", EmitDefaultValue = false)]
        public string ReferenceType { get; set; }

        /// <summary>
        /// Type of account. Can be &lt;code&gt;checking&lt;/code&gt;, &lt;code&gt;savings&lt;/code&gt;, &lt;code&gt;securities&lt;/code&gt;, &lt;code&gt;card&lt;/code&gt; or &lt;code&gt;psp&lt;/code&gt;
        /// </summary>
        /// <value>Type of account. Can be &lt;code&gt;checking&lt;/code&gt;, &lt;code&gt;savings&lt;/code&gt;, &lt;code&gt;securities&lt;/code&gt;, &lt;code&gt;card&lt;/code&gt; or &lt;code&gt;psp&lt;/code&gt;</value>
        [DataMember(Name = "subtype", EmitDefaultValue = false)]
        public string Subtype { get; set; }
    }

    /// <inheritdoc cref="Account" />
    [DataContract]
    public class AccountResponse : Account, IIdentified<Guid>
    {
        /// <inheritdoc />
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// When this account was last synchronized.
        /// </summary>
        public DateTimeOffset SynchronizedAt { get; set; }

        /// <summary>
        /// Details of the most recently completed (with success or error) synchronization of the account
        /// </summary>
        public SynchronizationResponse LatestSynchronization { get; set; }

        /// <summary>
        /// <para>Indicates the availability of the account. The possible values are:</para>
        /// <para>available (default)</para>
        /// <para>readonly if the financial institution is undertaking maintenance and cannot be reached. Existing data is available but you cannot, for example, synchronize the account until the maintenance is complete</para>
        /// </summary>
        public string Availability { get; set; }
    }

    /// <summary>
    /// Account metadata
    /// </summary>
    [DataContract]
    public class AccountMeta
    {
        /// <summary>
        /// When this account was last synchronized.
        /// </summary>
        [DataMember(Name = "synchronizedAt", EmitDefaultValue = false)]
        public DateTimeOffset SynchronizedAt { get; set; }

        /// <summary>
        /// Details of the most recently completed (with success or error) synchronization of the account
        /// </summary>
        [DataMember(Name = "latestSynchronization", EmitDefaultValue = false)]
        public JsonApi.Data<SynchronizationResponse, object, object, object> LatestSynchronization { get; set; }

        /// <summary>
        /// <para>Indicates the availability of the account. The possible values are:</para>
        /// <para>available (default)</para>
        /// <para>readonly if the financial institution is undertaking maintenance and cannot be reached. Existing data is available but you cannot, for example, synchronize the account until the maintenance is complete</para>
        /// </summary>
        [DataMember(Name = "availability", EmitDefaultValue = false)]
        public string Availability { get; set; }
    }
}
