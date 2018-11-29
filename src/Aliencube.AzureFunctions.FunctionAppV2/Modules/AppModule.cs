using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionAppV2.Dependencies;
using Aliencube.AzureFunctions.FunctionAppV2.Functions;

using Microsoft.Extensions.DependencyInjection;

namespace Aliencube.AzureFunctions.FunctionAppV2.Modules
{
    public class AppModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddTransient<ISampleHttpFunction, SampleHttpFunction>();
            services.AddTransient<ISampleTimerFunction, SampleTimerFunction>();

            services.AddTransient<IMyDependency, MyDependency>();
        }
    }
}