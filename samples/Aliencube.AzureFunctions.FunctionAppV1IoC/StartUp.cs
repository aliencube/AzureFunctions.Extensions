using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionApp.Functions;
using Aliencube.AzureFunctions.FunctionApp.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Aliencube.AzureFunctions.FunctionAppV1IoC
{
    /// <summary>
    /// This represents the module entity to register dependencies.
    /// </summary>
    public class StartUp : Module
    {
        /// <inheritdoc />
        public override void Load(IServiceCollection services)
        {
            services.AddTransient<ISampleHttpService, SampleHttpService>();

            services.AddTransient<IGetSamplesFunction, GetSamplesFunction>();
        }
    }
}