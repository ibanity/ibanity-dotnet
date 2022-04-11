namespace Ibanity.Apis.Client.Utils.Logging
{
    /// <inheritdoc />
    /// <remarks>Always returns the same logger.</remarks>
    public class SimpleLoggerFactory : ILoggerFactory
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="logger">Logger to be returned</param>
        public SimpleLoggerFactory(ILogger logger) =>
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));

        /// <inheritdoc />
        public ILogger CreateLogger<T>() => _logger;
    }
}
