using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    [DataContract]
    public class SandboxAccount : Account, IIdentified<Guid>
    {
        public Guid Id { get; set; }
    }
}
