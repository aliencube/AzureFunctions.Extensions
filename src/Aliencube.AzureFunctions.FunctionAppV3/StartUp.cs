using Aliencube.AzureFunctions.Extensions.OpenApi;
using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Configurations;
using Aliencube.AzureFunctions.FunctionAppCommon.Configurations;
using Aliencube.AzureFunctions.FunctionAppCommon.Dependencies;
using Aliencube.AzureFunctions.FunctionAppCommon.Functions;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Aliencube.AzureFunctions.FunctionAppV3.StartUp))]
namespace Aliencube.AzureFunctions.FunctionAppV3
{
    /// <summary>
    /// This represents the entity to be invoked during the runtime startup.
    /// </summary>
    public class StartUp : FunctionsStartup
    {
        /// <inheritdoc />
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<AppSettings>();
            builder.Services.AddSingleton<RouteConstraintFilter, RouteConstraintFilter>();

            builder.Services.AddTransient<IDocumentHelper, DocumentHelper>();
            builder.Services.AddTransient<IDocument, Document>();
            builder.Services.AddTransient<ISwaggerUI, SwaggerUI>();

            builder.Services.AddTransient<ISampleHttpFunction, SampleHttpFunction>();
            builder.Services.AddTransient<ISampleTimerFunction, SampleTimerFunction>();
            builder.Services.AddTransient<IRenderOpeApiDocumentFunction, RenderOpeApiDocumentFunction>();
            builder.Services.AddTransient<IRenderSwaggerUIFunction, RenderSwaggerUIFunction>();

            builder.Services.AddTransient<IMyDependency, MyDependency>();
        }
    }
}
