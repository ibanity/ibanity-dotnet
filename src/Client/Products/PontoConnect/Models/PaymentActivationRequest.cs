using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    /// <summary>
    /// <para>This is an object representing a payment activation request. If your customer has not yet requested payment activation for their organization (as indicated by the user info endpoint), you can redirect them to Ponto to submit a request for payment activation.</para>
    /// <para>When creating the payment activation request, you will receive the redirect link in the response. Your customer should be redirected to this url to begin the process. At the end of the flow, they will be returned to the redirect uri that you defined.</para>
    /// <para>When using this endpoint in the sandbox, the redirection flow will work but the user will not be prompted to request payment activation as this is enabled by default in the sandbox.</para>
    /// </summary>
    [DataContract]
    public class PaymentActivationRequest : Identified<Guid>
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
    public class PaymentActivationRequestLinks
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
