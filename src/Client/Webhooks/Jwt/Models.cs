using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Jwt
{
    /// <summary>
    /// Token header.
    /// </summary>
    [DataContract]
    public class Header
    {
        /// <summary>
        /// Signature algorithm.
        /// </summary>
        [DataMember(Name = "alg", EmitDefaultValue = false)]
        public string Algorithm { get; set; }

        /// <summary>
        /// Key ID.
        /// </summary>
        [DataMember(Name = "kid", EmitDefaultValue = false)]
        public string KeyId { get; set; }
    }

    /// <summary>
    /// Token payload.
    /// </summary>
    [DataContract]
    public class Payload
    {
        /// <summary>
        /// Expiration time (seconds since Unix epoch).
        /// </summary>
        [DataMember(Name = "exp", EmitDefaultValue = false)]
        public long ExpirationEpoch { get; set; }

        /// <summary>
        /// Expiration time.
        /// </summary>
        public DateTimeOffset Expiration => DateTimeOffset.FromUnixTimeSeconds(ExpirationEpoch);

        /// <summary>
        /// Issued at (seconds since Unix epoch).
        /// </summary>
        [DataMember(Name = "iat", EmitDefaultValue = false)]
        public long IssuedAtEpoch { get; set; }

        /// <summary>
        /// Issued at.
        /// </summary>
        public DateTimeOffset IssuedAt => DateTimeOffset.FromUnixTimeSeconds(IssuedAtEpoch);

        /// <summary>
        /// Who created and signed this token.
        /// </summary>
        [DataMember(Name = "iss", EmitDefaultValue = false)]
        public string Issuer { get; set; }

        /// <summary>
        /// JWT ID (unique identifier for this token).
        /// </summary>
        [DataMember(Name = "jti", EmitDefaultValue = false)]
        public string Id { get; set; }


    }

    /// <inheritdoc />
    [DataContract]
    public class IbanityPayload : Payload
    {
        /// <summary>
        /// Message digest.
        /// </summary>
        [DataMember(Name = "digest", EmitDefaultValue = false)]
        public string Digest { get; set; }
    }
}
