using Aliencube.AzureFunctions.Extensions.DependencyInjection.Triggers.Abstractions;

using Microsoft.Azure.WebJobs.Host;

namespace Aliencube.AzureFunctions.Tests.Fakes
{
    /// <summary>
    /// This provides interfaces to the <see cref="FakeTriggerWithTraceWriter"/> class.
    /// </summary>
    public interface IFakeTriggerWithTraceWriter : ITrigger<TraceWriter>
    {
    }
}
