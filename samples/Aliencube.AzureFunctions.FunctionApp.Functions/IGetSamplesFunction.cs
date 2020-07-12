using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionApp.Functions
{
    /// <summary>
    /// This provides interfaces to <see cref="GetSamplesFunction"/>.
    /// </summary>
    public interface IGetSamplesFunction : IFunction<ILogger>
    {
    }
}
