using System;

namespace Ibanity.Apis.Client.Utils.Logging
{
    /// <summary>
    /// This logger does nothing.
    /// </summary>
    /// <remarks>Used as a stub when there isn't any logger configured.</remarks>
    public class NullLogger : ILogger
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static readonly ILogger Instance = new NullLogger();

        private NullLogger() { }

        /// <inheritdoc />
        /// <remarks>Always false.</remarks>
        public bool TraceEnabled => false;

        /// <inheritdoc />
        /// <remarks>Does nothing.</remarks>
        public void Trace(string message) { }

        /// <inheritdoc />
        /// <remarks>Always false.</remarks>
        public bool DebugEnabled => false;

        /// <inheritdoc />
        /// <remarks>Does nothing.</remarks>
        public void Debug(string message) { }

        /// <inheritdoc />
        /// <remarks>Always false.</remarks>
        public bool InfoEnabled => false;

        /// <inheritdoc />
        /// <remarks>Does nothing.</remarks>
        public void Info(string message) { }

        /// <inheritdoc />
        /// <remarks>Always false.</remarks>
        public bool WarnEnabled => false;

        /// <inheritdoc />
        /// <remarks>Does nothing.</remarks>
        public void Warn(string message) { }

        /// <inheritdoc />
        /// <remarks>Does nothing.</remarks>
        public void Warn(string message, Exception exception) { }

        /// <inheritdoc />
        /// <remarks>Always false.</remarks>
        public bool ErrorEnabled => false;

        /// <inheritdoc />
        /// <remarks>Does nothing.</remarks>
        public void Error(string message) { }

        /// <inheritdoc />
        /// <remarks>Does nothing.</remarks>
        public void Error(string message, Exception exception) { }

        /// <inheritdoc />
        /// <remarks>Always false.</remarks>
        public bool FatalEnabled => false;

        /// <inheritdoc />
        /// <remarks>Does nothing.</remarks>
        public void Fatal(string message) { }

        /// <inheritdoc />
        /// <remarks>Does nothing.</remarks>
        public void Fatal(string message, Exception exception) { }
    }
}
