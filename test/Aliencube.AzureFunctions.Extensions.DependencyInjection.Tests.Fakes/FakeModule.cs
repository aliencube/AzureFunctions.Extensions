using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Extensions.DependencyInjection;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests.Fakes
{
    /// <summary>
    /// This represents the module entity for test.
    /// </summary>
    public class FakeModule : Module
    {
        /// <inheritdoc />
        public override void Load(IServiceCollection services)
        {
            services.AddTransient<IFakeInterface, FakeClass>();
            services.AddTransient<IFakeFunctionWithTraceWriter, FakeFunctionWithTraceWriter>();
            services.AddTransient<IFakeFunctionWithILogger, FakeFunctionWithILogger>();
        }
    }
}
