using System;

namespace Ibanity.Apis.Client.Http
{
    /// <summary>
    /// Customer access token, generated from a client ID and client secret.
    /// </summary>
    public class CustomerAccessToken : BaseToken
    {
        /// <summary>
        /// Unique identifier for the customer access token.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Your unique identifier for this customer.
        /// </summary>
        public string ApplicationCustomerReference { get; set; }
    }
}
