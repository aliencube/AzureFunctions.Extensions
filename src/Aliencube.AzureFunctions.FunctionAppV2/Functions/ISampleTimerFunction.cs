using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionAppV2.Functions
{
    public interface ISampleTimerFunction : IFunction<ILogger>
    {
    }
}