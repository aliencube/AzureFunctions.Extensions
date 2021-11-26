using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.Extensions.Common
{
    /// <summary>
    /// This represents the extensions entity for any payload.
    /// </summary>
    public static class PayloadExtensions
    {
        /// <summary>
        /// Replaces the placeholder with the given value.
        /// </summary>
        /// <param name="payload"><see cref="PlaceholderReplaceRequest"/> instance.</param>
        /// <returns>Returns the replaced value.</returns>
        public static string ToJson<T>(this T payload)
        {
            if (payload == null)
            {
                return default;
            }

            var result = JsonConvert.SerializeObject(payload);

            return result;
        }

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
