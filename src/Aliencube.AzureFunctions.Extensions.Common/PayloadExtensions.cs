using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.Extensions.Common
{
    /// <summary>
    /// This represents the extensions entity for any payload.
    /// </summary>
    public static class PayloadExtensions
    {
        /// <summary>
        /// Serialises the given payload to JSON string.
        /// </summary>
        /// <typeparam name="T">Type of the payload to serialise.</typeparam>
        /// <param name="payload">Payload object.</param>
        /// <returns>Returns the serialised JSON string value.</returns>
        public static string ToJson<T>(this T payload)
        {
            if (payload == null)
            {
                return default;
            }

            var result = JsonConvert.SerializeObject(payload);

            return result;
        }

        /// <summary>
        /// Deserialises the JSON string value to given type.
        /// </summary>
        /// <typeparam name="T">Type of the payload to deserialise.</typeparam>
        /// <param name="payload">JSON string value.</param>
        /// <returns>Returns the deserialised object.</returns>
        public static T FromJson<T>(this string payload)
        {
            if (string.IsNullOrWhiteSpace(payload))
            {
                return default;
            }

            var result = JsonConvert.DeserializeObject<T>(payload);

            return result;
        }
    }
}
