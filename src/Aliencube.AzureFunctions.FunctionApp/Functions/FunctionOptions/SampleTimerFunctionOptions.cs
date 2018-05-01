using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Azure.WebJobs;

namespace Aliencube.AzureFunctions.FunctionApp.Functions.FunctionOptions
{
    public class SampleTimerFunctionOptions : FunctionOptionsBase
    {
        public IAsyncCollector<string> Collector { get; set; }
    }
}