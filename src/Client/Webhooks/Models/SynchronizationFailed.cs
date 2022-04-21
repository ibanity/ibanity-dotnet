using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models
{
    /// <summary>
    /// A webhook payload delivered whenever a synchronization fails.
    /// </summary>
    public class SynchronizationFailed : PayloadData<SynchronizationFailedAttributes, SynchronizationFailedRelationships> { }

    /// <summary>
    /// Payload attributes delivered whenever a synchronization fails.
    /// </summary>
    public class SynchronizationFailedAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Subtype of the related synchronization.
        /// </summary>
        [DataMember(Name = "synchronizationSubtype", EmitDefaultValue = false)]
        public string SynchronizationSubtype { get; set; }
    }

    /// <summary>
    /// Payload relationships delivered whenever a synchronization fails.
    /// </summary>
    public class SynchronizationFailedRelationships
    {
        /// <summary>
        /// Details about the associated account.
        /// </summary>
        [DataMember(Name = "account", EmitDefaultValue = false)]
        public Relationship Account { get; set; }

        /// <summary>
        /// Details about the associated synchronization.
        /// </summary>
        [DataMember(Name = "synchronization", EmitDefaultValue = false)]
        public Relationship Synchronization { get; set; }
    }
}
