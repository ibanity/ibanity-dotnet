using System;
using System.Globalization;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.PontoConnect.Models
{
    /// <summary>
    /// <para>This is an object representing a payment request. When you want to initiate payment request from one of your user's accounts, you have to create one to start the authorization flow.</para>
    /// <para>If you provide a redirect URI when creating the payment request, you will receive a redirect link to send your customer to to start the authorization flow. Note that for live payment requests, your user must have already requested and been granted payment request service for their organization to use this flow.</para>
    /// <para>Otherwise, the user can sign the payment request in the Ponto Dashboard.</para>
    /// <para>When authorizing payment request initiation in the sandbox, you should use the pre-filled credentials and 123456 as the digipass response.</para>
    /// </summary>
    [DataContract]
    public class PaymentRequest
    {
        /// <summary>
        /// Type of remittance information, can be &lt;code&gt;structured&lt;/code&gt; or &lt;code&gt;unstructured&lt;/code&gt;
        /// </summary>
        /// <value>Type of remittance information, can be &lt;code&gt;structured&lt;/code&gt; or &lt;code&gt;unstructured&lt;/code&gt;</value>
        [DataMember(Name = "remittanceInformationType", EmitDefaultValue = false)]
        public string RemittanceInformationType { get; set; }

        /// <summary>
        /// Content of the remittance information (aka communication). Limited to 140 characters in the set &lt;code&gt;a b c d e f g h i j k l m n o p q r s t u v w x y z A B C D E F G H I J K L M N O P Q R S T U V W X Y Z 0 1 2 3 4 5 6 7 8 9 / - ? : ( ) . , &#39; + Space&lt;/code&gt; to ensure it is not rejected by the financial institution.
        /// </summary>
        /// <value>Content of the remittance information (aka communication). Limited to 140 characters in the set &lt;code&gt;a b c d e f g h i j k l m n o p q r s t u v w x y z A B C D E F G H I J K L M N O P Q R S T U V W X Y Z 0 1 2 3 4 5 6 7 8 9 / - ? : ( ) . , &#39; + Space&lt;/code&gt; to ensure it is not rejected by the financial institution.</value>
        [DataMember(Name = "remittanceInformation", EmitDefaultValue = false)]
        public string RemittanceInformation { get; set; }

        /// <summary>
        /// Currency of the payment request, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_4217&#39;&gt;ISO4217&lt;/a&gt; format
        /// </summary>
        /// <value>Currency of the payment request, in &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_4217&#39;&gt;ISO4217&lt;/a&gt; format</value>
        [DataMember(Name = "currency", IsRequired = true, EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        /// Name of the payee. Limited to 60 characters in the set &lt;code&gt;a b c d e f g h i j k l m n o p q r s t u v w x y z A B C D E F G H I J K L M N O P Q R S T U V W X Y Z 0 1 2 3 4 5 6 7 8 9 / - ? : ( ) . , &#39; + Space&lt;/code&gt; to ensure it is not rejected by the financial institution.
        /// </summary>
        /// <value>Name of the payee. Limited to 60 characters in the set &lt;code&gt;a b c d e f g h i j k l m n o p q r s t u v w x y z A B C D E F G H I J K L M N O P Q R S T U V W X Y Z 0 1 2 3 4 5 6 7 8 9 / - ? : ( ) . , &#39; + Space&lt;/code&gt; to ensure it is not rejected by the financial institution.</value>
        [DataMember(Name = "creditorName", IsRequired = false, EmitDefaultValue = false)]
        public string CreditorName { get; set; }

        /// <summary>
        /// Type of financial institution reference, currently must be &lt;code&gt;BIC&lt;/code&gt;
        /// </summary>
        /// <value>Type of financial institution reference, currently must be &lt;code&gt;BIC&lt;/code&gt;</value>
        [DataMember(Name = "creditorAgentType", EmitDefaultValue = false)]
        public string CreditorAgentType { get; set; }

        /// <summary>
        /// Reference to the financial institution
        /// </summary>
        /// <value>Reference to the financial institution</value>
        [DataMember(Name = "creditorAgent", EmitDefaultValue = false)]
        public string CreditorAgent { get; set; }

        /// <summary>
        /// Type of payee&#39;s account reference, currently must be &lt;code&gt;IBAN&lt;/code&gt;
        /// </summary>
        /// <value>Type of payee&#39;s account reference, currently must be &lt;code&gt;IBAN&lt;/code&gt;</value>
        [DataMember(Name = "creditorAccountReferenceType", IsRequired = true, EmitDefaultValue = false)]
        public string CreditorAccountReferenceType { get; set; }

        /// <summary>
        /// Financial institution&#39;s internal reference for the payee&#39;s account
        /// </summary>
        /// <value>Financial institution&#39;s internal reference for the payee&#39;s account</value>
        [DataMember(Name = "creditorAccountReference", IsRequired = true, EmitDefaultValue = false)]
        public string CreditorAccountReference { get; set; }

        /// <summary>
        /// Type of payer account reference, currently must be &lt;code&gt;IBAN&lt;/code&gt;
        /// </summary>
        /// <value>Type of payer account reference, currently must be &lt;code&gt;IBAN&lt;/code&gt;</value>
        [DataMember(Name = "debtorAccountReferenceType", IsRequired = false, EmitDefaultValue = false)]
        public string DebtorAccountReferenceType { get; set; }

        /// <summary>
        /// Financial institution&#39;s internal reference for the payer account
        /// </summary>
        /// <value>Financial institution&#39;s internal reference for the payer account</value>
        [DataMember(Name = "debtorAccountReference", IsRequired = false, EmitDefaultValue = false)]
        public string DebtorAccountReference { get; set; }

        /// <summary>
        /// Amount of the payment request.
        /// </summary>
        /// <value>Amount of the payment request.</value>
        [DataMember(Name = "amount", IsRequired = true, EmitDefaultValue = false)]
        public decimal Amount { get; set; }

        /// <summary>
        /// TBD
        /// </summary>
        /// <value>TBD</value>
        [DataMember(Name = "endToEndId", EmitDefaultValue = true)]
        public string EndToEndId { get; set; }

        /// <summary>
        /// When the payment request is fully closed
        /// </summary>
        /// <value>When the payment request is fully closed</value>
        [DataMember(Name = "closedAt", EmitDefaultValue = false)]
        public string ClosedAt { get; set; }

        /// <summary>
        /// When the payment request was signed
        /// </summary>
        /// <value>When the payment request was signed</value>
        [DataMember(Name = "signedAt", EmitDefaultValue = false)]
        public string SignedAt { get; set; }

        /// <summary>
        /// When the payment request is handled where should the user be redirected to
        /// </summary>
        /// <value>When the payment request is handled where should the user be redirected to</value>
        [DataMember(Name = "redirectUri", EmitDefaultValue = false)]
        public string RedirectUri { get; set; }

        /// <summary>
        /// Short string representation.
        /// </summary>
        /// <returns>Short string representation</returns>
        public override string ToString() => $"To {CreditorName} ({Amount} {Currency})";
    }

    /// <inheritdoc />
    public class PaymentRequestRequestInitiation : PaymentRequest
    {
    }

    /// <inheritdoc cref="PaymentRequest" />
    [DataContract]
    public class PaymentRequestResponse : PaymentRequest, IIdentified<Guid>
    {
        /// <summary>
        /// URI to redirect to from your customer frontend to conduct the authorization flow.
        /// </summary>
        /// <value>URI to redirect to from your customer frontend to conduct the authorization flow.</value>
        [DataMember(Name = "signingUri", EmitDefaultValue = false)]
        public string SigningUri { get; set; }

        /// <summary>
        /// URI to redirect to from your customer frontend to conduct the authorization flow.
        /// </summary>
        /// <value>URI to redirect to from your customer frontend to conduct the authorization flow.</value>
        public Uri SigningRedirect => string.IsNullOrWhiteSpace(SigningUri)
            ? null
            : new Uri(SigningUri);

        /// <summary>
        /// URI to redirect to from your customer frontend to conduct the authorization flow.
        /// </summary>
        /// <value>URI to redirect to from your customer frontend to conduct the authorization flow.</value>
        public Uri Redirect => string.IsNullOrWhiteSpace(RedirectUri)
            ? null
            : new Uri(RedirectUri);

        /// <inheritdoc />
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }
    }

    /// <summary>
    /// URI to redirect to from your customer frontend to conduct the authorization flow.
    /// </summary>
    [DataContract]
    public class PaymentRequestLinks
    {
        /// <summary>
        /// URI to redirect to from your customer frontend to conduct the authorization flow.
        /// </summary>
        [DataMember(Name = "signingRedirect", EmitDefaultValue = false)]
        public string SigningRedirectString { get; set; }

        /// <summary>
        /// URI to redirect to from your customer frontend to conduct the authorization flow.
        /// </summary>
        public Uri SigningRedirect => string.IsNullOrWhiteSpace(SigningRedirectString)
            ? null
            : new Uri(SigningRedirectString);

        /// <summary>
        /// URI to redirect your customer to after a succesful authorization flow.
        /// </summary>
        [DataMember(Name = "redirect", EmitDefaultValue = false)]
        public string RedirectString { get; set; }

        /// <summary>
        /// URI to redirect your customer to after a succesful authorization flow.
        /// </summary>
        public Uri Redirect => string.IsNullOrWhiteSpace(RedirectString)
            ? null
            : new Uri(RedirectString);
    }
}
