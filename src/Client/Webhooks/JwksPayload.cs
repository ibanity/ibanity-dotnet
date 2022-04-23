using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks
{
    /// <summary>
    /// A set of JWKs.
    /// </summary>
    [DataContract]
    public class JsonWebKeySet
    {
        /// <summary>
        /// An array of JWKs.
        /// </summary>
        [DataMember(Name = "keys", EmitDefaultValue = false)]
        public JsonWebKey[] Keys { get; set; }
    }

    /// <summary>
    /// A cryptographic key. The members of the object represent properties of the key, including its value.
    /// </summary>
    public class JsonWebKey
    {
        /// <summary>
        /// The specific cryptographic algorithm used with the key.
        /// </summary>
        [DataMember(Name = "alg", EmitDefaultValue = false)]
        public string Algorithm { get; set; }

        /// <summary>
        /// The family of cryptographic algorithms used with the key.
        /// </summary>
        [DataMember(Name = "kty", EmitDefaultValue = false)]
        public string AlgorithmFamily { get; set; }

        /// <summary>
        /// How the key was meant to be used; <c>sig</c> represents the signature.
        /// </summary>
        [DataMember(Name = "use", EmitDefaultValue = false)]
        public string Usage { get; set; }

        /// <summary>
        /// The x.509 certificate chain.
        /// </summary>
        /// <remarks>The first entry in the array is the certificate to use for token verification; the other certificates can be used to verify this first certificate.</remarks>
        [DataMember(Name = "x5c", EmitDefaultValue = false)]
        public string CertificateChain { get; set; }

        /// <summary>
        /// The modulus for the RSA public key.
        /// </summary>
        [DataMember(Name = "n", EmitDefaultValue = false)]
        public string Modulus { get; set; }

        /// <summary>
        /// The exponent for the RSA public key.
        /// </summary>
        [DataMember(Name = "e", EmitDefaultValue = false)]
        public string Exponent { get; set; }

        /// <summary>
        /// The unique identifier for the key.
        /// </summary>
        [DataMember(Name = "kid", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The thumbprint of the x.509 cert (SHA-1 thumbprint).
        /// </summary>
        [DataMember(Name = "x5t", EmitDefaultValue = false)]
        public string Thumbprint { get; set; }

        /// <summary>
        /// Is this <see cref="JsonWebKey" /> active?
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }
    }
}
