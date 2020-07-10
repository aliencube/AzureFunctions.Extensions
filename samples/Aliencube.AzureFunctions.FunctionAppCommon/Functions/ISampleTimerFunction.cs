using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionAppCommon.Functions
{
    public interface ISampleTimerFunction : IFunction<ILogger>
    {
    }
}