using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

#if NET461
using System.Net.Http;
using System.Text;
#endif

using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;

#if !NET461
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
#endif

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;


namespace <# NAMESPACE #>
{
    /// <summary>
    /// This represents the HTTP trigger entity for Open API documents.
    /// </summary>
    public static class OpenApiHttpTrigger
    {
        private readonly static IOpenApiHttpTriggerContext context = new OpenApiHttpTriggerContext();

        /// <summary>
        /// Invokes the HTTP trigger endpoint to get Open API document.
        /// </summary>
#if NET461
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
#else
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
#endif
        /// <param name="extension">File extension representing the document format. This MUST be either "json" or "yaml".</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Open API document in a format of either JSON or YAML.</returns>
        [FunctionName(nameof(OpenApiHttpTrigger.RenderSwaggerDocument))]
        [OpenApiIgnore]
#if NET461
        public static async Task<HttpResponseMessage> RenderSwaggerDocument(
            [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "swagger.{extension}")] HttpRequestMessage req,
#else
        public static async Task<IActionResult> RenderSwaggerDocument(
            [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "swagger.{extension}")] HttpRequest req,
#endif
            string extension,
            ILogger log)
        {
            log.LogInformation($"swagger.{extension} was requested.");

            var assembly1 = context.GetExecutingAssembly();
            log.LogInformation($"executing assembly: {assembly1.Location}");

            var assembly2 = Assembly.GetEntryAssembly();
            log.LogInformation($"entry assembly: {assembly2.Location}");

            var assembly3 = Assembly.GetCallingAssembly();
            log.LogInformation($"calling assembly: {assembly3.Location}");
#if NET461
            var instanceName = req.RequestUri.ParseQueryString()["instance"];
#else
            var instanceName = req.Query["instance"].ToString();
#endif
            var filename = assembly1.Location.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries).Last();
            var assembly4 = Assembly.Load(AssemblyName.GetAssemblyName($"{assembly1.Location.Replace(filename, "").TrimEnd(Path.DirectorySeparatorChar)}{Path.DirectorySeparatorChar}{instanceName}.dll"));
            log.LogInformation($"loaded assembly: {assembly4.FullName}");

            var result = await context.Document
                                      .InitialiseDocument()
                                      .AddMetadata(context.OpenApiInfo)
                                      .AddServer(req, context.HttpSettings.RoutePrefix)
                                      .Build(context.GetExecutingAssembly())
                                      .RenderAsync(context.GetOpenApiSpecVersion("v2"), context.GetOpenApiFormat(extension))
                                      .ConfigureAwait(false);
#if NET461
            var content = new StringContent(result, Encoding.UTF8, context.GetOpenApiFormat(extension).GetContentType());
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };

            return response;
#else
            var content = new ContentResult()
            {
                Content = result,
                ContentType = context.GetOpenApiFormat(extension).GetContentType(),
                StatusCode = (int)HttpStatusCode.OK
            };

            return content;
#endif
        }

        /// <summary>
        /// Invokes the HTTP trigger endpoint to get Open API document.
        /// </summary>
#if NET461
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
#else
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
#endif
        /// <param name="version">Open API document spec version. This MUST be either "v2" or "v3".</param>
        /// <param name="extension">File extension representing the document format. This MUST be either "json" or "yaml".</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Open API document in a format of either JSON or YAML.</returns>
        [FunctionName(nameof(OpenApiHttpTrigger.RenderOpenApiDocument))]
        [OpenApiIgnore]
#if NET461
        public static async Task<HttpResponseMessage> RenderOpenApiDocument(
            [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "openapi/{version}.{extension}")] HttpRequestMessage req,
#else
        public static async Task<IActionResult> RenderOpenApiDocument(
            [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "openapi/{version}.{extension}")] HttpRequest req,
