using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;
using Aliencube.AzureFunctions.FunctionApp.Models;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace Aliencube.AzureFunctions.FunctionAppV1Static
{
    public static class SampleHttpTrigger
    {
        [FunctionName(nameof(SampleHttpTrigger.GetSamples))]
        [OpenApiOperation(operationId: "list", tags: new[] { "sample" }, Summary = "Gets the list of samples", Description = "This gets the list of samples.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<SampleResponseModel>), Summary = "List of the sample responses")]
        public static async Task<HttpResponseMessage> GetSamples(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "samples")] HttpRequestMessage req,
            TraceWriter log)
        {
            var content = new List<SampleResponseModel>();
            var result = req.CreateResponse(HttpStatusCode.OK, content);

            return await Task.FromResult(result).ConfigureAwait(false);
        }
    }
}
