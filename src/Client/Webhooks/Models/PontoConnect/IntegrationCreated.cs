using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.PontoConnect
{
    /// <summary>
    /// A webhook payload delivered whenever an integration has been created.
    /// </summary>
    public class IntegrationCreated : JsonApi.Data, IWebhookEvent
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
    /// A webhook payload delivered whenever an integration has been created.
    /// </summary>
    public class NestedIntegrationCreated : PayloadData<IntegrationCreatedAttributes, IntegrationCreatedRelationships>
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new IntegrationCreated
            {
                Id = Id,
                Type = Type,
                OrganizationId = Guid.Parse(Relationships.Organization.Data.Id),
                CreatedAt = Attributes.CreatedAt
            };
    }

    /// <summary>
    /// Payload attributes delivered whenever an integration has been created.
    /// </summary>
    public class IntegrationCreatedAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// Payload relationships delivered whenever an integration has been created.
    /// </summary>
    public class IntegrationCreatedRelationships
    {
        /// <summary>
        /// Details about the associated organization.
        /// </summary>
        [DataMember(Name = "organization", EmitDefaultValue = false)]
        public Relationship Organization { get; set; }
    }
}
