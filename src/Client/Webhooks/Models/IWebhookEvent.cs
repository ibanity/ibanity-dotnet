namespace Ibanity.Apis.Client.Webhooks
{
    /// <summary>
    /// Webhook event payload.
    /// </summary>
    public interface IWebhookEvent
    {
        /// <summary>
        /// Event type.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Event ID.
        /// </summary>
        string Id { get; }
    }
}
