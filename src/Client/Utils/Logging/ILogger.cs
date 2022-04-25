using System;

namespace Ibanity.Apis.Client.Utils.Logging
{
    /// <summary>
    /// Logging interface.
    /// </summary>
    /// <remarks>To be implemented by the application using the library.</remarks>
    public interface ILogger
    {
        /// <summary>
        /// Is TRACE level enabled?
        /// </summary>
        bool TraceEnabled { get; }

        /// <summary>
        /// Write TRACE message.
        /// </summary>
        /// <param name="message">Message to write</param>
        void Trace(string message);

        /// <summary>
        /// Is DEBUG level enabled?
        /// </summary>
        bool DebugEnabled { get; }

        /// <summary>
        /// Write DEBUG message.
        /// </summary>
        /// <param name="message">Message to write</param>
        void Debug(string message);

        /// <summary>
        /// Is INFO level enabled?
        /// </summary>
        bool InfoEnabled { get; }

        /// <summary>
        /// Write INFO message.
        /// </summary>
        /// <param name="message">Message to write</param>
        void Info(string message);

        /// <summary>
        /// Is WARN level enabled?
        /// </summary>
        bool WarnEnabled { get; }

        /// <summary>
        /// Write WARN message.
        /// </summary>
        /// <param name="message">Message to write</param>
        void Warn(string message);

        /// <summary>
        /// Write WARN message.
        /// </summary>
        /// <param name="message">Message to write</param>
        /// <param name="exception">Exception that occurred</param>
        void Warn(string message, Exception exception);

        /// <summary>
        /// Is ERROR level enabled?
        /// </summary>
        bool ErrorEnabled { get; }

        /// <summary>
        /// Write ERROR message.
        /// </summary>
        /// <param name="message">Message to write</param>
        void Error(string message);

        /// <summary>
        /// Write ERROR message.
        /// </summary>
        /// <param name="message">Message to write</param>
        /// <param name="exception">Exception that occurred</param>
        void Error(string message, Exception exception);

        /// <summary>
        /// Is FATAL level enabled?
        /// </summary>
        bool FatalEnabled { get; }

        /// <summary>
        /// Write FATAL message.
        /// </summary>
        /// <param name="message">Message to write</param>
        void Fatal(string message);

        /// <summary>
        /// Write FATAL message.
        /// </summary>
        /// <param name="message">Message to write</param>
        /// <param name="exception">Exception that occurred</param>
        void Fatal(string message, Exception exception);
    }
}
