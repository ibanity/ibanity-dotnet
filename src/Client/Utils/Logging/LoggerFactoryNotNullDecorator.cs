using System;

namespace Ibanity.Apis.Client.Utils.Logging
{
    /// <inheritdoc />
    /// <remarks>Ensure the underlying logger factory doesn't return a <c>null</c> logger.</remarks>
    public class LoggerFactoryNotNullDecorator : ILoggerFactory
    {
        private readonly ILoggerFactory _underlyingInstance;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="underlyingInstance">Actual logger factory</param>
        public LoggerFactoryNotNullDecorator(ILoggerFactory underlyingInstance) =>
            _underlyingInstance = underlyingInstance ?? throw new ArgumentNullException(nameof(underlyingInstance));

        /// <inheritdoc />
        public ILogger CreateLogger<T>()
        {
            var logger = _underlyingInstance.CreateLogger<T>();

            if (logger == null)
                throw new IbanityConfigurationException("Logger factory returns a null logger");

            return logger;
        }
    }
}
