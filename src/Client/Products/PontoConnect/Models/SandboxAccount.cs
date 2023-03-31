using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    /// <summary>
    /// <para>This is an object representing a financial institution account, a fake account you can use for test purposes in a sandbox integration.</para>
    /// <para>These sandbox accounts are available only to the related organization, and can be authorized in the Ponto dashboard.</para>
    /// <para>A financial institution account belongs to a financial institution and can have many associated financial institution transactions.</para>
    /// </summary>
    [DataContract]
    public class SandboxAccount : Account, IIdentified<Guid>
    {
        /// <inheritdoc />
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }
    }
}
