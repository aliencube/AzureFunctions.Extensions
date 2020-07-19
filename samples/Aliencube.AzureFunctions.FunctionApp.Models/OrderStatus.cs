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
        Placed = 1,

        /// <summary>
        /// Identifies as "approved".
        /// </summary>
        Approved = 2,

        /// <summary>
        /// Identifies as "delivered".
        /// </summary>
        Delivered = 3
    }
}
