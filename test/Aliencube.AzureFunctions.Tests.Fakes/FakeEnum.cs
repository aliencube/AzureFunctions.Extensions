using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

namespace Aliencube.AzureFunctions.Tests.Fakes
{
    /// <summary>
    /// This specifies fake enum values.
    /// </summary>
    public enum FakeEnum
    {
        Value1,

        [Display("lorem")]
        Value2
    }
}
