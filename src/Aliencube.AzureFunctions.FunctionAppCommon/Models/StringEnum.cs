using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aliencube.AzureFunctions.FunctionAppCommon.Models
{
    /// <summary>
    /// This specifies an enum that will be serialized as a string.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StringEnum
    {
        /// <summary>
        /// Identifies "off".
        /// </summary>
        Off = 0,

        /// <summary>
        /// Identifies "on".
        /// </summary>
        On = 1
    }
}