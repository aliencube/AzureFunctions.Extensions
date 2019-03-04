using Aliencube.AzureFunctions.Extensions.DependencyInjection.Triggers.Abstractions;

using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.Tests.Fakes
{
    /// <summary>
    /// This provides interfaces to the <see cref="FakeTriggerWithILogger"/> class.
    /// </summary>
    public interface IFakeTriggerWithILogger : ITrigger<ILogger>
    {
    }
}
