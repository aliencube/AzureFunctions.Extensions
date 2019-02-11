using System.Net;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;
using Aliencube.AzureFunctions.FunctionAppCommon.Functions;
using Aliencube.AzureFunctions.FunctionAppCommon.Models;
using Aliencube.AzureFunctions.FunctionAppCommon.Modules;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Aliencube.AzureFunctions.FunctionAppV2
{
    /// <summary>
    /// This represents the HTTP trigger.
    /// </summary>
    public static class SampleHttpTrigger
    {
        /// <summary>
        /// Gets the <see cref="IFunctionFactory"/> instance as an IoC container.
        /// </summary>
        public static IFunctionFactory Factory = new FunctionFactory<AppModule>();

        /// <summary>
        /// Invokes the HTTP trigger endpoint to get sample.
        /// </summary>
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns><see cref="SampleResponseModel"/> instance.</returns>
        [FunctionName(nameof(GetSample))]
        [OpenApiOperation("list", "sample", Summary = "Gets the list of samples", Description = "This gets the list of samples.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter("name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "The name parameter", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter("limit", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "The number of samples to return")]
        [OpenApiResponseBody(HttpStatusCode.OK, "application/json", typeof(SampleResponseModel), Summary = "Sample response")]
        public static async Task<IActionResult> GetSample(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "samples")] HttpRequest req,
            ILogger log)
        {
            var result = await Factory.Create<ISampleHttpFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequest, IActionResult>(req)
                                      .ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Invokes the HTTP trigger endpoint to create sample.
        /// </summary>
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns><see cref="SampleResponseModel"/> instance.</returns>
        [FunctionName(nameof(PostSample))]
        [OpenApiOperation("add", "sample")]
        [OpenApiRequestBody("application/json", typeof(SampleRequestModel))]
        [OpenApiResponseBody(HttpStatusCode.OK, "application/json", typeof(SampleResponseModel), Summary = "Sample response")]
        public static async Task<IActionResult> PostSample(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "samples")] HttpRequest req,
            ILogger log)
        {
            var result = await Factory.Create<ISampleHttpFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequest, IActionResult>(req)
                                      .ConfigureAwait(false);

            return result;
        }
    }
}