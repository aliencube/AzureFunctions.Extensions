namespace Aliencube.AzureFunctions.Tests.Fakes
{
    /// <summary>
    /// This represents the fake class entity.
    /// </summary>
    public class FakeClass : IFakeInterface
    {
        /// <inheritdoc />
        public bool DoSomething(bool input)
        {
            return input;
        }
    }
}
