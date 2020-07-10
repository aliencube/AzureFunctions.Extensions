using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests.Fakes
{
    /// <summary>
    /// This provides interfaces to the <see cref="FakeFunctionWithILogger"/> class.
    /// </summary>
    public interface IFakeFunctionWithILogger : IFunction<ILogger>
    {
    }
}
