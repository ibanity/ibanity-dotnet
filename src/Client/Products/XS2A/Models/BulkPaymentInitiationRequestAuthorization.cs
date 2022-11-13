using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// Details about the associated bulk payment initiation request
    /// </summary>
    [DataContract]
    public class BulkPaymentInitiationRequestAuthorizationRelationships
    {
        /// <summary>
        /// Details about the associated bulk payment initiation request
        /// </summary>
        [DataMember(Name = "bulkPaymentInitiationRequest", EmitDefaultValue = false)]
        public JsonApi.Relationship<PaymentInitiationRequestRelationship> BulkPaymentInitiationRequest { get; set; }
    }
}
