namespace Ibanity.Apis.Client.Utils
{
    public class Identified<T> where T : struct
    {
        public T Id { get; set; }
    }
}
