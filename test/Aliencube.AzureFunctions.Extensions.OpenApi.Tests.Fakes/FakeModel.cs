using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Fakes
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

        /// <summary>
        /// Gets or sets the nullable int value.
        /// </summary>
        public int? NullableInt { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="FakeSubModel"/> instance.
        /// </summary>
        public FakeSubModel SubProperty { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="FakeEnum"/> value.
        /// </summary>
        public FakeEnum EnumProperty { get; set; }

    }
}
