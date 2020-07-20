using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.FunctionApp.Models
{
    public class DummyArrayResponseModel
    {
        public string Id { get; set; }

        [JsonRequired]
        public string JsonRequiredValue { get; set; }
    }
}
