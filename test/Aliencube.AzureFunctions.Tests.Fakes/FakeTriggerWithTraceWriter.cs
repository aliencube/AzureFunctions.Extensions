using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Triggers.Abstractions;

using Microsoft.Azure.WebJobs.Host;

namespace Aliencube.AzureFunctions.Tests.Fakes
{
    /// <summary>
    /// This represents the function entity.
    /// </summary>
    public class FakeTriggerWithTraceWriter : TriggerBase<TraceWriter>
    {
        /// <summary>
        /// Runs the trigger.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <param name="log"><see cref="TraceWriter"/> instance.</param>
        /// <returns>The <see cref="Task"/> instance.</returns>
        public async Task RunAsync(object obj, TraceWriter log)
        {
            await Task.Factory.StartNew(() => { this.ResultInvoked = true; });
        }
    }
}
