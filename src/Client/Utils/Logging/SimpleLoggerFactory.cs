namespace Ibanity.Apis.Client.Utils.Logging
{
    public class SimpleLoggerFactory : ILoggerFactory
    {
        private readonly ILogger _logger;

        public SimpleLoggerFactory(ILogger logger) =>
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));

        public ILogger CreateLogger<T>() => _logger;
    }
}
