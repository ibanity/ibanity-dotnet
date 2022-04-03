namespace Ibanity.Apis.Client.Utils
{
    public class Identified<T> : IIdentified<T> where T : struct
    {
        public T Id { get; set; }
    }

    public interface IIdentified<T> where T : struct
    {
        T Id { get; set; }
    }
}
