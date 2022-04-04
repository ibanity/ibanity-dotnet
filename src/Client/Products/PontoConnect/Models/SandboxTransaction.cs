using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    [DataContract]
    public class SandboxTransaction : Transaction, IIdentified<Guid>
    {
        public Guid Id { get; set; }
    }
}
