using System;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Extensions;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Triggers.Abstractions;
using Aliencube.AzureFunctions.FunctionAppCommon.Functions;
using Aliencube.AzureFunctions.FunctionAppCommon.Functions.FunctionOptions;

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionAppV2
{
    /// <summary>
    /// This represents the timer trigger.
    /// </summary>
    public class SampleTimerTrigger : TriggerBase<ILogger>
    {
        private readonly ISampleTimerFunction _function;

        public SampleTimerTrigger(ISampleTimerFunction function)
        {
            this._function = function ?? throw new ArgumentNullException(nameof(function));
        }

        /// <summary>
        /// Invokes the timer trigger.
        /// </summary>
        /// <param name="myTimer"><see cref="TimerInfo"/> instance.</param>
        /// <param name="collector"><see cref="IAsyncCollector{T}"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns><see cref="Task"/> instance.</returns>
        [FunctionName(nameof(Run))]
        public async Task Run(
            [TimerTrigger("0/5 * * * * *")] TimerInfo myTimer,
            [Queue("output")] IAsyncCollector<string> collector,
            ILogger log)
        {
            var options = new SampleTimerFunctionOptions() { Collector = collector };

            var result = await this._function
                                   .AddLogger(log)
                                   .InvokeAsync<TimerInfo, bool>(myTimer, options)
                                   .ConfigureAwait(false);

            this.ResultInvoked = true;
        }
    }
}