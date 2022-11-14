using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// This is an object representing a financial institution, providing its basic details - including whether it is a sandbox object or not.
    /// </summary>
    [DataContract]
    public class FinancialInstitution : Identified<Guid>
    {
        /// <summary>
        /// Identifies the authorization models that are offered by the financial institution. &lt;a href&#x3D;&#39;/xs2a/products#authorization-models&#39;&gt;Learn more&lt;/a&gt;.
        /// </summary>
        /// <value>Identifies the authorization models that are offered by the financial institution. &lt;a href&#x3D;&#39;/xs2a/products#authorization-models&#39;&gt;Learn more&lt;/a&gt;.</value>
        [DataMember(Name = "authorizationModels", EmitDefaultValue = false)]
        public List<string> AuthorizationModels { get; set; }

        /// <summary>
        /// Identifier for the financial institution, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_9362&#39;&gt;ISO9362&lt;/a&gt; format.
        /// </summary>
        /// <value>Identifier for the financial institution, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_9362&#39;&gt;ISO9362&lt;/a&gt; format.</value>
        [DataMember(Name = "bic", EmitDefaultValue = false)]
        public string Bic { get; set; }

        /// <summary>
        /// Indicates whether the financial institution allows bulk payment initiation requests
        /// </summary>
        /// <value>Indicates whether the financial institution allows bulk payment initiation requests</value>
        [DataMember(Name = "bulkPaymentsEnabled", EmitDefaultValue = false)]
        public bool? BulkPaymentsEnabled { get; set; }

        /// <summary>
        /// Identifies which values are accepted for the bulk payment initiation request &lt;code&gt;productType&lt;/code&gt;
        /// </summary>
        /// <value>Identifies which values are accepted for the bulk payment initiation request &lt;code&gt;productType&lt;/code&gt;</value>
        [DataMember(Name = "bulkPaymentsProductTypes", EmitDefaultValue = false)]
        public List<string> BulkPaymentsProductTypes { get; set; }

        /// <summary>
        /// Country of the financial institution, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2&#39;&gt;ISO 3166-1 alpha-2&lt;/a&gt; format. Is &lt;code&gt;null&lt;/code&gt; in the case of an international financial institution.
        /// </summary>
        /// <value>Country of the financial institution, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2&#39;&gt;ISO 3166-1 alpha-2&lt;/a&gt; format. Is &lt;code&gt;null&lt;/code&gt; in the case of an international financial institution.</value>
        [DataMember(Name = "country", EmitDefaultValue = false)]
        public string Country { get; set; }

        /// <summary>
        /// Indicates if a &lt;code&gt;financialInstitutionCustomerReference&lt;/code&gt; must be provided for &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#create-account-information-access-request&#39;&gt;account information access requests&lt;/a&gt; and &lt;a href&#x3D;&#39;#create-payment-initiation-request&#39;&gt;payment initiation requests&lt;/a&gt; for this financial institution
        /// </summary>
        /// <value>Indicates if a &lt;code&gt;financialInstitutionCustomerReference&lt;/code&gt; must be provided for &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#create-account-information-access-request&#39;&gt;account information access requests&lt;/a&gt; and &lt;a href&#x3D;&#39;#create-payment-initiation-request&#39;&gt;payment initiation requests&lt;/a&gt; for this financial institution</value>
        [DataMember(Name = "financialInstitutionCustomerReferenceRequired", EmitDefaultValue = false)]
        public bool? FinancialInstitutionCustomerReferenceRequired { get; set; }

        /// <summary>
        /// Indicates whether a &lt;code&gt;requestedExecutionDate&lt;/code&gt; is supported for &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#payment-initiation-request&#39;&gt;payment initiation requests&lt;/a&gt; from accounts belonging to this financial institution
        /// </summary>
        /// <value>Indicates whether a &lt;code&gt;requestedExecutionDate&lt;/code&gt; is supported for &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#payment-initiation-request&#39;&gt;payment initiation requests&lt;/a&gt; from accounts belonging to this financial institution</value>
        [DataMember(Name = "futureDatedPaymentsAllowed", EmitDefaultValue = false)]
        public bool? FutureDatedPaymentsAllowed { get; set; }

        /// <summary>
        /// Location of the logo image for the financial institution
        /// </summary>
        /// <value>Location of the logo image for the financial institution</value>
        [DataMember(Name = "logoUrl", EmitDefaultValue = false)]
        public string LogoUrl { get; set; }

        /// <summary>
        /// Indicates the start date of the maintenance.
        /// </summary>
        /// <value>Indicates the start date of the maintenance.</value>
        [DataMember(Name = "maintenanceFrom", EmitDefaultValue = false)]
        public Object MaintenanceFrom { get; set; }

        /// <summary>
        /// Indicates the end date of the maintenance.
        /// </summary>
        /// <value>Indicates the end date of the maintenance.</value>
        [DataMember(Name = "maintenanceTo", EmitDefaultValue = false)]
        public Object MaintenanceTo { get; set; }

        /// <summary>
        /// Indicates if there is an ongoing or scheduled maintenance. If present, the possible values are:&lt;ul&gt;&lt;li&gt;&lt;code&gt;internal&lt;/code&gt;, meaning we are working on the connection and no data access is possible&lt;/li&gt;&lt;li&gt;&lt;code&gt;financialInstitution&lt;/code&gt;, indicating the financial institution cannot be reached, but existing data is available in read-only mode&lt;/li&gt;&lt;/ul&gt;
        /// </summary>
        /// <value>Indicates if there is an ongoing or scheduled maintenance. If present, the possible values are:&lt;ul&gt;&lt;li&gt;&lt;code&gt;internal&lt;/code&gt;, meaning we are working on the connection and no data access is possible&lt;/li&gt;&lt;li&gt;&lt;code&gt;financialInstitution&lt;/code&gt;, indicating the financial institution cannot be reached, but existing data is available in read-only mode&lt;/li&gt;&lt;/ul&gt;</value>
        [DataMember(Name = "maintenanceType", EmitDefaultValue = false)]
        public Object MaintenanceType { get; set; }

        /// <summary>
        /// Indicates the maximum permitted length of the &lt;code&gt;requestedAccountReferences&lt;/code&gt; array when creating an &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#create-account-information-access-request&#39;&gt;account information access request&lt;/a&gt; for the financial institution. When &lt;code&gt;0&lt;/code&gt;, the account reference(s) must be provided during the subsequent authorization process rather than during the creation of the access request. When &lt;code&gt;null&lt;/code&gt;, there is no upper limit to the number of account references that may be provided with the request.
        /// </summary>
        /// <value>Indicates the maximum permitted length of the &lt;code&gt;requestedAccountReferences&lt;/code&gt; array when creating an &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#create-account-information-access-request&#39;&gt;account information access request&lt;/a&gt; for the financial institution. When &lt;code&gt;0&lt;/code&gt;, the account reference(s) must be provided during the subsequent authorization process rather than during the creation of the access request. When &lt;code&gt;null&lt;/code&gt;, there is no upper limit to the number of account references that may be provided with the request.</value>
        [DataMember(Name = "maxRequestedAccountReferences", EmitDefaultValue = false)]
        public Object MaxRequestedAccountReferences { get; set; }

        /// <summary>
        /// Indicates the minimum permitted length of the &lt;code&gt;requestedAccountReferences&lt;/code&gt; array when creating an &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#create-account-information-access-request&#39;&gt;account information access request&lt;/a&gt; for the financial institution.
        /// </summary>
        /// <value>Indicates the minimum permitted length of the &lt;code&gt;requestedAccountReferences&lt;/code&gt; array when creating an &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#create-account-information-access-request&#39;&gt;account information access request&lt;/a&gt; for the financial institution.</value>
        [DataMember(Name = "minRequestedAccountReferences", EmitDefaultValue = false)]
        public decimal? MinRequestedAccountReferences { get; set; }

        /// <summary>
        /// Name of the financial institution
        /// </summary>
        /// <value>Name of the financial institution</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Indicates whether the financial institution allows &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#payment-initiation-request&#39;&gt;payment initiation requests&lt;/a&gt;
        /// </summary>
        /// <value>Indicates whether the financial institution allows &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#payment-initiation-request&#39;&gt;payment initiation requests&lt;/a&gt;</value>
        [DataMember(Name = "paymentsEnabled", EmitDefaultValue = false)]
        public bool? PaymentsEnabled { get; set; }

        /// <summary>
        /// Identifies which values are accepted for the &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#payment-initiation-request&#39;&gt;payment initiation request&lt;/a&gt; &lt;code&gt;productType&lt;/code&gt;
        /// </summary>
        /// <value>Identifies which values are accepted for the &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#payment-initiation-request&#39;&gt;payment initiation request&lt;/a&gt; &lt;code&gt;productType&lt;/code&gt;</value>
        [DataMember(Name = "paymentsProductTypes", EmitDefaultValue = false)]
        public List<string> PaymentsProductTypes { get; set; }

        /// <summary>
        /// Indicates whether the financial institution allows periodic payment initiation requests
        /// </summary>
        /// <value>Indicates whether the financial institution allows periodic payment initiation requests</value>
        [DataMember(Name = "periodicPaymentsEnabled", EmitDefaultValue = false)]
        public bool? PeriodicPaymentsEnabled { get; set; }

        /// <summary>
        /// Identifies which values are accepted for the periodic payment initiation request &lt;code&gt;productType&lt;/code&gt;
        /// </summary>
        /// <value>Identifies which values are accepted for the periodic payment initiation request &lt;code&gt;productType&lt;/code&gt;</value>
        [DataMember(Name = "periodicPaymentsProductTypes", EmitDefaultValue = false)]
        public List<string> PeriodicPaymentsProductTypes { get; set; }

        /// <summary>
        /// Hexadecimal color code related to the primary branding color of the financial institution
        /// </summary>
        /// <value>Hexadecimal color code related to the primary branding color of the financial institution</value>
        [DataMember(Name = "primaryColor", EmitDefaultValue = false)]
        public string PrimaryColor { get; set; }

        /// <summary>
        /// Indicates whether credentials must be stored to access a customer&#39;s account information
        /// </summary>
        /// <value>Indicates whether credentials must be stored to access a customer&#39;s account information</value>
        [DataMember(Name = "requiresCredentialStorage", EmitDefaultValue = false)]
        public bool? RequiresCredentialStorage { get; set; }

        /// <summary>
        /// Indicates whether the IP address of the customer must be provided when creating an &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#create-account-information-access-request&#39;&gt;account information access request&lt;/a&gt; or &lt;a href&#x3D;&#39;#create-payment-initiation-request&#39;&gt;payment initiation request&lt;/a&gt; for the financial institution
        /// </summary>
        /// <value>Indicates whether the IP address of the customer must be provided when creating an &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#create-account-information-access-request&#39;&gt;account information access request&lt;/a&gt; or &lt;a href&#x3D;&#39;#create-payment-initiation-request&#39;&gt;payment initiation request&lt;/a&gt; for the financial institution</value>
        [DataMember(Name = "requiresCustomerIpAddress", EmitDefaultValue = false)]
        public bool? RequiresCustomerIpAddress { get; set; }

        /// <summary>
        /// Indicates whether the financial institution is one in the sandbox or the real deal
        /// </summary>
        /// <value>Indicates whether the financial institution is one in the sandbox or the real deal</value>
        [DataMember(Name = "sandbox", EmitDefaultValue = false)]
        public bool? Sandbox { get; set; }

        /// <summary>
        /// Hexadecimal color code related to the secondary branding color of the financial institution
        /// </summary>
        /// <value>Hexadecimal color code related to the secondary branding color of the financial institution</value>
        [DataMember(Name = "secondaryColor", EmitDefaultValue = false)]
        public string SecondaryColor { get; set; }

        /// <summary>
        /// Customer-friendly name of the financial institution&#39;s shared brand, if it is a member of one
        /// </summary>
        /// <value>Customer-friendly name of the financial institution&#39;s shared brand, if it is a member of one</value>
        [DataMember(Name = "sharedBrandName", EmitDefaultValue = false)]
        public string SharedBrandName { get; set; }

        /// <summary>
        /// Attribute used to group multiple individual financial institutions in the same country
        /// </summary>
        /// <value>Attribute used to group multiple individual financial institutions in the same country</value>
        [DataMember(Name = "sharedBrandReference", EmitDefaultValue = false)]
        public string SharedBrandReference { get; set; }

        /// <summary>
        /// Availability of the connection (experimental, beta, stable)
        /// </summary>
        /// <value>Availability of the connection (experimental, beta, stable)</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }
    }

    /// <summary>
    /// This is an object representing a financial institution, providing its basic details - including whether it is a sandbox object or not.
    /// </summary>
    public class SandboxFinancialInstitution
    {
        /// <summary>
        /// Name of the financial institution
        /// </summary>
        /// <value>Name of the financial institution</value>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = true)]
        public string Name { get; set; }

        /// <summary>
        /// Location of the logo image for the financial institution
        /// </summary>
        /// <value>Location of the logo image for the financial institution</value>
        [DataMember(Name = "logoUrl", EmitDefaultValue = false)]
        public string LogoUrl { get; set; }

        /// <summary>
        /// Identifier for the financial institution, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_9362&#39;&gt;ISO9362&lt;/a&gt; format.
        /// </summary>
        /// <value>Identifier for the financial institution, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_9362&#39;&gt;ISO9362&lt;/a&gt; format.</value>
        [DataMember(Name = "bic", EmitDefaultValue = false)]
        public string Bic { get; set; }

        /// <summary>
        /// Country of the financial institution, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2&#39;&gt;ISO 3166-1 alpha-2&lt;/a&gt; format. Is &lt;code&gt;null&lt;/code&gt; in the case of an international financial institution.
        /// </summary>
        /// <value>Country of the financial institution, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2&#39;&gt;ISO 3166-1 alpha-2&lt;/a&gt; format. Is &lt;code&gt;null&lt;/code&gt; in the case of an international financial institution.</value>
        [DataMember(Name = "country", EmitDefaultValue = false)]
        public string Country { get; set; }

        /// <summary>
        /// Indicates if a &lt;code&gt;financialInstitutionCustomerReference&lt;/code&gt; must be provided for &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#create-account-information-access-request&#39;&gt;account information access requests&lt;/a&gt; and &lt;a href&#x3D;&#39;#create-payment-initiation-request&#39;&gt;payment initiation requests&lt;/a&gt; for this financial institution
        /// </summary>
        /// <value>Indicates if a &lt;code&gt;financialInstitutionCustomerReference&lt;/code&gt; must be provided for &lt;a href&#x3D;&#39;https://documentation.ibanity.com/xs2a/api#create-account-information-access-request&#39;&gt;account information access requests&lt;/a&gt; and &lt;a href&#x3D;&#39;#create-payment-initiation-request&#39;&gt;payment initiation requests&lt;/a&gt; for this financial institution</value>
        [DataMember(Name = "financialInstitutionCustomerReferenceRequired", EmitDefaultValue = true)]
        public bool? FinancialInstitutionCustomerReferenceRequired { get; set; }

        /// <summary>
        /// Identifies the authorization models that are offered by the financial institution. &lt;a href&#x3D;&#39;/xs2a/products#authorization-models&#39;&gt;Learn more&lt;/a&gt;.
        /// </summary>
        /// <value>Identifies the authorization models that are offered by the financial institution. &lt;a href&#x3D;&#39;/xs2a/products#authorization-models&#39;&gt;Learn more&lt;/a&gt;.</value>
        [DataMember(Name = "authorizationModels", EmitDefaultValue = false)]
        public List<string> AuthorizationModels { get; set; }

        /// <summary>
        /// Attribute used to group multiple individual financial institutions in the same country
        /// </summary>
        /// <value>Attribute used to group multiple individual financial institutions in the same country</value>
        [DataMember(Name = "sharedBrandReference", EmitDefaultValue = false)]
        public string SharedBrandReference { get; set; }

        /// <summary>
        /// Customer-friendly name of the financial institution&#39;s shared brand, if it is a member of one
        /// </summary>
        /// <value>Customer-friendly name of the financial institution&#39;s shared brand, if it is a member of one</value>
        [DataMember(Name = "sharedBrandName", EmitDefaultValue = false)]
        public string SharedBrandName { get; set; }
    }
}
