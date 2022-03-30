namespace Ibanity.Apis.Client.Utils.Logging
{
    public class LoggerFactoryNotNullDecorator : ILoggerFactory
    {
        private readonly ILoggerFactory _underlyingInstance;

        public LoggerFactoryNotNullDecorator(ILoggerFactory underlyingInstance) =>
            _underlyingInstance = underlyingInstance ?? throw new System.ArgumentNullException(nameof(underlyingInstance));

        public ILogger CreateLogger<T>()
        {
            var logger = _underlyingInstance.CreateLogger<T>();

            if (logger == null)
                throw new IbanityConfigurationException("Logger factory returns a null logger");

            return logger;
        }
    }
}
