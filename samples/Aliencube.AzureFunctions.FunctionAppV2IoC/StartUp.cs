using Aliencube.AzureFunctions.FunctionApp.Services;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Aliencube.AzureFunctions.FunctionAppV2IoC.StartUp))]
namespace Aliencube.AzureFunctions.FunctionAppV2IoC
{
    /// <summary>
    /// This represents the entity to be invoked during the runtime startup.
    /// </summary>
    public class StartUp : FunctionsStartup
    {
        /// <inheritdoc />
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IDummyHttpService, DummyHttpService>();
        }
    }
}
