using System;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionAppV1.Functions.FunctionOptions;

using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionAppV1.Functions
{
    public class SampleTimerFunction : FunctionBase<ILogger>, ISampleTimerFunction
    {
        public override async Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null)
        {
            var collector = (options as SampleTimerFunctionOptions).Collector;

            var now = DateTimeOffset.UtcNow;

            this.Log.LogInformation($"C# Timer trigger function executed at: {now}");

            await collector.AddAsync(now.ToString()).ConfigureAwait(false);

            return (TOutput)(object)true;
        }
    }
}