using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// Details about the associated payment initiation request
    /// </summary>
    [DataContract]
    public class PaymentInitiationRequestAuthorizationRelationships
    {
        /// <summary>
        /// Details about the associated payment initiation request
        /// </summary>
        [DataMember(Name = "paymentInitiationRequest", EmitDefaultValue = false)]
        public JsonApi.Relationship<PaymentInitiationRequestRelationship> PaymentInitiationRequest { get; set; }
    }
}
