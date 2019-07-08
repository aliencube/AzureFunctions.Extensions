using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.FunctionAppCommon.Models
{
    /// <summary>
    /// This represents the item model of a collection.
    /// </summary>
    public class SampleItemModel
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [JsonProperty("lastName")]
        public string LastName { get; set; }
    }
}