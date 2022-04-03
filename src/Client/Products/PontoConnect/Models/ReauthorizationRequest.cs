using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Models
{
    [DataContract]
    public class ReauthorizationRequest : Identified<Guid>
    {
        [DataMember(Name = "redirectUri", EmitDefaultValue = false)]
        public Uri Redirect { get; set; }
    }

    [DataContract]
    public class ReauthorizationRequestLinks
    {
        [DataMember(Name = "redirect", EmitDefaultValue = false)]
        public string RedirectString { get; set; }

        public Uri Redirect => string.IsNullOrWhiteSpace(RedirectString)
            ? null
            : new Uri(RedirectString);
    }
}
