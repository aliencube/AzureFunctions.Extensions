using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi;
using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Configurations;
using Aliencube.AzureFunctions.FunctionAppCommon.Configurations;
using Aliencube.AzureFunctions.FunctionAppCommon.Dependencies;
using Aliencube.AzureFunctions.FunctionAppCommon.Functions;

using Microsoft.Extensions.DependencyInjection;

namespace Aliencube.AzureFunctions.FunctionAppV2
{
    /// <summary>
    /// This represents the entity to be invoked during the runtime startup.
    /// </summary>
    public class StartUp : Module
    {
        /// <inheritdoc />
        public override void Load(IServiceCollection services)
        {
            services.AddSingleton<AppSettings>();
            services.AddSingleton<RouteConstraintFilter, RouteConstraintFilter>();

            services.AddTransient<IDocumentHelper, DocumentHelper>();
            services.AddTransient<IDocument, Document>();
            services.AddTransient<ISwaggerUI, SwaggerUI>();

            services.AddTransient<ISampleHttpFunction, SampleHttpFunction>();
            services.AddTransient<ISampleTimerFunction, SampleTimerFunction>();
            services.AddTransient<IRenderOpeApiDocumentFunction, RenderOpeApiDocumentFunction>();
            services.AddTransient<IRenderSwaggerUIFunction, RenderSwaggerUIFunction>();

            services.AddTransient<IMyDependency, MyDependency>();
        }
    }
}
