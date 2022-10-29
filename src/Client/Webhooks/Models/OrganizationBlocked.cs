using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models
{
    /// <summary>
    /// A webhook payload delivered whenever an organization has been blocked.
    /// </summary>
    public class OrganizationBlocked : JsonApi.Data, IWebhookEvent
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
    /// A webhook payload delivered whenever an organization has been blocked.
    /// </summary>
    public class NestedOrganizationBlocked : PayloadData<OrganizationBlockedAttributes, OrganizationBlockedRelationships>
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new OrganizationBlocked
            {
                Id = Id,
                Type = Type,
                OrganizationId = Guid.Parse(Relationships.Organization.Data.Id),
                CreatedAt = Attributes.CreatedAt
            };
    }

    /// <summary>
    /// Payload attributes delivered whenever an organization has been blocked.
    /// </summary>
    public class OrganizationBlockedAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// Payload relationships delivered whenever an organization has been blocked.
    /// </summary>
    public class OrganizationBlockedRelationships
    {
        /// <summary>
        /// Details about the associated organization.
        /// </summary>
        [DataMember(Name = "organization", EmitDefaultValue = false)]
        public Relationship Organization { get; set; }
    }
}
