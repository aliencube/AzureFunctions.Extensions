using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Fakes
{
    /// <summary>
    /// This specifies fake enum values.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FakeEnum
    {
        Value1,

        [Display("lorem")]
        Value2
    }
}
