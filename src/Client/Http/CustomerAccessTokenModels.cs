using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Http
{
    /// <summary>
    /// <p>This is an object representing a customer access token. A customer access token has to be created for each of your customers and is required to call customer-specific endpoints like accounts or transactions.</p>
    /// <p>If you replace an active customer access token by creating a new one with the same application customer reference, it will be linked to the existing customer and the previous token will be revoked.</p>
    /// </summary>
    public class CustomerAccessTokenRequest
    {
        /// <summary>
        /// Your unique identifier for this customer
        /// </summary>
        [DataMember(Name = "applicationCustomerReference", EmitDefaultValue = false)]
        public string ApplicationCustomerReference { get; set; }
    }

    /// <summary>
    /// The created customer access token resource.
    /// </summary>
    public class CustomerAccessTokenResponse
    {
        /// <summary>
        /// Token corresponding to the customer. To be used when accessing customer-specific endpoints like account and transaction
        /// </summary>
        [DataMember(Name = "token", EmitDefaultValue = false)]
        public string Token { get; set; }
    }
}
