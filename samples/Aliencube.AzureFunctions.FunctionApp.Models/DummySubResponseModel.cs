using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.FunctionApp.Models
{
    public class DummySubResponseModel
    {
        public int Id { get; set; }

        [JsonProperty("CapitalisedJsonRequiredValue", Required = Required.Always)]
        public string JsonRequiredValue { get; set; }
    }
}
