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

        private const string Message = "Missing CA certificate";

        /// <inheritdoc />
        /// <remarks>Does nothing besides throwing an exception.</remarks>
        public string GetPayloadType(string payload) =>
            throw new IbanityConfigurationException(Message);

        /// <inheritdoc />
        /// <remarks>Does nothing besides throwing an exception.</remarks>
        public T ValidateAndDeserialize<T>(string payload, string signature) =>
            throw new IbanityConfigurationException(Message);
    }
}
