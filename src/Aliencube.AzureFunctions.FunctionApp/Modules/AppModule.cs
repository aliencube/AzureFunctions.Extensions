using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionApp.Dependencies;
using Aliencube.AzureFunctions.FunctionApp.Functions;

using Microsoft.Extensions.DependencyInjection;

namespace Aliencube.AzureFunctions.FunctionApp.Modules
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