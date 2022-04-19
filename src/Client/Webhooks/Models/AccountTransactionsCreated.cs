using System;

namespace Ibanity.Apis.Client.Webhooks.Models
{
    /// <summary>
    /// A webhook payload delivered whenever a synchronization completes which has created transactions for an account.
    /// </summary>
    public class AccountTransactionsCreated : Payload<AccountTransactionsCreatedAttributes, AccountTransactionsCreatedRelationships> { }

    /// <summary>
    /// Payload attributes delivered whenever a synchronization completes which has created transactions for an account.
    /// </summary>
    public class AccountTransactionsCreatedAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Number of transactions created by the synchronization.
        /// </summary>
        public int Count { get; set; }
    }

    /// <summary>
    /// Payload relationships delivered whenever a synchronization completes which has created transactions for an account.
    /// </summary>
    public class AccountTransactionsCreatedRelationships
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
