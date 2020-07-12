using Aliencube.AzureFunctions.FunctionApp.Services;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Aliencube.AzureFunctions.FunctionAppV3IoC.StartUp))]
namespace Aliencube.AzureFunctions.FunctionAppV3IoC
{
    /// <summary>
    /// This represents the entity to be invoked during the runtime startup.
    /// </summary>
    public class StartUp : FunctionsStartup
    {
        /// <inheritdoc />
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<ISampleHttpService, SampleHttpService>();
        }
    }
}
