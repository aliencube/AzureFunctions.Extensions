using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Core.Tests.Fakes
{
    public class FakeRecursiveModel
    {
        [JsonRequired]
        public string StringValue { get; set; }
        public FakeRecursiveModel RecursiveValue { get; set; }
    }
}
