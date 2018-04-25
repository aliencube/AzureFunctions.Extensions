namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests.Fixtures
{
    /// <summary>
    /// This provides interfaces to the <see cref="FakeClass"/> class.
    /// </summary>
    public interface IFakeInterface
    {
        /// <summary>
        /// Does something.
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <returns>Output value.</returns>
        bool DoSomething(bool input);
    }
}
