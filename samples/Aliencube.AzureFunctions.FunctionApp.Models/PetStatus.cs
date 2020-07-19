using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aliencube.AzureFunctions.FunctionApp.Models
{
    /// <summary>
    /// This specifices the pet status.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PetStatus
    {
        /// <summary>
        /// Identifies as "available".
        /// </summary>
        Available = 1,

        /// <summary>
        /// Identifies as "pending".
        /// </summary>
        Pending = 2,

        /// <summary>
        /// Identifies as "sold".
        /// </summary>
        Sold = 3
    }
}
