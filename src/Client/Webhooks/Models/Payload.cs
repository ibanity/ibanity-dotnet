using System.Runtime.Serialization;
using Ibanity.Apis.Client.JsonApi;

namespace Ibanity.Apis.Client.Webhooks.Models
{
    /// <summary>
    /// Webhooks body.
    /// </summary>
    /// <typeparam name="TAttributes">Resource attribute type</typeparam>
    /// <typeparam name="TRelationships">Resource relationships type</typeparam>
    [DataContract]
    public abstract class Payload<TAttributes, TRelationships> : Resource<TAttributes, object, TRelationships, object>
    {
    }
}
