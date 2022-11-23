using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.PontoConnect
{
    /// <summary>
    /// A webhook payload delivered whenever a synchronization succeeds but does not change any account details or transactions.
    /// </summary>
    public class SynchronizationSucceededWithoutChange : JsonApi.Data, IWebhookEvent
    {
        /// <summary>
        /// Unique identifier of the associated account.
        /// </summary>
        [DataMember(Name = "accountId", EmitDefaultValue = false)]
        public Guid AccountId { get; set; }

        /// <summary>
        /// Unique identifier of the associated synchronization.
        /// </summary>
        [DataMember(Name = "synchronizationId", EmitDefaultValue = false)]
        public Guid SynchronizationId { get; set; }

        /// <summary>
        /// Subtype of the related synchronization.
        /// </summary>
        [DataMember(Name = "synchronizationSubtype", EmitDefaultValue = false)]
        public string SynchronizationSubtype { get; set; }

        /// <summary>
        /// Unique identifier of the associated organization.
        /// </summary>
        [DataMember(Name = "organizationId", EmitDefaultValue = false)]
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// A webhook payload delivered whenever a synchronization succeeds but does not change any account details or transactions.
    /// </summary>
    public class NestedSynchronizationSucceededWithoutChange : PayloadData<SynchronizationSucceededWithoutChangeAttributes, SynchronizationSucceededWithoutChangeRelationships>
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new SynchronizationSucceededWithoutChange
            {
                Id = Id,
                Type = Type,
                AccountId = Guid.Parse(Relationships.Account.Data.Id),
                SynchronizationId = Guid.Parse(Relationships.Synchronization.Data.Id),
                SynchronizationSubtype = Attributes.SynchronizationSubtype,
                OrganizationId = Guid.Parse(Relationships.Organization.Data.Id),
                CreatedAt = Attributes.CreatedAt
            };
    }

    /// <summary>
    /// Payload attributes delivered whenever a synchronization succeeds but does not change any account details or transactions.
    /// </summary>
    public class SynchronizationSucceededWithoutChangeAttributes
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
    /// Payload relationships delivered whenever a synchronization succeeds but does not change any account details or transactions.
    /// </summary>
    public class SynchronizationSucceededWithoutChangeRelationships
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

        /// <summary>
        /// Details about the associated organization.
        /// </summary>
        [DataMember(Name = "organization", EmitDefaultValue = false)]
        public Relationship Organization { get; set; }
    }
}
