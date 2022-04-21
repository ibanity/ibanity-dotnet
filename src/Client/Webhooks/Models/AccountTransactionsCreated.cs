using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models
{
    /// <summary>
    /// A webhook payload delivered whenever a synchronization completes which has created transactions for an account.
    /// </summary>
    public class AccountTransactionsCreated : PayloadData<AccountTransactionsCreatedAttributes, AccountTransactionsCreatedRelationships> { }

    /// <summary>
    /// Payload attributes delivered whenever a synchronization completes which has created transactions for an account.
    /// </summary>
    public class AccountTransactionsCreatedAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Number of transactions created by the synchronization.
        /// </summary>
        [DataMember(Name = "count", EmitDefaultValue = false)]
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
        [DataMember(Name = "account", EmitDefaultValue = false)]
        public Relationship Account { get; set; }

        /// <summary>
        /// Details about the associated synchronization.
        /// </summary>
        [DataMember(Name = "synchronization", EmitDefaultValue = false)]
        public Relationship Synchronization { get; set; }
    }
}
