using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.PontoConnect
{
    /// <summary>
    /// A webhook payload delivered whenever a synchronization completes which has created transactions for an account.
    /// </summary>
    public class AccountTransactionsCreated : JsonApi.Data, IWebhookEvent
    {
        /// <summary>
        /// Unique identifier of the associated account.
        /// </summary>
        [DataMember(Name = "accountId", EmitDefaultValue = false)]
        public Guid AccountId { get; set; }

        /// <summary>
        /// Number of transactions created by the synchronization.
        /// </summary>
        [DataMember(Name = "count", EmitDefaultValue = false)]
        public int Count { get; set; }

        /// <summary>
        /// Unique identifier of the associated synchronization.
        /// </summary>
        [DataMember(Name = "synchronizationId", EmitDefaultValue = false)]
        public Guid SynchronizationId { get; set; }

        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// A webhook payload delivered whenever a synchronization completes which has created transactions for an account.
    /// </summary>
    public class NestedAccountTransactionsCreated : PayloadData<AccountTransactionsCreatedAttributes, AccountTransactionsCreatedRelationships>
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new AccountTransactionsCreated
            {
                Id = Id,
                Type = Type,
                AccountId = Guid.Parse(Relationships.Account.Data.Id),
                Count = Attributes.Count,
                SynchronizationId = Guid.Parse(Relationships.Synchronization.Data.Id),
                CreatedAt = Attributes.CreatedAt
            };
    }

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
