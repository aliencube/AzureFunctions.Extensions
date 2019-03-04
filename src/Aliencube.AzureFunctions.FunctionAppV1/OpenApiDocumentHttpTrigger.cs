using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.FunctionAppCommon.Functions;
using Aliencube.AzureFunctions.FunctionAppCommon.Functions.FunctionOptions;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionAppV1
{
    /// <summary>
    /// This represents the HTTP trigger for Open API.
    /// </summary>
    public static class OpenApiDocumentHttpTrigger
    {
        /// <summary>
        /// Gets the <see cref="IFunctionFactory"/> instance as an IoC container.
        /// </summary>
        public static IFunctionFactory Factory { get; set; } = new FunctionFactory<StartUp>();

        /// <summary>
        /// Invokes the HTTP trigger endpoint to get Open API document in JSON.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Open API document in JSON.</returns>
        [FunctionName(nameof(RenderSwaggerJson))]
        [OpenApiIgnore]
        public static async Task<HttpResponseMessage> RenderSwaggerJson(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "swagger.json")] HttpRequestMessage req,
            ILogger log)
        {
            var options = new RenderOpeApiDocumentFunctionOptions("v2", "json", Assembly.GetExecutingAssembly());
            var result = await Factory.Create<IRenderOpeApiDocumentFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequestMessage, HttpResponseMessage>(req, options)
                                      .ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Invokes the HTTP trigger endpoint to get Open API document in YAML.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Open API document in YAML.</returns>
        [FunctionName(nameof(RenderSwaggerYaml))]
        [OpenApiIgnore]
        public static async Task<HttpResponseMessage> RenderSwaggerYaml(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "swagger.yaml")] HttpRequestMessage req,
            ILogger log)
        {
            var options = new RenderOpeApiDocumentFunctionOptions("v2", "yaml", Assembly.GetExecutingAssembly());
            var result = await Factory.Create<IRenderOpeApiDocumentFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequestMessage, HttpResponseMessage>(req, options)
                                      .ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Invokes the HTTP trigger endpoint to get Open API document V2 in JSON.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Open API document in JSON.</returns>
        [FunctionName(nameof(RenderOpenApiV2Json))]
        [OpenApiIgnore]
        public static async Task<HttpResponseMessage> RenderOpenApiV2Json(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "openapi/v2.json")] HttpRequestMessage req,
            ILogger log)
        {
            var options = new RenderOpeApiDocumentFunctionOptions("v2", "json", Assembly.GetExecutingAssembly());
            var result = await Factory.Create<IRenderOpeApiDocumentFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequestMessage, HttpResponseMessage>(req, options)
                                      .ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Invokes the HTTP trigger endpoint to get Open API document V2 in YAML.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Open API document in YAML.</returns>
        [FunctionName(nameof(RenderOpenApiV2Yaml))]
        [OpenApiIgnore]
        public static async Task<HttpResponseMessage> RenderOpenApiV2Yaml(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "openapi/v2.yaml")] HttpRequestMessage req,
            ILogger log)
        {
            var options = new RenderOpeApiDocumentFunctionOptions("v2", "yaml", Assembly.GetExecutingAssembly());
            var result = await Factory.Create<IRenderOpeApiDocumentFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequestMessage, HttpResponseMessage>(req, options)
                                      .ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Invokes the HTTP trigger endpoint to get Open API document V3 in JSON.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Open API document in JSON.</returns>
        [FunctionName(nameof(RenderOpenApiV3Json))]
        [OpenApiIgnore]
        public static async Task<HttpResponseMessage> RenderOpenApiV3Json(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "openapi/v3.json")] HttpRequestMessage req,
            ILogger log)
        {
            var options = new RenderOpeApiDocumentFunctionOptions("v3", "json", Assembly.GetExecutingAssembly());
            var result = await Factory.Create<IRenderOpeApiDocumentFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequestMessage, HttpResponseMessage>(req, options)
                                      .ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Invokes the HTTP trigger endpoint to get Open API document V3 in YAML.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Open API document in YAML.</returns>
        [FunctionName(nameof(RenderOpenApiV3Yaml))]
        [OpenApiIgnore]
        public static async Task<HttpResponseMessage> RenderOpenApiV3Yaml(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "openapi/v3.yaml")] HttpRequestMessage req,
            ILogger log)
        {
            var options = new RenderOpeApiDocumentFunctionOptions("v3", "yaml", Assembly.GetExecutingAssembly());
            var result = await Factory.Create<IRenderOpeApiDocumentFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequestMessage, HttpResponseMessage>(req, options)
                                      .ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Invokes the HTTP trigger endpoint to render Swagger UI in HTML.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Swagger UI in HTML.</returns>
        [FunctionName(nameof(RenderSwaggerUI))]
        [OpenApiIgnore]
        public static async Task<HttpResponseMessage> RenderSwaggerUI(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "swagger/ui")] HttpRequestMessage req,
            ILogger log)
        {
            var options = new RenderSwaggerUIFunctionOptions();
            var result = await Factory.Create<IRenderSwaggerUIFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequestMessage, HttpResponseMessage>(req, options)
                                      .ConfigureAwait(false);

            return result;
        }
    }
}