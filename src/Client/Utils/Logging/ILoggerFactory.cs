namespace Ibanity.Apis.Client.Utils.Logging
{
    /// <summary>
    /// Create an new logger instance.
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Create an new logger instance.
        /// </summary>
        /// <typeparam name="T">Type used as logger name</typeparam>
        /// <returns>A ready to use logger instance</returns>
        ILogger CreateLogger<T>();
    }
}
