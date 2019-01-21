using System.Net.Http;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.FunctionAppV1.Functions;
using Aliencube.AzureFunctions.FunctionAppV1.Modules;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionAppV1
{
    public static class OpenApiHttpTrigger
    {
        public static IFunctionFactory Factory = new FunctionFactory<AppModule>();

        [FunctionName(nameof(RenderOpenApiDefinition))]
        [OpenApiIgnore]
        public static async Task<HttpResponseMessage> RenderOpenApiDefinition(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "openapi")]HttpRequestMessage req,
            ILogger log)
        {
            var result = await Factory.Create<IRenderOpeApiDocumentFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequestMessage, HttpResponseMessage>(req)
                                      .ConfigureAwait(false);

            return result;
        }
    }
}