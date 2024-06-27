using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.eInvoicing.Models
{
    /// <summary>
    /// <p>This is an object representing the invoice that can be sent by a supplier. This document is always an UBL in a format supported by CodaBox.</p>
    /// <p>The maximum file size is 100MB.</p>
    /// </summary>
    [DataContract]
    public class PeppolRegistration : Identified<Guid>
    {
        /// <summary>
        /// Peppol registration error reason. Can be &lt;code&gt;already-registered&lt;/code&gt;, &lt;code&gt;blocked&lt;/code&gt; or &lt;code&gt;general-error&lt;/code&gt;.
        /// </summary>
        /// <value>Peppol registration error reason. Can be &lt;code&gt;already-registered&lt;/code&gt;, &lt;code&gt;blocked&lt;/code&gt; or &lt;code&gt;general-error&lt;/code&gt;.</value>
        [DataMember(Name = "reason", EmitDefaultValue = false)]
        public string Reason { get; set; }

        /// <summary>
        /// Peppol registration status. Can be &lt;code&gt;registration-in-progress&lt;/code&gt;, &lt;code&gt;registered&lt;/code&gt;, &lt;code&gt;registration-failed&lt;/code&gt; or &lt;code&gt;deregistered&lt;/code&gt;.
        /// </summary>
        /// <value>Peppol registration status. Can be &lt;code&gt;registration-in-progress&lt;/code&gt;, &lt;code&gt;registered&lt;/code&gt;, &lt;code&gt;registration-failed&lt;/code&gt; or &lt;code&gt;deregistered&lt;/code&gt;.</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Can be &lt;code&gt;vat-number&lt;/code&gt;, &lt;code&gt;enterprise-number&lt;/code&gt;, &lt;code&gt;kvk-number&lt;/code&gt; or &lt;code&gt;national-identification-number&lt;/code&gt;.
        /// </summary>
        /// <value>Can be &lt;code&gt;vat-number&lt;/code&gt;, &lt;code&gt;enterprise-number&lt;/code&gt;, &lt;code&gt;kvk-number&lt;/code&gt; or &lt;code&gt;national-identification-number&lt;/code&gt;.</value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// Identifier value.
        /// </summary>
        /// <value>Identifier value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }

        /// <summary>
        /// When this peppol registration was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this peppol registration was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Only filled in when &lt;code&gt;status&lt;/code&gt; is &lt;code&gt;registration-failed&lt;/code&gt; with &lt;code&gt;reason&lt;/code&gt; &lt;code&gt;already-registered&lt;/code&gt;.
        /// </summary>
        /// <value>Only filled in when &lt;code&gt;status&lt;/code&gt; is &lt;code&gt;registration-failed&lt;/code&gt; with &lt;code&gt;reason&lt;/code&gt; &lt;code&gt;already-registered&lt;/code&gt;.</value>
        [DataMember(Name = "accessPoints", EmitDefaultValue = false)]
        public List<string> AccessPoints { get; set; }

        /// <summary>
        /// When was the last time this Peppol registration failed. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec.
        /// </summary>
        /// <value>When was the last time this Peppol registration failed. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec.</value>
        [DataMember(Name = "failedSince", EmitDefaultValue = false)]
        public DateTimeOffset? FailedSince { get; set; }

        /// <summary>
        /// When was this Peppol registration modified for the last time. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec.
        /// </summary>
        /// <value>When was this Peppol registration modified for the last time. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec.</value>
        [DataMember(Name = "modifiedAt", EmitDefaultValue = false)]
        public DateTimeOffset? ModifiedAt { get; set; }
    }
}
