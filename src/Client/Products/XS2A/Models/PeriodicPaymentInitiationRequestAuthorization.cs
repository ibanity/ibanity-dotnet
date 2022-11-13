using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// Details about the associated periodic payment initiation request
    /// </summary>
    [DataContract]
    public class PeriodicPaymentInitiationRequestAuthorizationRelationships
    {
        /// <summary>
        /// Details about the associated periodic payment initiation request
        /// </summary>
        [DataMember(Name = "periodicPaymentInitiationRequest", EmitDefaultValue = false)]
        public JsonApi.Relationship<PaymentInitiationRequestRelationship> PeriodicPaymentInitiationRequest { get; set; }
    }
}
