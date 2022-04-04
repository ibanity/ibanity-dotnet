using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    [DataContract]
    public class OnboardingDetails
    {
        /// <summary>
        /// VAT number corresponding to the onboarding user&#39;s organization
        /// </summary>
        /// <value>VAT number corresponding to the onboarding user&#39;s organization</value>
        [DataMember(Name = "vatNumber", EmitDefaultValue = false)]
        public string VatNumber { get; set; }

        /// <summary>
        /// Phone number of the onboarding user
        /// </summary>
        /// <value>Phone number of the onboarding user</value>
        [DataMember(Name = "phoneNumber", EmitDefaultValue = false)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Name of the onboarding user&#39;s organization
        /// </summary>
        /// <value>Name of the onboarding user&#39;s organization</value>
        [DataMember(Name = "organizationName", EmitDefaultValue = false)]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Last name of the onboarding user
        /// </summary>
        /// <value>Last name of the onboarding user</value>
        [DataMember(Name = "lastName", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// First name of the onboarding user
        /// </summary>
        /// <value>First name of the onboarding user</value>
        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Enterprise number corresponding to the onboarding user&#39;s organization
        /// </summary>
        /// <value>Enterprise number corresponding to the onboarding user&#39;s organization</value>
        [DataMember(Name = "enterpriseNumber", EmitDefaultValue = false)]
        public string EnterpriseNumber { get; set; }

        /// <summary>
        /// Email belonging to the onboarding user
        /// </summary>
        /// <value>Email belonging to the onboarding user</value>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        /// Street address of the onboarding user&#39;s organization
        /// </summary>
        /// <value>Street address of the onboarding user&#39;s organization</value>
        [DataMember(Name = "addressStreetAddress", EmitDefaultValue = false)]
        public string AddressStreetAddress { get; set; }

        /// <summary>
        /// Postal code of the onboarding user&#39;s organization
        /// </summary>
        /// <value>Postal code of the onboarding user&#39;s organization</value>
        [DataMember(Name = "addressPostalCode", EmitDefaultValue = false)]
        public string AddressPostalCode { get; set; }

        /// <summary>
        /// Country of the onboarding user&#39;s organization
        /// </summary>
        /// <value>Country of the onboarding user&#39;s organization</value>
        [DataMember(Name = "addressCountry", EmitDefaultValue = false)]
        public string AddressCountry { get; set; }

        /// <summary>
        /// City of the onboarding user&#39;s organization
        /// </summary>
        /// <value>City of the onboarding user&#39;s organization</value>
        [DataMember(Name = "addressCity", EmitDefaultValue = false)]
        public string AddressCity { get; set; }
    }

    [DataContract]
    public class OnboardingDetailsResponse : OnboardingDetails, IIdentified<Guid>
    {
        public Guid Id { get; set; }
    }
}
