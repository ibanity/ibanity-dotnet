using System;

namespace Ibanity.Apis.Client.Webhooks.Models
{
    /// <summary>
    /// A webhook payload delivered whenever a synchronization succeeds but does not change any account details or transactions.
    /// </summary>
    public class SynchronizationSucceededWithoutChange : Payload<SynchronizationSucceededWithoutChangeAttributes, SynchronizationSucceededWithoutChangeRelationships> { }

    /// <summary>
    /// Payload attributes delivered whenever a synchronization succeeds but does not change any account details or transactions.
    /// </summary>
    public class SynchronizationSucceededWithoutChangeAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Subtype of the related synchronization.
        /// </summary>
        public string SynchronizationSubtype { get; set; }
    }

    /// <summary>
    /// Payload relationships delivered whenever a synchronization succeeds but does not change any account details or transactions.
    /// </summary>
    public class SynchronizationSucceededWithoutChangeRelationships
    {
        /// <summary>
        /// Details about the associated account.
        /// </summary>
        public Relationship Account { get; set; }

        /// <summary>
        /// Details about the associated synchronization.
        /// </summary>
        public Relationship Synchronization { get; set; }
    }
}
