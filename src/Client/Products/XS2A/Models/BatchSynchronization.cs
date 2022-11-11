using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// This is an object representing a resource batch-synchronization. This object will give you the details of the batch-synchronization.
    /// </summary>
    [DataContract]
    public class BatchSynchronization
    {
        /// <summary>
        /// Type of the resource to be synchronized. Currently must be &lt;code&gt;account&lt;/code&gt;
        /// </summary>
        /// <value>Type of the resource to be synchronized. Currently must be &lt;code&gt;account&lt;/code&gt;</value>
        [DataMember(Name = "resourceType", IsRequired = true, EmitDefaultValue = true)]
        public string ResourceType { get; set; }

        /// <summary>
        /// Execution limit for execution of the batch-synchronization. Should the synchronization start after the given date, then it would be cancelled.
        /// </summary>
        /// <value>Execution limit for execution of the batch-synchronization. Should the synchronization start after the given date, then it would be cancelled.</value>
        [DataMember(Name = "cancelAfter", EmitDefaultValue = false)]
        public DateTimeOffset? CancelAfter { get; set; }

        /// <summary>
        /// Datetime to prevent the synchronization of the resource, if it was already synchronized after the given date.
        /// </summary>
        /// <value>Datetime to prevent the synchronization of the resource, if it was already synchronized after the given date.</value>
        [DataMember(Name = "unlessSynchronizedAfter", EmitDefaultValue = false)]
        public DateTimeOffset? UnlessSynchronizedAfter { get; set; }

        /// <summary>
        /// Array of what is being synchronized. Account information such as balance is updated using &lt;code&gt;accountDetails&lt;/code&gt;, while &lt;code&gt;accountTransactions&lt;/code&gt; is used to synchronize the transactions.
        /// </summary>
        /// <value>Array of what is being synchronized. Account information such as balance is updated using &lt;code&gt;accountDetails&lt;/code&gt;, while &lt;code&gt;accountTransactions&lt;/code&gt; is used to synchronize the transactions.</value>
        [DataMember(Name = "subtypes", IsRequired = true, EmitDefaultValue = true)]
        public List<string> Subtypes { get; set; }
    }

    /// <summary>
    /// This is an object representing a resource batch-synchronization. This object will give you the details of the batch-synchronization.
    /// </summary>
    [DataContract]
    public class BatchSynchronizationResponse : Identified<Guid> { }
}
