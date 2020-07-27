using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Fakes
{
    /// <summary>
    /// This specifies fake enum values as string.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FakeStringEnum
    {
        [Display("lorem")]
        StringValue1,

        [Display("ipsum")]
        StringValue2
    }
}
