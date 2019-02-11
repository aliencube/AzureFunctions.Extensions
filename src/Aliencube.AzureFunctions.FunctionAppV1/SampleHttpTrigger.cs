using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.FunctionAppCommon.Functions;
using Aliencube.AzureFunctions.FunctionAppCommon.Models;
using Aliencube.AzureFunctions.FunctionAppCommon.Modules;

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
        public static IFunctionFactory Factory = new FunctionFactory<AppModule>();

        /// <summary>
        /// Invokes the HTTP trigger endpoint to get sample.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns><see cref="SampleResponseModel"/> instance.</returns>
        [FunctionName(nameof(GetSample))]
        [OpenApiOperation("list", "sample")]
        [OpenApiParameter("name", In = ParameterLocation.Query, Required = true, Type = typeof(string))]
        [OpenApiParameter("limit", In = ParameterLocation.Query, Required = false, Type = typeof(int))]
        [OpenApiResponseBody(HttpStatusCode.OK, "application/json", typeof(SampleResponseModel))]
        public static async Task<HttpResponseMessage> GetSample(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "samples")] HttpRequestMessage req,
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