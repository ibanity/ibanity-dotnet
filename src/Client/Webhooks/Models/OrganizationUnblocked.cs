using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models
{
    /// <summary>
    /// A webhook payload delivered whenever an organization has been unblocked.
    /// </summary>
    public class OrganizationUnblocked : JsonApi.Data, IWebhookEvent
    {
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
    /// A webhook payload delivered whenever an organization has been unblocked.
    /// </summary>
    public class NestedOrganizationUnblocked : PayloadData<OrganizationUnblockedAttributes, OrganizationUnblockedRelationships>
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new OrganizationUnblocked
            {
                Id = Id,
                Type = Type,
                OrganizationId = Guid.Parse(Relationships.Organization.Data.Id),
                CreatedAt = Attributes.CreatedAt
            };
    }

    /// <summary>
    /// Payload attributes delivered whenever an organization has been unblocked.
    /// </summary>
    public class OrganizationUnblockedAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// Payload relationships delivered whenever an organization has been unblocked.
    /// </summary>
    public class OrganizationUnblockedRelationships
    {
        /// <summary>
        /// Details about the associated organization.
        /// </summary>
        [DataMember(Name = "organization", EmitDefaultValue = false)]
        public Relationship Organization { get; set; }
    }
}
