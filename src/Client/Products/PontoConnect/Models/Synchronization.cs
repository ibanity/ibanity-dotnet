using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    [DataContract]
    public class Synchronization : Identified<Guid>
    {
        /// <summary>
        /// When this synchronization was last synchronized successfully. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this synchronization was last synchronized successfully. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "updatedAt", EmitDefaultValue = false)]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// What is being synchronized. Account information such as balance is updated using &lt;code&gt;accountDetails&lt;/code&gt;, while &lt;code&gt;accountTransactions&lt;/code&gt; is used to synchronize the transactions.
        /// </summary>
        /// <value>What is being synchronized. Account information such as balance is updated using &lt;code&gt;accountDetails&lt;/code&gt;, while &lt;code&gt;accountTransactions&lt;/code&gt; is used to synchronize the transactions.</value>
        [DataMember(Name = "subtype", EmitDefaultValue = false)]
        public string Subtype { get; set; }

        /// <summary>
        /// Current status of the synchronization, which changes from &lt;code&gt;pending&lt;/code&gt; to &lt;code&gt;running&lt;/code&gt; to &lt;code&gt;success&lt;/code&gt; or &lt;code&gt;error&lt;/code&gt;
        /// </summary>
        /// <value>Current status of the synchronization, which changes from &lt;code&gt;pending&lt;/code&gt; to &lt;code&gt;running&lt;/code&gt; to &lt;code&gt;success&lt;/code&gt; or &lt;code&gt;error&lt;/code&gt;</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

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
        /// Details of any errors that have occurred during synchronization, due to invalid authorization or technical failure. &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#sync&#39;&gt;See possible errors&lt;/a&gt;
        /// </summary>
        /// <value>Details of any errors that have occurred during synchronization, due to invalid authorization or technical failure. &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#sync&#39;&gt;See possible errors&lt;/a&gt;</value>
        [DataMember(Name = "errors", EmitDefaultValue = false)]
        public List<ErrorItem> Errors { get; set; }

        /// <summary>
        /// When this synchronization was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this synchronization was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }
}
