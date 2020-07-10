using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Azure.WebJobs.Host;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests.Fakes
{
    /// <summary>
    /// This provides interfaces to the <see cref="FakeFunctionWithTraceWriter"/> class.
    /// </summary>
    public interface IFakeFunctionWithTraceWriter : IFunction<TraceWriter>
    {
    }
}
