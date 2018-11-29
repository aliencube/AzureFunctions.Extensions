using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionAppV1.Dependencies;
using Aliencube.AzureFunctions.FunctionAppV1.Functions;

using Microsoft.Extensions.DependencyInjection;

namespace Aliencube.AzureFunctions.FunctionAppV1.Modules
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