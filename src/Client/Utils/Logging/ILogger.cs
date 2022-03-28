using System;

namespace Ibanity.Apis.Client.Utils.Logging
{
    public interface ILogger
    {
        bool TraceEnabled { get; }
        void Trace(string message);

        bool DebugEnabled { get; }
        void Debug(string message);

        bool InfoEnabled { get; }
        void Info(string message);

        bool WarnEnabled { get; }
        void Warn(string message);
        void Warn(string message, Exception exception);

        bool ErrorEnabled { get; }
        void Error(string message);
        void Error(string message, Exception exception);

        bool FatalEnabled { get; }
        void Fatal(string message);
        void Fatal(string message, Exception exception);
    }
}
