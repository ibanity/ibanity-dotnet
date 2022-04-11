using System;

namespace Ibanity.Apis.Client.Utils
{
    /// <inheritdoc />
    public class Clock : IClock
    {
        /// <inheritdoc />
        public DateTimeOffset Now => DateTimeOffset.Now;
    }

    /// <summary>
    /// Allow to get current date and time.
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// Current date and time.
        /// </summary>
        DateTimeOffset Now { get; }
    }
}
