using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.IsabelConnect.Models
{
    /// <summary>
    /// <p>This is an object representing a bulk payment initiation request. When you want to request the initiation of payments on behalf of one of your customers, you can create one to start the authorization flow.</p>
    /// <p>When creating the request, you should provide the payment information by uploading a PAIN xml file. <see href="https://documentation.ibanity.com/isabel-connect/products#bulk-payment-initiation">Learn more about the supported formats in Isabel Connect.</see></p>
    /// </summary>
    [DataContract]
    public class BulkPaymentInitiationRequest : Identified<string>
    {
        /// <summary>
        /// Status of the bulk payment initiation request. &lt;a href&#x3D;&#39;/isabel-connect/products#bulk-payment-statuses&#39;&gt;See possible statuses&lt;/a&gt;.
        /// </summary>
        /// <value>Status of the bulk payment initiation request. &lt;a href&#x3D;&#39;/isabel-connect/products#bulk-payment-statuses&#39;&gt;See possible statuses&lt;/a&gt;.</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }
    }
}
