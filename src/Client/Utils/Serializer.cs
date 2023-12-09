using System;
using Newtonsoft.Json;

namespace Ibanity.Apis.Client.Utils
{
    /// <summary>
    /// Convert an object to and from a JSON string.
    /// </summary>
    public class JsonSerializer : ISerializer<string>
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            MaxDepth = 128 // Mitigation for https://github.com/advisories/GHSA-5crp-9r3c-p9vr
        };

        /// <summary>
        /// Convert an object to a JSON string.
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="value">Object to transform</param>
        /// <returns>The JSON representation of the argument</returns>
        public string Serialize<T>(T value) =>
            JsonConvert.SerializeObject(value, _settings);

        /// <summary>
        /// Create an object from a JSON string.
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="value">JSON of an object</param>
        /// <returns>An instance matching the JSON value</returns>
        public T Deserialize<T>(string value) =>
            JsonConvert.DeserializeObject<T>(value ?? throw new ArgumentNullException(nameof(value)), _settings);

        /// <summary>
        /// Create an object from a JSON string.
        /// </summary>
        /// <param name="value">JSON of an object</param>
        /// <param name="type">Object type</param>
        /// <returns>An instance matching the JSON value</returns>
        public object Deserialize(string value, Type type) =>
            JsonConvert.DeserializeObject(
                value ?? throw new ArgumentNullException(nameof(value)),
                type ?? throw new ArgumentNullException(nameof(type)),
                _settings);
    }

    /// <summary>
    /// Convert an object to and from a transferable format.
    /// </summary>
    /// <typeparam name="TTransfer">Transferable format type</typeparam>
    public interface ISerializer<TTransfer>
    {
        /// <summary>
        /// Convert an object to a transferable format.
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="value">Object to transform</param>
        /// <returns>Object in transferable format</returns>
        TTransfer Serialize<T>(T value);

        /// <summary>
        /// Create an object from a transferable format.
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="value">Transferable representation of an object</param>
        /// <returns>Deserialized object</returns>
        T Deserialize<T>(TTransfer value);

        /// <summary>
        /// Create an object from a transferable format.
        /// </summary>
        /// <param name="value">Transferable representation of an object</param>
        /// <param name="type">Object type</param>
        /// <returns>Deserialized object</returns>
        object Deserialize(TTransfer value, Type type);
    }
}
