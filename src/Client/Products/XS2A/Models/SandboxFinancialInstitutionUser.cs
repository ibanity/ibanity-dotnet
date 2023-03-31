using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// <p>This is an object representing a financial institution user. It is a fake financial institution customer you can create for test purposes.</p>
    /// <p>From this object, you can follow the links to its related financial institution accounts and transactions.</p>
    /// </summary>
    [DataContract]
    public class SandboxFinancialInstitutionUser
    {
        /// <summary>
        /// User&#39;s fake financial institution login
        /// </summary>
        /// <value>User&#39;s fake financial institution login</value>
        [DataMember(Name = "login", EmitDefaultValue = false)]
        public string Login { get; set; }

        /// <summary>
        /// User&#39;s fake financial institution login. Remember this is test data, so the password is stored and distributed unencrypted.
        /// </summary>
        /// <value>User&#39;s fake financial institution login. Remember this is test data, so the password is stored and distributed unencrypted.</value>
        [DataMember(Name = "password", EmitDefaultValue = false)]
        public string Password { get; set; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        /// <value>Last name of the user</value>
        [DataMember(Name = "lastName", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// First name of the user
        /// </summary>
        /// <value>First name of the user</value>
        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }
    }

    /// <inheritdoc />
    [DataContract]
    public class SandboxFinancialInstitutionUserResponse : SandboxFinancialInstitutionUser, IIdentified<Guid>
    {
        /// <inheritdoc />
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// When this financial institution user was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this financial institution user was created. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// When this financial institution user was last synchronized successfully. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When this financial institution user was last synchronized successfully. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "updatedAt", EmitDefaultValue = false)]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
