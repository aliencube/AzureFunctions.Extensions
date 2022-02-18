using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.Extensions.Common.Tests.Models
{
    public class FakeRequestHeader
    {
        [JsonProperty("x-secret-key")]
        public string? SecretKey { get; set; }
    }
}
