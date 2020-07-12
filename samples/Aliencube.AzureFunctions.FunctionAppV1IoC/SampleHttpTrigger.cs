using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;
using Aliencube.AzureFunctions.FunctionApp.Functions;
using Aliencube.AzureFunctions.FunctionApp.Models;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionAppV1IoC
{
    public static class SampleHttpTrigger
    {
        /// <summary>
        /// Gets the <see cref="IFunctionFactory"/> instance as an IoC container.
        /// </summary>
        public static IFunctionFactory Factory = new FunctionFactory<StartUp>();

        [FunctionName(nameof(SampleHttpTrigger.GetSamples))]
        [OpenApiOperation(operationId: "list", tags: new[] { "sample" }, Summary = "Gets the list of samples", Description = "This gets the list of samples.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<SampleResponseModel>), Summary = "List of the sample responses")]
        public static async Task<HttpResponseMessage> GetSamples(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "samples")] HttpRequestMessage req,
            ILogger log)
        {
            var result = await Factory.Create<IGetSamplesFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequestMessage, HttpResponseMessage>(req)
                                      .ConfigureAwait(false);

            return result;
        }
    }
}
