using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    /// <summary>
    /// <para>This is an object representing a financial institution transaction, a fake transaction on a fake account you can create for test purposes.</para>
    /// <para>Once the account corresponding to the financial institution account has been synchronized, your custom financial institution transactions will be visible in the transactions list.</para>
    /// </summary>
    [DataContract]
    public class SandboxTransaction : Transaction, IIdentified<Guid>
    {
        /// <inheritdoc />
        public Guid Id { get; set; }
    }
}
