namespace Ibanity.Apis.Client.Webhooks
{
    /// <summary>
    /// Webhooks service only throwing exception.
    /// </summary>
    /// <remarks>Used when CA certificate isn't configured.</remarks>
    public class UnconfiguredWebhooksService : IWebhooksService
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static readonly IWebhooksService Instance = new UnconfiguredWebhooksService();

        private UnconfiguredWebhooksService() { }

        /// <inheritdoc />
        /// <remarks>Does nothing besides throwing an exception.</remarks>
        public IWebhookEvent ValidateAndDeserialize(string payload, string signature) =>
            throw new IbanityConfigurationException("Missing CA certificate");
    }
}
