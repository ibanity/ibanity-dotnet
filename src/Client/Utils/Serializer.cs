using System;
using Newtonsoft.Json;

namespace Ibanity.Apis.Client.Utils
{
    public class JsonSerializer : ISerializer<string>
    {
        public string Serialize<T>(T value) =>
            JsonConvert.SerializeObject(value);

        public T Deserialize<T>(string value) =>
            JsonConvert.DeserializeObject<T>(value ?? throw new ArgumentNullException(nameof(value)));
    }

    public interface ISerializer<U>
    {
        U Serialize<T>(T value);
        T Deserialize<T>(U value);
    }
}
