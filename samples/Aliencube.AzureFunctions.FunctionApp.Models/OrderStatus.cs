using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aliencube.AzureFunctions.FunctionApp.Models
{
    /// <summary>
    /// This specifies the order status.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderStatus
    {
        /// <summary>
        /// Identifies as "placed".
        /// </summary>
        [Display("placed")]
        Placed = 1,

        /// <summary>
        /// Identifies as "approved".
        /// </summary>
        [Display("approved")]
        Approved = 2,

        /// <summary>
        /// Identifies as "delivered".
        /// </summary>
        [Display("delivered")]
        Delivered = 3
    }
}
