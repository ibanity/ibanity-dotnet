using Newtonsoft.Json;

namespace Ibanity.Apis.Client.Utils
{
    public class JsonSerializer : ISerializer<string>
    {
        public string Serialize<T>(T? value) where T : class =>
            JsonConvert.SerializeObject(value);

        public T? Deserialize<T>(string value) where T : class =>
            JsonConvert.DeserializeObject<T>(value);
    }

    public interface ISerializer<U>
    {
        U Serialize<T>(T? value) where T : class;
        T? Deserialize<T>(U value) where T : class;
    }
}
