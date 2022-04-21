using System;
using System.Runtime.Serialization;

namespace Ibanity.Apis.Client.Webhooks.Models
{
    /// <summary>
    /// A webhook payload delivered whenever a synchronization completes which updates details related to an account, such as the balance.
    /// </summary>
    public class AccountDetailsUpdated : PayloadData<AccountDetailsUpdatedAttributes, AccountDetailsUpdatedRelationships> { }

    /// <summary>
    /// Payload attributes delivered whenever a synchronization completes which updates details related to an account, such as the balance.
    /// </summary>
    public class AccountDetailsUpdatedAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }
    }

    /// <summary>
    /// Payload relationships delivered whenever a synchronization completes which updates details related to an account, such as the balance.
    /// </summary>
    public class AccountDetailsUpdatedRelationships
    {
        /// <summary>
        /// Details about the associated account.
        /// </summary>
        [DataMember(Name = "account", EmitDefaultValue = false)]
        public Relationship Account { get; set; }

        /// <summary>
        /// Details about the associated synchronization.
        /// </summary>
        [DataMember(Name = "synchronization", EmitDefaultValue = false)]
        public Relationship Synchronization { get; set; }
    }
}
