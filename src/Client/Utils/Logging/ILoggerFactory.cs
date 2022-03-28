namespace Ibanity.Apis.Client.Utils.Logging
{
    public interface ILoggerFactory
    {
        ILogger CreateLogger<T>();
    }
}
