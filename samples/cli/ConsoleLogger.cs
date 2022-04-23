using System.Globalization;
using Ibanity.Apis.Client.Utils.Logging;

namespace Ibanity.Apis.Sample.CLI
{
    public class ConsoleLogger : ILogger
    {
        public bool TraceEnabled => true;
        public bool DebugEnabled => true;
        public bool InfoEnabled => true;
        public bool WarnEnabled => true;
        public bool ErrorEnabled => true;
        public bool FatalEnabled => true;

        public void Debug(string message) => Log("Debug", message);
        public void Error(string message) => Log("Error", message);
        public void Error(string message, Exception exception) => Log("Error", message, exception);
        public void Fatal(string message) => Log("Fatal", message);
        public void Fatal(string message, Exception exception) => Log("Fatal", message, exception);
        public void Info(string message) => Log("Info", message);
        public void Trace(string message) => Log("Trace", message);
        public void Warn(string message) => Log("Warn", message);
        public void Warn(string message, Exception exception) => Log("Warn", message, exception);

        private static void Log(string level, string message) =>
            Console.WriteLine($"{DateTimeOffset.UtcNow:r} [{level.ToUpper(CultureInfo.InvariantCulture)}] {message}");

        private static void Log(string level, string message, Exception exception) =>
            Log(level, $"{message} - {exception.Message}{Environment.NewLine}{exception.StackTrace}");
    }
}
