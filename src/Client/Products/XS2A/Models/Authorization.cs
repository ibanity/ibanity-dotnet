using System;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// <p>This object represent the authorization resource. When you perform an Authorization flow using TPP managed authorization flow, you need to create an authorization resource to complete the flow.</p>
    /// <p>The attribute queryParameters contains the query parameters returned by the financial institution when the customer is redirected to your configured redirect uri.</p>
    /// </summary>
    [DataContract]
    public class RequestAuthorization
    {
        /// <summary>
        /// Parameters returned by the financial institution when the customer is redirected to your backend at the end of the authorization flow.
        /// </summary>
        /// <value>Parameters returned by the financial institution when the customer is redirected to your backend at the end of the authorization flow.</value>
        [DataMember(Name = "queryParameters", EmitDefaultValue = true)]
        public Object QueryParameters { get; set; }
    }

    /// <summary>
    /// <p>This object represent the authorization resource. When you perform an Authorization flow using TPP managed authorization flow, you need to create an authorization resource to complete the flow.</p>
    /// <p>The attribute queryParameters contains the query parameters returned by the financial institution when the customer is redirected to your configured redirect uri.</p>
    /// </summary>
    [DataContract]
    public class AuthorizationResponse : Identified<Guid>
    {
        /// <summary>
        /// URI to redirect to from your customer frontend to conduct the authorization flow
        /// </summary>
        [DataMember(Name = "nextRedirect", EmitDefaultValue = false)]
        public Uri NextRedirect { get; set; }
    }

    /// <summary>
    /// URI to redirect to from your customer frontend to conduct the authorization flow
    /// </summary>
    [DataContract]
    public class AuthorizationLinks
    {
        /// <summary>
        /// URI to redirect to from your customer frontend to conduct the authorization flow.
        /// </summary>
        [DataMember(Name = "nextRedirect", EmitDefaultValue = false)]
        public string NextRedirectString { get; set; }

        /// <summary>
        /// URI to redirect to from your customer frontend to conduct the authorization flow.
        /// </summary>
        public Uri NextRedirect => string.IsNullOrWhiteSpace(NextRedirectString)
            ? null
            : new Uri(NextRedirectString);
    }
}
