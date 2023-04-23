using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    /// <summary>
    /// <p>This is an object representing the link between a user's account and an integration.</p>
    /// <p>All accounts linked to your Ponto Connect application are returned by this endpoint.</p>
    /// </summary>
    [DataContract]
    public class IntegrationAccount : Identified<Guid>
    {
        /// <summary>
        /// &lt;p&gt;When the account was linked to the integration
        /// </summary>
        /// <value>&lt;p&gt;When the account was linked to the integration</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// &lt;p&gt;When the account was last accessed through the integration.
        /// </summary>
        /// <value>&lt;p&gt;When the account was last accessed through the integration.</value>
        [DataMember(Name = "lastAccessedAt", EmitDefaultValue = false)]
        public DateTimeOffset? LastAccessedAt { get; set; }

        /// <summary>
        /// Corresponding account ID
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Corresponding financial institution ID
        /// </summary>
        public Guid FinancialInstitutionId { get; set; }

        /// <summary>
        /// Corresponding organization ID
        /// </summary>
        public Guid OrganizationId { get; set; }
    }

    /// <summary>
    /// Details about the corresponding account, financial institution and organization.
    /// </summary>
    [DataContract]
    public class IntegrationAccountRelationships
    {
        /// <summary>
        /// Details about the corresponding account
        /// </summary>
        [DataMember(Name = "account", EmitDefaultValue = false)]
        public JsonApi.Relationship Account { get; set; }

        /// <summary>
        /// Details about the corresponding financial institution
        /// </summary>
        [DataMember(Name = "financialInstitution", EmitDefaultValue = false)]
        public JsonApi.Relationship FinancialInstitution { get; set; }

        /// <summary>
        /// Details about the corresponding organization
        /// </summary>
        [DataMember(Name = "organization", EmitDefaultValue = false)]
        public JsonApi.Relationship Organization { get; set; }
    }
}
