using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    /// <summary>
    /// <para>This object allows you to request the reauthorization of a specific bank account.</para>
    /// <para>By providing a redirect URI, you can create a redirect link to which you can send your customer so they can directly reauthorize their account on Ponto. After reauthorizing at their bank portal, they are redirected automatically back to your application, to the redirect URI of your choosing.</para>
    /// </summary>
    [DataContract]
    public class ReauthorizationRequest : Identified<Guid>
    {
        /// <summary>
        /// URI that your user will be redirected to at the end of the authorization flow.
        /// </summary>
        [DataMember(Name = "redirectUri", EmitDefaultValue = false)]
        public Uri Redirect { get; set; }
    }

    /// <summary>
    /// URI to redirect to from your customer frontend to conduct the authorization flow.
    /// </summary>
    [DataContract]
    public class ReauthorizationRequestLinks
    {
        /// <summary>
        /// URI to redirect to from your customer frontend to conduct the authorization flow.
        /// </summary>
        [DataMember(Name = "redirect", EmitDefaultValue = false)]
        public string RedirectString { get; set; }

        /// <summary>
        /// URI to redirect to from your customer frontend to conduct the authorization flow.
        /// </summary>
        public Uri Redirect => string.IsNullOrWhiteSpace(RedirectString)
            ? null
            : new Uri(RedirectString);
    }
}
