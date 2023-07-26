using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.XS2A
{
    /// <summary>
    /// A webhook payload delivered whenever a synchronization completes which has updated transactions for an account.
    /// </summary>
    public class AccountTransactionsDeleted : JsonApi.Data, IWebhookEvent
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
        /// The selected deletion date limit.
        /// </summary>
        [DataMember(Name = "deletedBefore", EmitDefaultValue = false)]
        public DateTimeOffset DeletedBefore { get; set; }

        /// <summary>
        /// Unique identifier of the associated transactionDeleteRequest.
        /// </summary>
        [DataMember(Name = "transactionDeleteRequestId", EmitDefaultValue = false)]
        public Guid? TransactionDeleteRequestId { get; set; }

        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// A webhook payload delivered whenever a synchronization completes which has updated transactions for an account.
    /// </summary>
    public class NestedAccountTransactionsDeleted : PayloadData<AccountTransactionsDeletedAttributes, AccountTransactionsDeletedRelationships>
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new AccountTransactionsDeleted
            {
                Id = Id,
                Type = Type,
                AccountId = Guid.Parse(Relationships.Account.Data.Id),
                DeletedBefore = Attributes.DeletedBefore,
                Count = Attributes.Count,
                TransactionDeleteRequestId = Relationships.TransactionDeleteRequest == null ? null : (Guid?)Guid.Parse(Relationships.TransactionDeleteRequest.Data.Id),
                CreatedAt = Attributes.CreatedAt
            };
    }

    /// <summary>
    /// Payload attributes delivered whenever a synchronization completes which has updated transactions for an account.
    /// </summary>
    public class AccountTransactionsDeletedAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The selected deletion date limit.
        /// </summary>
        [DataMember(Name = "deletedBefore", EmitDefaultValue = false)]
        public DateTimeOffset DeletedBefore { get; set; }

        /// <summary>
        /// Number of transactions created by the synchronization.
        /// </summary>
        [DataMember(Name = "count", EmitDefaultValue = false)]
        public int Count { get; set; }
    }

    /// <summary>
    /// Payload relationships delivered whenever a transaction delete request completes for an account.
    /// </summary>
    public class AccountTransactionsDeletedRelationships
    {
        /// <summary>
        /// Details about the associated account.
        /// </summary>
        [DataMember(Name = "account", EmitDefaultValue = false)]
        public Relationship Account { get; set; }

        /// <summary>
        /// Details about the associated transactionDeleteRequest.
        /// </summary>
        [DataMember(Name = "transactionDeleteRequest", EmitDefaultValue = false)]
        public Relationship TransactionDeleteRequest { get; set; }
    }
}
