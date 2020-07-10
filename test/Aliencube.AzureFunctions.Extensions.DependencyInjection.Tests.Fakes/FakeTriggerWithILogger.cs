using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Triggers.Abstractions;

using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests.Fakes
{
    /// <summary>
    /// This represents the trigger entity.
    /// </summary>
    public class FakeTriggerWithILogger : TriggerBase<ILogger>
    {
        /// <summary>
        /// Runs the trigger.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>The <see cref="Task"/> instance.</returns>
        public async Task RunAsync(object obj, ILogger log)
        {
            await Task.Factory.StartNew(() => { this.ResultInvoked = true; });
        }
    }
}
