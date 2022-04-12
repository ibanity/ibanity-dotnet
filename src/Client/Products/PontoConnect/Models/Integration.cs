using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    /// <summary>
    /// This endpoint provides an alternative method to revoke the integration (in addition to the revoke refresh token endpoint). This endpoint remains accessible with a client access token, even if your refresh token is lost or expired.
    /// </summary>
    [DataContract]
    public class Integration : Identified<Guid>
    {
        /// <summary>
        /// Corresponding organization ID
        /// </summary>
        public Guid OrganizationId { get; set; }
    }

    /// <summary>
    /// Details about the corresponding organization.
    /// </summary>
    [DataContract]
    public class IntegrationRelationships
    {
        /// <summary>
        /// Details about the corresponding organization
        /// </summary>
        [DataMember(Name = "organization", EmitDefaultValue = false)]
        public JsonApi.Relationship Organization { get; set; }
    }
}
