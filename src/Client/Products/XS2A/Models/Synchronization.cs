using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// <p>This is an object representing a resource synchronization. This object will give you the details of the synchronization, including its resource, type, and status.</p>
    /// <p>The synchronization API endpoints are customer specific and therefore can only be accessed by providing the corresponding customer access token.</p>
    /// </summary>
    [DataContract]
    public class Synchronization
    {
        /// <summary>
        /// Type of the resource to be synchronized. Currently must be &lt;code&gt;account&lt;/code&gt;
        /// </summary>
        /// <value>Type of the resource to be synchronized. Currently must be &lt;code&gt;account&lt;/code&gt;</value>
        [DataMember(Name = "resourceType", EmitDefaultValue = false)]
        public string ResourceType { get; set; }

        /// <summary>
        /// Identifier of the resource to be synchronized
        /// </summary>
        /// <value>Identifier of the resource to be synchronized</value>
        [DataMember(Name = "resourceId", EmitDefaultValue = false)]
        public Guid ResourceId { get; set; }

        /// <summary>
        /// What is being synchronized. Account information such as balance is updated using &lt;code&gt;accountDetails&lt;/code&gt;, while &lt;code&gt;accountTransactions&lt;/code&gt; is used to synchronize the transactions.
        /// </summary>
        /// <value>What is being synchronized. Account information such as balance is updated using &lt;code&gt;accountDetails&lt;/code&gt;, while &lt;code&gt;accountTransactions&lt;/code&gt; is used to synchronize the transactions.</value>
        [DataMember(Name = "subtype", EmitDefaultValue = false)]
        public string Subtype { get; set; }

        /// <summary>
        /// When set to &lt;code&gt;true&lt;/code&gt;, the TPP indicates that the customer is actively using the product during the call. In this case, the &lt;code&gt;customerIpAddress&lt;/code&gt; is also required. When set to &lt;code&gt;false&lt;/code&gt;, the TPP indicates that the call is made without the customer being present (scheduled / automated call).
        /// </summary>
        /// <value>When set to &lt;code&gt;true&lt;/code&gt;, the TPP indicates that the customer is actively using the product during the call. In this case, the &lt;code&gt;customerIpAddress&lt;/code&gt; is also required. When set to &lt;code&gt;false&lt;/code&gt;, the TPP indicates that the call is made without the customer being present (scheduled / automated call).</value>
        [DataMember(Name = "customerOnline", EmitDefaultValue = true)]
        public bool CustomerOnline { get; set; }
    }

    /// <inheritdoc />
    [DataContract]
    public class SynchronizationRequest : Synchronization
    {
        /// <summary>
        /// If the customer is online (&lt;code&gt;customerOnline&lt;/code&gt; is set to &lt;code&gt;true&lt;/code&gt;), this must contain the IP address of the customer
        /// </summary>
        /// <value>If the customer is online (&lt;code&gt;customerOnline&lt;/code&gt; is set to &lt;code&gt;true&lt;/code&gt;), this must contain the IP address of the customer</value>
        [DataMember(Name = "customerIpAddress", EmitDefaultValue = true)]
        public string CustomerIpAddress { get; set; }
    }

    /// <inheritdoc />
    [DataContract]
    public class SynchronizationResponse : Synchronization, IIdentified<Guid>
    {
        /// <inheritdoc />
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// When this synchronization was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this synchronization was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Details of any errors that have occurred during synchronization, due to invalid authorization or technical failure. &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#sync&#39;&gt;See possible errors&lt;/a&gt;
        /// </summary>
        /// <value>Details of any errors that have occurred during synchronization, due to invalid authorization or technical failure. &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#sync&#39;&gt;See possible errors&lt;/a&gt;</value>
        [DataMember(Name = "errors", EmitDefaultValue = false)]
        public List<Object> Errors { get; set; }

        /// <summary>
        /// Current status of the synchronization, which changes from &lt;code&gt;pending&lt;/code&gt; to &lt;code&gt;running&lt;/code&gt; to &lt;code&gt;success&lt;/code&gt; or &lt;code&gt;error&lt;/code&gt;
        /// </summary>
        /// <value>Current status of the synchronization, which changes from &lt;code&gt;pending&lt;/code&gt; to &lt;code&gt;running&lt;/code&gt; to &lt;code&gt;success&lt;/code&gt; or &lt;code&gt;error&lt;/code&gt;</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// When this synchronization was last synchronized successfully. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this synchronization was last synchronized successfully. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "updatedAt", EmitDefaultValue = false)]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
