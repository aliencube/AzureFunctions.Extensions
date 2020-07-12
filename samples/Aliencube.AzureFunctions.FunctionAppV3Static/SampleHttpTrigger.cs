using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;
using Aliencube.AzureFunctions.FunctionApp.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionAppV3Static
{
    public static class SampleHttpTrigger
    {
        [FunctionName(nameof(SampleHttpTrigger.GetSamples))]
        [OpenApiOperation(operationId: "list", tags: new[] { "sample" }, Summary = "Gets the list of samples", Description = "This gets the list of samples.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<SampleResponseModel>), Summary = "List of the sample responses")]
        public static async Task<IActionResult> GetSamples(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "samples")] HttpRequest req,
            ILogger log)
        {
            var content = new List<SampleResponseModel>()
            {
                new SampleResponseModel(),
            };
            var result = new OkObjectResult(content);

            return await Task.FromResult(result).ConfigureAwait(false);
        }
    }
}
