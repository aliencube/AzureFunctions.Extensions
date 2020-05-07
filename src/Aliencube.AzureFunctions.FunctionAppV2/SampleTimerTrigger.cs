//using Aliencube.AzureFunctions.Extensions.DependencyInjection;
//using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
//using Aliencube.AzureFunctions.FunctionAppCommon.Functions;
//using Aliencube.AzureFunctions.FunctionAppCommon.Functions.FunctionOptions;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Extensions.Logging;
//using System.Threading.Tasks;

//namespace Aliencube.AzureFunctions.FunctionAppV2
//{
//    /// <summary>
//    /// This represents the timer trigger.
//    /// </summary>
//    public static class SampleTimerTrigger
//    {
//        /// <summary>
//        /// Gets the <see cref="IFunctionFactory"/> instance as an IoC container.
//        /// </summary>
//        public static IFunctionFactory Factory { get; set; } = new FunctionFactory<StartUp>();

//        /// <summary>
//        /// Invokes the timer trigger.
//        /// </summary>
//        /// <param name="myTimer"><see cref="TimerInfo"/> instance.</param>
//        /// <param name="collector"><see cref="IAsyncCollector{T}"/> instance.</param>
//        /// <param name="log"><see cref="ILogger"/> instance.</param>
//        /// <returns><see cref="Task"/> instance.</returns>
//        [FunctionName(nameof(Run))]
//        public static async Task Run(
//            [TimerTrigger("0/5 * * * * *")] TimerInfo myTimer,
//            [Queue("output")] IAsyncCollector<string> collector,
//            ILogger log)
//        {
//            var options = new SampleTimerFunctionOptions() { Collector = collector };

//            var result = await Factory.Create<ISampleTimerFunction, ILogger>(log)
//                                      .InvokeAsync<TimerInfo, bool>(myTimer, options)
//                                      .ConfigureAwait(false);

//            Factory.ResultInvoked = true;
//        }
//    }
//}