using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// <p>This is an object representing a resource transaction delete request.</p>
    /// <p>The transaction delete request API endpoints can be issued at the level of the application (all customers), a specific customer (all hiis/her accounts), or at account level. The customer access token is therefore only necessary for customer or account.</p>
    /// </summary>
    [DataContract]
    public class TransactionDeleteRequest
    {
        /// <summary>
        /// The requested date before which the transactions should be deleted.
        /// </summary>
        /// <value>The requested date before which the transactions should be deleted.</value>
        [DataMember(Name = "beforeDate", EmitDefaultValue = false)]
        public string BeforeDate { get; set; }
    }

    /// <inheritdoc />
    [DataContract]
    public class TransactionDeleteRequestRequest : TransactionDeleteRequest
    {
    }

    /// <inheritdoc />
    [DataContract]
    public class TransactionDeleteRequestResponse : TransactionDeleteRequest, IIdentified<Guid>
    {
        /// <inheritdoc />
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }
    }
}
