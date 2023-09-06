using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.CodaboxConnect.Models
{
    /// <summary>
    /// This resource allows an Accounting Software to retrieve a purchase invoice or credit note document for a client of an accounting office.
    /// </summary>
    [DataContract]
    public class PurchaseInvoice : Document<Guid>
    {
        /// <summary>
        /// Format of the response you expect from the call. If present, it must be one of the following: &lt;ul&gt;&lt;li&gt;&lt;code&gt;application/vnd.api+json&lt;/code&gt; a purchase invoice resource.&lt;/li&gt;&lt;li&gt;&lt;code&gt;application/pdf&lt;/code&gt; a purchase invoice in its original format.&lt;/li&gt;&lt;li&gt;&lt;code&gt;application/xml&lt;/code&gt; a purchase invoice in a structured format for easier booking.&lt;/li&gt;&lt;/ul&gt;Defaults to &lt;code&gt;application/vnd.api+json&lt;/code&gt;.
        /// </summary>
        /// <value>Format of the response you expect from the call. If present, it must be one of the following: &lt;ul&gt;&lt;li&gt;&lt;code&gt;application/vnd.api+json&lt;/code&gt; a purchase invoice resource.&lt;/li&gt;&lt;li&gt;&lt;code&gt;application/pdf&lt;/code&gt; a purchase invoice in its original format.&lt;/li&gt;&lt;li&gt;&lt;code&gt;application/xml&lt;/code&gt; a purchase invoice in a structured format for easier booking.&lt;/li&gt;&lt;/ul&gt;Defaults to &lt;code&gt;application/vnd.api+json&lt;/code&gt;.</value>
        [DataMember(Name = "availableContentTypes", EmitDefaultValue = false)]
        public string[] AvailableContentTypes { get; set; }

        /// <summary>
        /// When this purchase invoice was received by CodaBox. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec.
        /// </summary>
        /// <value>When this purchase invoice was received by CodaBox. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec.</value>
        [DataMember(Name = "receivedAt", EmitDefaultValue = false)]
        public DateTimeOffset ReceivedAt { get; set; }
    }
}
