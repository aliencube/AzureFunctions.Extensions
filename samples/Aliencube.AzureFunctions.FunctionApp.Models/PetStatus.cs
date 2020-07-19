using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

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
        [Display("available")]
        Available = 1,

        /// <summary>
        /// Identifies as "pending".
        /// </summary>
        [Display("pending")]
        Pending = 2,

        /// <summary>
        /// Identifies as "sold".
        /// </summary>
        [Display("sold")]
        Sold = 3
    }
}
