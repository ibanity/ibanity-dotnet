using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models
{
    /// <summary>
    /// A webhook payload.
    /// </summary>
    public class AccountReauthorized : JsonApi.Data, IWebhookEvent
    {
        /// <summary>
        /// Unique identifier of the associated account.
        /// </summary>
        [DataMember(Name = "accountId", EmitDefaultValue = false)]
        public Guid AccountId { get; set; }

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
    /// A webhook payload.
    /// </summary>
    public class NestedAccountReauthorized : PayloadData<AccountReauthorizedAttributes, AccountReauthorizedRelationships>
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new AccountReauthorized
            {
                Id = Id,
                Type = Type,
                AccountId = Guid.Parse(Relationships.Account.Data.Id),
                OrganizationId = Guid.Parse(Relationships.Organization.Data.Id),
                CreatedAt = Attributes.CreatedAt
            };
    }

    /// <summary>
    /// Payload attributes.
    /// </summary>
    public class AccountReauthorizedAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// Payload relationships.
    /// </summary>
    public class AccountReauthorizedRelationships
    {
        /// <summary>
        /// Details about the associated account.
        /// </summary>
        [DataMember(Name = "account", EmitDefaultValue = false)]
        public Relationship Account { get; set; }

        /// <summary>
        /// Details about the associated organization.
        /// </summary>
        [DataMember(Name = "organization", EmitDefaultValue = false)]
        public Relationship Organization { get; set; }
    }
}
