using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    [DataContract]
    public class UserInfo
    {
        /// <summary>
        /// ID of the organization corresponding to the provided access token
        /// </summary>
        /// <value>ID of the organization corresponding to the provided access token</value>
        [DataMember(Name = "sub", EmitDefaultValue = false)]
        public Guid Sub { get; set; }

        /// <summary>
        /// Indicates whether the organization has requested to be able to sign payments from Ponto. If it is &lt;code&gt;false&lt;/code&gt; (or you are in the sandbox), you may use the &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#request-payment-activation&#39;&gt;payment activation request&lt;/a&gt; redirect flow.
        /// </summary>
        /// <value>Indicates whether the organization has requested to be able to sign payments from Ponto. If it is &lt;code&gt;false&lt;/code&gt; (or you are in the sandbox), you may use the &lt;a href&#x3D;&#39;https://documentation.ibanity.com//api#request-payment-activation&#39;&gt;payment activation request&lt;/a&gt; redirect flow.</value>
        [DataMember(Name = "paymentsActivationRequested", EmitDefaultValue = true)]
        public bool PaymentsActivationRequested { get; set; }

        /// <summary>
        /// Indicates whether the organization can currently sign live payments from Ponto. Must be &lt;code&gt;true&lt;/code&gt; to use the (bulk) payment redirect flow.
        /// </summary>
        /// <value>Indicates whether the organization can currently sign live payments from Ponto. Must be &lt;code&gt;true&lt;/code&gt; to use the (bulk) payment redirect flow.</value>
        [DataMember(Name = "paymentsActivated", EmitDefaultValue = true)]
        public bool PaymentsActivated { get; set; }

        /// <summary>
        /// Indicates whether the organization has completed the onboarding process in the Ponto dashboard. If not completed within 72 hours of creation, the organization&#39;s account information and integration will be automatically deleted from Ponto.
        /// </summary>
        /// <value>Indicates whether the organization has completed the onboarding process in the Ponto dashboard. If not completed within 72 hours of creation, the organization&#39;s account information and integration will be automatically deleted from Ponto.</value>
        [DataMember(Name = "onboardingComplete", EmitDefaultValue = true)]
        public bool OnboardingComplete { get; set; }

        /// <summary>
        /// Name of the organization
        /// </summary>
        /// <value>Name of the organization</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }
    }
}
