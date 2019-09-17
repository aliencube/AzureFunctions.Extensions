using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;
using Aliencube.AzureFunctions.FunctionAppCommon.Functions;
using Aliencube.AzureFunctions.FunctionAppCommon.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Linq;

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
        public static IFunctionFactory Factory { get; set; } = new FunctionFactory<StartUp>();

        /// <summary>
        /// Invokes the HTTP trigger endpoint to get sample.
        /// </summary>
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns><see cref="SampleResponseModel"/> instance.</returns>
        [FunctionName(nameof(GetSample))]
        [OpenApiOperation(operationId: "list", tags: new[] { "sample" }, Summary = "Gets the list of samples", Description = "This gets the list of samples.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "The ID parameter", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "category", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "The category parameter", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "The name query key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "limit", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "The number of samples to return")]
        [OpenApiResponseBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SampleResponseModel), Summary = "Sample response")]
        [OpenApiResponseBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(List<SampleItemModel>), Summary = "Sample response of a List")]
        [OpenApiResponseBody(statusCode: HttpStatusCode.Accepted, contentType: "application/json", bodyType: typeof(IList<SampleResponseModel>), Summary = "Sample response of a IList")]
        [OpenApiResponseBody(statusCode: HttpStatusCode.NonAuthoritativeInformation, contentType: "application/json", bodyType: typeof(SampleResponseModel[]), Summary = "Sample response of a Array")]
        [OpenApiResponseBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Dictionary<string, SampleResponseModel>), Summary = "Sample response of a Dictionary")]
        [OpenApiResponseBody(statusCode: HttpStatusCode.Unauthorized, contentType: "application/json", bodyType: typeof(IDictionary<string, SampleResponseModel>), Summary = "Sample response of a IDictionary")]
        [OpenApiResponseBody(statusCode: HttpStatusCode.Forbidden, contentType: "application/json", bodyType: typeof(JObject), Summary = "Sample response of a JObject")]
        [OpenApiResponseBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(JToken), Summary = "Sample response of a JToken")]
        [OpenApiResponseBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(List<string>), Summary = "Sample response of a List")]
        [OpenApiResponseBody(statusCode: HttpStatusCode.NotImplemented, contentType: "application/json", bodyType: typeof(Dictionary<string, int>), Summary = "Sample response of a Dictionary")]
        [OpenApiResponseBody(statusCode: HttpStatusCode.BadGateway, contentType: "application/json", bodyType: typeof(SampleGenericResponseModel<SampleResponseModel>), Summary = "Sample response of a Generic")]
        [OpenApiResponse(statusCode: HttpStatusCode.NoContent)]
        [OpenApiResponse(statusCode: HttpStatusCode.RequestTimeout, Description = "Sample response with only status code", Summary = "Sample summary")]
        public static async Task<IActionResult> GetSample(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "samples/{id:int}/categories/{category:regex(^[a-z]{{3,}}$)}")] HttpRequest req,
            int id,
            string category,
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
        [OpenApiOperation(operationId: "add", tags: new[] { "sample" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SampleRequestModel))]
        [OpenApiResponseBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SampleResponseModel), Summary = "Sample response")]
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