using System;

namespace Ibanity.Apis.Client.Utils
{
    public class Clock : IClock
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }

    public interface IClock
    {
        DateTimeOffset Now { get; }
    }
}
