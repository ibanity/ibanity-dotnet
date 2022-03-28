namespace Ibanity.Apis.Client.Utils.Logging
{
    public class NullLoggerFactory : ILoggerFactory
    {
        public static readonly ILoggerFactory Instance = new NullLoggerFactory();

        private NullLoggerFactory() { }

        public ILogger CreateLogger<T>() =>
            NullLogger.Instance;
    }
}
