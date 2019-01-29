using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi;
using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;
using Aliencube.AzureFunctions.FunctionAppV1.Configurations;
using Aliencube.AzureFunctions.FunctionAppV1.Dependencies;
using Aliencube.AzureFunctions.FunctionAppV1.Functions;

using Microsoft.Extensions.DependencyInjection;

namespace Aliencube.AzureFunctions.FunctionAppV1.Modules
{
    public class AppModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddSingleton<AppSettings>();

            services.AddTransient<IDocumentHelper, DocumentHelper>();
            services.AddTransient<IDocument, Document>();
            services.AddTransient<ISwaggerUI, SwaggerUI>();

            services.AddTransient<ISampleHttpFunction, SampleHttpFunction>();
            services.AddTransient<ISampleTimerFunction, SampleTimerFunction>();
            services.AddTransient<IRenderOpeApiDocumentFunction, RenderOpeApiDocumentFunction>();
            services.AddTransient<IRenderOpeApiUiFunction, RenderOpeApiUiFunction>();

            services.AddTransient<IMyDependency, MyDependency>();
        }
    }
}