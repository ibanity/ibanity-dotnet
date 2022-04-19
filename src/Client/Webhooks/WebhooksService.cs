using Ibanity.Apis.Client.JsonApi;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client
{
    /// <inheritdoc />
    public class WebhooksService : IWebhooksService
    {
        private readonly ISerializer<string> _serializer;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="serializer">To-string serializer</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public WebhooksService(ISerializer<string> serializer) =>
            _serializer = serializer ?? throw new System.ArgumentNullException(nameof(serializer));

        /// <inheritdoc />
        public string GetPayloadType(string payload) =>
            _serializer.Deserialize<Resource<object, object, object, object>>(payload)?.Data?.Type;
    }

    /// <summary>
    /// Allows to validate and deserialize webhook payloads.
    /// </summary>
    public interface IWebhooksService
    {
        /// <summary>
        /// Get event type.
        /// </summary>
        /// <param name="payload">Webhook payload</param>
        /// <returns></returns>
        string GetPayloadType(string payload);
    }
}
