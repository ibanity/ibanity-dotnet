using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    [DataContract]
    public class Usage
    {
        /// <summary>
        /// Number of initiated payments created by the integration
        /// </summary>
        /// <value>Number of initiated payments created by the integration</value>
        [DataMember(Name = "paymentCount", EmitDefaultValue = false)]
        public int PaymentCount { get; set; }

        /// <summary>
        /// Number of accounts which initiated a payment created by the integration
        /// </summary>
        /// <value>Number of accounts which initiated a payment created by the integration</value>
        [DataMember(Name = "paymentAccountCount", EmitDefaultValue = true)]
        public int PaymentAccountCount { get; set; }

        /// <summary>
        /// Number of initiated bulk payment bundles created by the integration
        /// </summary>
        /// <value>Number of initiated bulk payment bundles created by the integration</value>
        [DataMember(Name = "bulkPaymentBundleCount", EmitDefaultValue = true)]
        public int BulkPaymentBundleCount { get; set; }

        /// <summary>
        /// Number of accounts linked to the integration. The total is prorated, so may be a decimal number if accounts have been linked or unlinked during the month.
        /// </summary>
        /// <value>Number of accounts linked to the integration. The total is prorated, so may be a decimal number if accounts have been linked or unlinked during the month.</value>
        [DataMember(Name = "accountCount", EmitDefaultValue = true)]
        public decimal AccountCount { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public Guid OrganizationId { get; set; }
    }

    [DataContract]
    public class UsageRelationships
    {
        [DataMember(Name = "organization", EmitDefaultValue = false)]
        public JsonApi.Relationship Organization { get; set; }
    }
}
