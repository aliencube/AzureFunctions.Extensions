using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.Tests.Fakes
{
    /// <summary>
    /// This represents the fake model entity.
    /// </summary>
    public class FakeModel
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string FakeProperty { get; set; }

        /// <summary>
        /// Gets or sets the value 2.
        /// </summary>
        [JsonProperty("anotherFakeProperty")]
        public string FakeProperty2 { get; set; }
    }
}
