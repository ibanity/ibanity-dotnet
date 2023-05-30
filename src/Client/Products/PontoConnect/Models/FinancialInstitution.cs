using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    /// <summary>
    /// This is an object representing a financial institution, providing its basic details - ID and name. Only the financial institutions corresponding to authorized accounts will be available on the API.
    /// </summary>
    [DataContract]
    public class FinancialInstitution : Identified<Guid>
    {
        /// <summary>
        /// Availability of the connection (experimental, beta, stable)
        /// </summary>
        /// <value>Availability of the connection (experimental, beta, stable)</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Attribute used to group multiple individual financial institutions in the same country
        /// </summary>
        /// <value>Attribute used to group multiple individual financial institutions in the same country</value>
        [DataMember(Name = "sharedBrandReference", EmitDefaultValue = false)]
        public Object SharedBrandReference { get; set; }

        /// <summary>
        /// Customer-friendly name of the financial institution&#39;s shared brand, if it is a member of one
        /// </summary>
        /// <value>Customer-friendly name of the financial institution&#39;s shared brand, if it is a member of one</value>
        [DataMember(Name = "sharedBrandName", EmitDefaultValue = false)]
        public Object SharedBrandName { get; set; }

        /// <summary>
        /// Hexadecimal color code related to the secondary branding color of the financial institution
        /// </summary>
        /// <value>Hexadecimal color code related to the secondary branding color of the financial institution</value>
        [DataMember(Name = "secondaryColor", EmitDefaultValue = false)]
        public string SecondaryColor { get; set; }

        /// <summary>
        /// Hexadecimal color code related to the primary branding color of the financial institution
        /// </summary>
        /// <value>Hexadecimal color code related to the primary branding color of the financial institution</value>
        [DataMember(Name = "primaryColor", EmitDefaultValue = false)]
        public string PrimaryColor { get; set; }

        /// <summary>
        /// Identifies which values are accepted for the periodic payment initiation request &lt;code&gt;productType&lt;/code&gt;
        /// </summary>
        /// <value>Identifies which values are accepted for the periodic payment initiation request &lt;code&gt;productType&lt;/code&gt;</value>
        [DataMember(Name = "periodicPaymentsProductTypes", EmitDefaultValue = false)]
        public List<string> PeriodicPaymentsProductTypes { get; set; }

        /// <summary>
        /// Indicates whether the financial institution allows periodic payment initiation requests
        /// </summary>
        /// <value>Indicates whether the financial institution allows periodic payment initiation requests</value>
        [DataMember(Name = "periodicPaymentsEnabled", EmitDefaultValue = true)]
        public bool PeriodicPaymentsEnabled { get; set; }

        /// <summary>
        /// Identifies which values are accepted for the &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#payment-initiation-request&#39;&gt;payment initiation request&lt;/a&gt; &lt;code&gt;productType&lt;/code&gt;
        /// </summary>
        /// <value>Identifies which values are accepted for the &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#payment-initiation-request&#39;&gt;payment initiation request&lt;/a&gt; &lt;code&gt;productType&lt;/code&gt;</value>
        [DataMember(Name = "paymentsProductTypes", EmitDefaultValue = false)]
        public List<string> PaymentsProductTypes { get; set; }

        /// <summary>
        /// Indicates whether the financial institution allows &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#payment-initiation-request&#39;&gt;payment initiation requests&lt;/a&gt;
        /// </summary>
        /// <value>Indicates whether the financial institution allows &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#payment-initiation-request&#39;&gt;payment initiation requests&lt;/a&gt;</value>
        [DataMember(Name = "paymentsEnabled", EmitDefaultValue = true)]
        public bool PaymentsEnabled { get; set; }

        /// <summary>
        /// Name of the financial institution
        /// </summary>
        /// <value>Name of the financial institution</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Indicates if there is an ongoing or scheduled maintenance. If present, the possible values are:&lt;ul&gt;&lt;li&gt;&lt;code&gt;internal&lt;/code&gt;, meaning we are working on the connection and no data access is possible&lt;/li&gt;&lt;li&gt;&lt;code&gt;financialInstitution&lt;/code&gt;, indicating the financial institution cannot be reached, but existing data is available in read-only mode&lt;/li&gt;&lt;/ul&gt;
        /// </summary>
        /// <value>Indicates if there is an ongoing or scheduled maintenance. If present, the possible values are:&lt;ul&gt;&lt;li&gt;&lt;code&gt;internal&lt;/code&gt;, meaning we are working on the connection and no data access is possible&lt;/li&gt;&lt;li&gt;&lt;code&gt;financialInstitution&lt;/code&gt;, indicating the financial institution cannot be reached, but existing data is available in read-only mode&lt;/li&gt;&lt;/ul&gt;</value>
        [DataMember(Name = "maintenanceType", EmitDefaultValue = false)]
        public Object MaintenanceType { get; set; }

        /// <summary>
        /// Indicates the end date of the maintenance.
        /// </summary>
        /// <value>Indicates the end date of the maintenance.</value>
        [DataMember(Name = "maintenanceTo", EmitDefaultValue = false)]
        public Object MaintenanceTo { get; set; }

        /// <summary>
        /// Indicates the start date of the maintenance.
        /// </summary>
        /// <value>Indicates the start date of the maintenance.</value>
        [DataMember(Name = "maintenanceFrom", EmitDefaultValue = false)]
        public Object MaintenanceFrom { get; set; }

        /// <summary>
        /// Location of the logo image for the financial institution
        /// </summary>
        /// <value>Location of the logo image for the financial institution</value>
        [DataMember(Name = "logoUrl", EmitDefaultValue = false)]
        public string LogoUrl { get; set; }

        /// <summary>
        /// Indicates whether a &lt;code&gt;requestedExecutionDate&lt;/code&gt; is supported for &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#payment&#39;&gt;payments&lt;/a&gt; from accounts belonging to this financial institution
        /// </summary>
        /// <value>Indicates whether a &lt;code&gt;requestedExecutionDate&lt;/code&gt; is supported for &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#payment&#39;&gt;payments&lt;/a&gt; from accounts belonging to this financial institution</value>
        [DataMember(Name = "futureDatedPaymentsAllowed", EmitDefaultValue = true)]
        public bool FutureDatedPaymentsAllowed { get; set; }

        /// <summary>
        /// Indicates if the financial institution has been deprecated. Very rarely we may need to deprecate a &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#financial-institution&#39;&gt;financial institution&lt;/a&gt; and ask your users to authorize their accounts again on its replacement. You will be able to access both accounts (if authorized) but you will not be able to synchronize the deprecated account once its authorization has expired.
        /// </summary>
        /// <value>Indicates if the financial institution has been deprecated. Very rarely we may need to deprecate a &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#financial-institution&#39;&gt;financial institution&lt;/a&gt; and ask your users to authorize their accounts again on its replacement. You will be able to access both accounts (if authorized) but you will not be able to synchronize the deprecated account once its authorization has expired.</value>
        [DataMember(Name = "deprecated", EmitDefaultValue = true)]
        public bool Deprecated { get; set; }

        /// <summary>
        /// Country of the financial institution, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2&#39;&gt;ISO 3166-1 alpha-2&lt;/a&gt; format. Is &lt;code&gt;null&lt;/code&gt; in the case of an international financial institution.
        /// </summary>
        /// <value>Country of the financial institution, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2&#39;&gt;ISO 3166-1 alpha-2&lt;/a&gt; format. Is &lt;code&gt;null&lt;/code&gt; in the case of an international financial institution.</value>
        [DataMember(Name = "country", EmitDefaultValue = false)]
        public string Country { get; set; }

        /// <summary>
        /// Identifies which values are accepted for the bulk payment initiation request &lt;code&gt;productType&lt;/code&gt;
        /// </summary>
        /// <value>Identifies which values are accepted for the bulk payment initiation request &lt;code&gt;productType&lt;/code&gt;</value>
        [DataMember(Name = "bulkPaymentsProductTypes", EmitDefaultValue = false)]
        public List<string> BulkPaymentsProductTypes { get; set; }

        /// <summary>
        /// Indicates whether the financial institution allows bulk payment initiation requests
        /// </summary>
        /// <value>Indicates whether the financial institution allows bulk payment initiation requests</value>
        [DataMember(Name = "bulkPaymentsEnabled", EmitDefaultValue = true)]
        public bool BulkPaymentsEnabled { get; set; }

        /// <summary>
        /// Identifier for the financial institution, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_9362&#39;&gt;ISO9362&lt;/a&gt; format.
        /// </summary>
        /// <value>Identifier for the financial institution, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_9362&#39;&gt;ISO9362&lt;/a&gt; format.</value>
        [DataMember(Name = "bic", EmitDefaultValue = false)]
        public string Bic { get; set; }

        /// <summary>
        /// TBD
        /// </summary>
        /// <value>TBD</value>
        [DataMember(Name = "timeZone", EmitDefaultValue = false)]
        public string TimeZone { get; set; }

        /// <summary>
        /// Short string representation.
        /// </summary>
        /// <returns>Short string representation</returns>
        public override string ToString() => $"{Name} ({Id})";
    }
}
