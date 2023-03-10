using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    /// <summary>
    /// This endpoint provides the usage of your integration by the provided organization during a given month. In order to continue to allow access to this information if an integration is revoked, you must use a client access token for this endpoint.
    /// </summary>
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
        /// Number of initiated bulk payment created by the integration
        /// </summary>
        /// <value>Number of initiated bulk payment created by the integration</value>
        [DataMember(Name = "bulkPaymentCount", EmitDefaultValue = true)]
        public int BulkPaymentCount { get; set; }

        /// <summary>
        /// Number of accounts linked to the integration. The total is prorated, so may be a decimal number if accounts have been linked or unlinked during the month.
        /// </summary>
        /// <value>Number of accounts linked to the integration. The total is prorated, so may be a decimal number if accounts have been linked or unlinked during the month.</value>
        [DataMember(Name = "accountCount", EmitDefaultValue = true)]
        public decimal AccountCount { get; set; }

        /// <summary>
        /// Year of the usage.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Month of the usage.
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// ID of the corresponding organization
        /// </summary>
        public Guid OrganizationId { get; set; }
    }

    /// <summary>
    /// Details about the corresponding organization
    /// </summary>
    [DataContract]
    public class UsageRelationships
    {
        /// <summary>
        /// Details about the corresponding organization
        /// </summary>
        [DataMember(Name = "organization", EmitDefaultValue = false)]
        public JsonApi.Relationship Organization { get; set; }
    }
}
