using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionApp.Functions
{
    public interface ISampleHttpFunction : IFunction<ILogger>
    {
    }
}