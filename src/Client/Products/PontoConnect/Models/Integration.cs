using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    [DataContract]
    public class Integration : Identified<Guid>
    {
        public Guid OrganizationId { get; set; }
    }

    [DataContract]
    public class IntegrationRelationships
    {
        [DataMember(Name = "organization", EmitDefaultValue = false)]
        public JsonApi.Relationship Organization { get; set; }
    }
}
