using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;
using Aliencube.AzureFunctions.FunctionAppCommon.Functions;
using Aliencube.AzureFunctions.FunctionAppCommon.Models;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Aliencube.AzureFunctions.FunctionAppV1
{
    /// <summary>
    /// This represents the HTTP trigger.
    /// </summary>
    public static class SampleHttpTrigger
    {
        /// <summary>
        /// Gets the <see cref="IFunctionFactory"/> instance as an IoC container.
        /// </summary>
        public static IFunctionFactory Factory = new FunctionFactory<StartUp>();

        /// <summary>
        /// Invokes the HTTP trigger endpoint to get sample.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns><see cref="SampleResponseModel"/> instance.</returns>
        [FunctionName(nameof(GetSample))]
        [OpenApiOperation("list", "sample", Summary = "Gets the list of samples", Description = "This gets the list of samples.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "The ID parameter", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter("category", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "The category parameter", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter("name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "The name query key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter("limit", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "The number of samples to return")]
        [OpenApiResponseBody(HttpStatusCode.OK, "application/json", typeof(SampleResponseModel), Summary = "Sample response")]
        public static async Task<HttpResponseMessage> GetSample(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "samples/{id:int}/categories/{category:regex(^[a-z]{{3,}}$)}")] HttpRequestMessage req,
            ILogger log)
        {
            var result = await Factory.Create<ISampleHttpFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequestMessage, HttpResponseMessage>(req)
                                      .ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Invokes the HTTP trigger endpoint to create sample.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns><see cref="SampleResponseModel"/> instance.</returns>
        [FunctionName(nameof(PostSample))]
        [OpenApiOperation("add", "sample")]
        [OpenApiRequestBody("application/json", typeof(SampleRequestModel))]
        [OpenApiResponseBody(HttpStatusCode.OK, "application/json", typeof(SampleResponseModel))]
        public static async Task<HttpResponseMessage> PostSample(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "samples")] HttpRequestMessage req,
            ILogger log)
        {
            var result = await Factory.Create<ISampleHttpFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequestMessage, HttpResponseMessage>(req)
                                      .ConfigureAwait(false);

            return result;
        }
    }
}