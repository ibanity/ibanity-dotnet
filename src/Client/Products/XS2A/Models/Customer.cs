using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// <p>This is an object representing a customer. A customer resource is created with the creation of a related customer access token.</p>
    /// <p>In the case that the contractual relationship between you and your customer is terminated, you should probably use the Delete Customer endpoint to erase ALL customer personal data.</p>
    /// <p>In the case that your customer wants to revoke your access to some accounts, you should use the Delete Account endpoint instead.</p>
    /// </summary>
    [DataContract]
    public class Customer : Identified<Guid> { }
}
