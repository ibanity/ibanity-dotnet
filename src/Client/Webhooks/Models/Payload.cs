using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models
{
    /// <summary>
    /// Webhooks body.
    /// </summary>
    [DataContract]
    public abstract class Payload
    {
        internal abstract PayloadData UntypedData { get; }
    }

    /// <summary>
    /// Webhooks body.
    /// </summary>
    /// <typeparam name="T">Payload data type</typeparam>
    [DataContract]
    public class Payload<T> : Payload where T : PayloadData
    {
        /// <summary>
        /// Actual item.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public T Data { get; set; }

        internal override PayloadData UntypedData => Data;
    }

    /// <summary>
    /// Actual payload without attributes and relationships.
    /// </summary>
    [DataContract]
    public class PayloadData : JsonApi.Data
    {
        /// <summary>
        /// Move nested object values at top level.
        /// </summary>
        /// <returns>An object without nested properties</returns>
        public virtual IWebhookEvent Flatten() { throw new NotImplementedException(); }
    }

    /// <summary>
    /// Actual payload with attributes and relationships.
    /// </summary>
    /// <typeparam name="TAttributes">Resource attribute type</typeparam>
    /// <typeparam name="TRelationships">Resource relationships type</typeparam>
    [DataContract]
    public abstract class PayloadData<TAttributes, TRelationships> : PayloadData
    {
        /// <summary>
        /// Resource actual content.
        /// </summary>
        [DataMember(Name = "attributes", EmitDefaultValue = false)]
        public TAttributes Attributes { get; set; }

        /// <summary>
        /// Resource relationships.
        /// </summary>
        [DataMember(Name = "relationships", EmitDefaultValue = false)]
        public TRelationships Relationships { get; set; }
    }
}
