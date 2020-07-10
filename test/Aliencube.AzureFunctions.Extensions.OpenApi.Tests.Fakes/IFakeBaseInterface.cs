namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Fakes
{
    public interface IFakeBaseInterface
    {
        string Name { get; set; }
        string City { get; set; }

        /// <summary>
        /// Does something.
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <returns>Output value.</returns>
        bool DoSomething(bool input);
    }
}