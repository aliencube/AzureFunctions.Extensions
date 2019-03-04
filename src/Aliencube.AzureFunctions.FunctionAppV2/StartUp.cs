using Aliencube.AzureFunctions.Extensions.OpenApi;
using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;
using Aliencube.AzureFunctions.FunctionAppCommon.Configurations;
using Aliencube.AzureFunctions.FunctionAppCommon.Dependencies;
using Aliencube.AzureFunctions.FunctionAppCommon.Functions;
using Aliencube.AzureFunctions.FunctionAppV2;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(StartUp))]
namespace Aliencube.AzureFunctions.FunctionAppV2
{
    /// <summary>
    /// This represents the entity to be invoked during the runtime startup.
    /// </summary>
    public class StartUp : IWebJobsStartup
    {
        /// <summary>
        /// Configures <see cref="IWebJobsBuilder"/> and prepares dependencies.
        /// </summary>
        /// <param name="builder"><see cref="IWebJobsBuilder"/> instance.</param>
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddSingleton<AppSettings>();

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
