using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi;
using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Configurations;
using Aliencube.AzureFunctions.FunctionAppCommon.Configurations;
using Aliencube.AzureFunctions.FunctionAppCommon.Dependencies;
using Aliencube.AzureFunctions.FunctionAppCommon.Functions;

using Microsoft.Extensions.DependencyInjection;

namespace Aliencube.AzureFunctions.FunctionAppV1
{
    /// <summary>
    /// This represents the module entity to register dependencies.
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