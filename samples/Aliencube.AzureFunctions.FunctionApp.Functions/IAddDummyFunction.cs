using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionApp.Functions
{
    /// <summary>
    /// This provides interfaces to <see cref="AddDummyFunction"/>.
    /// </summary>
    public interface IAddDummyFunction : IFunction<ILogger>
    {
    }
}
