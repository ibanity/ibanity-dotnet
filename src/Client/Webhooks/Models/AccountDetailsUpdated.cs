using System;

namespace Ibanity.Apis.Client.Webhooks.Models
{
    /// <summary>
    /// A webhook payload delivered whenever a synchronization completes which updates details related to an account, such as the balance.
    /// </summary>
    public class AccountDetailsUpdated : Payload<AccountDetailsUpdatedAttributes, AccountDetailsUpdatedRelationships> { }

    /// <summary>
    /// Payload attributes delivered whenever a synchronization completes which updates details related to an account, such as the balance.
    /// </summary>
    public class AccountDetailsUpdatedAttributes
    {
        /// <summary>
        /// When this notification was created.
        /// </summary>
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
        public Relationship Account { get; set; }

        /// <summary>
        /// Details about the associated synchronization.
        /// </summary>
        public Relationship Synchronization { get; set; }
    }
}
