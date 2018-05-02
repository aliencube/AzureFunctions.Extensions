using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionApp.Functions;
using Aliencube.AzureFunctions.FunctionApp.Functions.FunctionOptions;
using Aliencube.AzureFunctions.FunctionApp.Modules;

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionApp
{
    public static class SampleTimerTrigger
    {
        public static IFunctionFactory Factory = new FunctionFactory(typeof(AppModule));

        [FunctionName("SampleTimerTrigger")]
        public static async Task Run([TimerTrigger("0/5 * * * * *")]TimerInfo myTimer, [Queue("output")]IAsyncCollector<string> collector, ILogger log)
        {
            var options = new SampleTimerFunctionOptions() { Collector = collector };

            var result = await Factory.Create<ISampleTimerFunction, ILogger>(log).InvokeAsync<TimerInfo, bool>(myTimer, options).ConfigureAwait(false);

            Factory.ResultInvoked = result;
        }
    }
}