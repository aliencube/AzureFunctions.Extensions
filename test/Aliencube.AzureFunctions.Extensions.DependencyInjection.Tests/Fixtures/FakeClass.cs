namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests.Fixtures
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
