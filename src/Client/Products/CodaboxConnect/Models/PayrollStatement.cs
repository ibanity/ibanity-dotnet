using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.CodaboxConnect.Models
{
    /// <summary>
    /// This resource allows an Accounting Software to retrieve a payroll statement for a client of an accounting office.
    /// </summary>
    [DataContract]
    public class PayrollStatement : Identified<Guid>
    {
        /// <summary>
        /// When this payroll statement was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this payroll statement was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Id of social office that issued the payroll statement
        /// </summary>
        /// <value>Id of social office that issued the payroll statement</value>
        [DataMember(Name = "socialOfficeId", EmitDefaultValue = false)]
        public string SocialOfficeId { get; set; }
    }
}
