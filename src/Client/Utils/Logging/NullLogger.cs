using System;

namespace Ibanity.Apis.Client.Utils.Logging
{
    public class NullLogger : ILogger
    {
        public static readonly ILogger Instance = new NullLogger();

        private NullLogger() { }

        public bool TraceEnabled => false;
        public void Trace(string message) { }

        public bool DebugEnabled => false;
        public void Debug(string message) { }

        public bool InfoEnabled => false;
        public void Info(string message) { }

        public bool WarnEnabled => false;
        public void Warn(string message) { }
        public void Warn(string message, Exception exception) { }

        public bool ErrorEnabled => false;
        public void Error(string message) { }
        public void Error(string message, Exception exception) { }

        public bool FatalEnabled => false;
        public void Fatal(string message) { }
        public void Fatal(string message, Exception exception) { }
    }
}
