using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models.PontoConnect
{
    /// <summary>
    /// A webhook payload delivered whenever a payment request has been closed.
    /// </summary>
    public class PaymentRequestClosed : JsonApi.Data, IWebhookEvent
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
        /// Unique identifier of the associated payment request.
        /// </summary>
        [DataMember(Name = "paymentRequestId", EmitDefaultValue = false)]
        public Guid PaymentRequestId { get; set; }

        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// A webhook payload delivered whenever a payment has been closed.
    /// </summary>
    public class NestedPaymentRequestClosed : PayloadData<PaymentRequestClosedAttributes, PaymentRequestClosedRelationships>
    {
        /// <inheritdoc />
        public override IWebhookEvent Flatten() =>
            new PaymentRequestClosed
            {
                Id = Id,
                Type = Type,
                AccountId = Guid.Parse(Relationships.Account.Data.Id),
                OrganizationId = Guid.Parse(Relationships.Organization.Data.Id),
                PaymentRequestId = Guid.Parse(Relationships.PaymentRequest.Data.Id),
                CreatedAt = Attributes.CreatedAt
            };
    }

    /// <summary>
    /// Payload attributes delivered whenever an payment request has been closed.
    /// </summary>
    public class PaymentRequestClosedAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// Payload relationships delivered whenever a payment request has been closed.
    /// </summary>
    public class PaymentRequestClosedRelationships
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

        /// <summary>
        /// Details about the associated payment request.
        /// </summary>
        [DataMember(Name = "paymentRequest", EmitDefaultValue = false)]
        public Relationship PaymentRequest { get; set; }
    }
}