#endif
            string version,
            string extension,
            ILogger log)
        {
            log.LogInformation($"{version}.{extension} was requested.");

            var assembly1 = context.GetExecutingAssembly();
            log.LogInformation($"executing assembly: {assembly1.Location}");

            var assembly2 = Assembly.GetEntryAssembly();
            log.LogInformation($"entry assembly: {assembly2.Location}");

            var assembly3 = Assembly.GetCallingAssembly();
            log.LogInformation($"calling assembly: {assembly3.Location}");
#if NET461
            var instanceName = req.RequestUri.ParseQueryString()["instance"];
#else
            var instanceName = req.Query["instance"].ToString();
#endif
            var filename = assembly1.Location.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries).Last();
            var assembly4 = Assembly.Load(AssemblyName.GetAssemblyName($"{assembly1.Location.Replace(filename, "").TrimEnd(Path.DirectorySeparatorChar)}{Path.DirectorySeparatorChar}{instanceName}.dll"));
            log.LogInformation($"loaded assembly: {assembly4.FullName}");

            var result = await context.Document
                                      .InitialiseDocument()
                                      .AddMetadata(context.OpenApiInfo)
                                      .AddServer(req, context.HttpSettings.RoutePrefix)
                                      .Build(context.GetExecutingAssembly())
                                      .RenderAsync(context.GetOpenApiSpecVersion(version), context.GetOpenApiFormat(extension))
                                      .ConfigureAwait(false);
#if NET461
            var content = new StringContent(result, Encoding.UTF8, context.GetOpenApiFormat(extension).GetContentType());
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };

            return response;
#else
            var content = new ContentResult()
            {
                Content = result,
                ContentType = context.GetOpenApiFormat(extension).GetContentType(),
                StatusCode = (int)HttpStatusCode.OK
            };

            return content;
#endif
        }

        /// <summary>
        /// Invokes the HTTP trigger endpoint to render Swagger UI in HTML.
        /// </summary>
#if NET461
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
#else
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
#endif
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Swagger UI in HTML.</returns>
        [FunctionName(nameof(OpenApiHttpTrigger.RenderSwaggerUI))]
        [OpenApiIgnore]
#if NET461
        public static async Task<HttpResponseMessage> RenderSwaggerUI(
            [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "swagger/ui")] HttpRequestMessage req,
#else
        public static async Task<IActionResult> RenderSwaggerUI(
            [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "swagger/ui")] HttpRequest req,
#endif
            ILogger log)
        {
            log.LogInformation($"SwaggerUI page was requested.");

            var assembly1 = context.GetExecutingAssembly();
            log.LogInformation($"executing assembly: {assembly1.Location}");

            var assembly2 = Assembly.GetEntryAssembly();
            log.LogInformation($"entry assembly: {assembly2.Location}");

            var assembly3 = Assembly.GetCallingAssembly();
            log.LogInformation($"calling assembly: {assembly3.Location}");
#if NET461
            var instanceName = req.RequestUri.ParseQueryString()["instance"];
#else
            var instanceName = req.Query["instance"].ToString();
#endif
            var filename = assembly1.Location.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries).Last();
            var assembly4 = Assembly.Load(AssemblyName.GetAssemblyName($"{assembly1.Location.Replace(filename, "").TrimEnd(Path.DirectorySeparatorChar)}{Path.DirectorySeparatorChar}{instanceName}.dll"));
            log.LogInformation($"loaded assembly: {assembly4.FullName}");

            var result = await context.SwaggerUI
                                      .AddMetadata(context.OpenApiInfo)
                                      .AddServer(req, context.HttpSettings.RoutePrefix)
                                      .BuildAsync()
                                      .RenderAsync("swagger.json", context.GetSwaggerAuthKey())
                                      .ConfigureAwait(false);
#if NET461
            var content = new StringContent(result, Encoding.UTF8, "text/html");
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };

            return response;
#else
            var content = new ContentResult()
            {
                Content = result,
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK
            };

            return content;
#endif
        }
    }
}
