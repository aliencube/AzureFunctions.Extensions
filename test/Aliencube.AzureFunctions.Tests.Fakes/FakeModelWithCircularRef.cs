namespace Aliencube.AzureFunctions.Tests.Fakes
{
    /// <summary>
    /// This represents the fake model entity.
    /// </summary>
    public class FakeModelWithCircularRef
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string FakeProperty { get; set; }
        public FakeModelWithCircularRefSub SubProperty { get; set; }
    }
}
