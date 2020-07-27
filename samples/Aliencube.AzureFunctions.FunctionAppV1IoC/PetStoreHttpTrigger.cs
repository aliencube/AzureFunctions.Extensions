using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Enums;
using Aliencube.AzureFunctions.FunctionApp.Models;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Aliencube.AzureFunctions.FunctionAppV1IoC
{
    public static class PetStoreHttpTrigger
    {
        /// <summary>
        /// Gets the <see cref="IFunctionFactory"/> instance as an IoC container.
        /// </summary>
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "")]
        public static IFunctionFactory Factory = new FunctionFactory<StartUp>();

        [FunctionName(nameof(PetStoreHttpTrigger.AddPet))]
        [OpenApiOperation(operationId: "addPet", tags: new[] { "pet" }, Summary = "Add a new pet to the store", Description = "This add a new pet to the store.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Pet), Required = true, Description = "Pet object that needs to be added to the store")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Pet), Summary = "New pet details added", Description = "New pet details added")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.MethodNotAllowed, Summary = "Invalid input", Description = "Invalid input")]
        public static async Task<HttpResponseMessage> AddPet(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "pet")] HttpRequestMessage req,
            ILogger log)
        {
            return await Task.FromResult(req.CreateResponse(HttpStatusCode.OK)).ConfigureAwait(false);
        }

        [FunctionName(nameof(PetStoreHttpTrigger.UpdatePet))]
        [OpenApiOperation(operationId: "updatePet", tags: new[] { "pet" }, Summary = "Update an existing pet", Description = "This updates an existing pet.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Pet), Required = true, Description = "Pet object that needs to be updated to the store")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Pet), Summary = "Pet details updated", Description = "Pet details updated")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid ID supplied", Description = "Invalid ID supplied")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "Pet not found", Description = "Pet not found")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.MethodNotAllowed, Summary = "Validation exception", Description = "Validation exception")]
        public static async Task<HttpResponseMessage> UpdatePet(
            [HttpTrigger(AuthorizationLevel.Function, "PUT", Route = "pet")] HttpRequestMessage req,
            ILogger log)
        {
            return await Task.FromResult(req.CreateResponse(HttpStatusCode.OK)).ConfigureAwait(false);
        }

        [FunctionName(nameof(PetStoreHttpTrigger.FindByStatus))]
        [OpenApiOperation(operationId: "findPetsByStatus", tags: new[] { "pet" }, Summary = "Finds Pets by status", Description = "Multiple status values can be provided with comma separated strings.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "status", In = ParameterLocation.Query, Required = true, Type = typeof(List<PetStatus>), Summary = "Pet status value", Description = "Status values that need to be considered for filter", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Pet>), Summary = "successful operation", Description = "successful operation")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid status value", Description = "Invalid status value")]
        public static async Task<HttpResponseMessage> FindByStatus(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "pet/findByStatus")] HttpRequestMessage req,
            ILogger log)
        {
            return await Task.FromResult(req.CreateResponse(HttpStatusCode.OK)).ConfigureAwait(false);
        }

        [FunctionName(nameof(PetStoreHttpTrigger.FindByTags))]
        [OpenApiOperation(operationId: "findPetsByTags", tags: new[] { "pet" }, Summary = "Finds Pets by tags", Description = "Muliple tags can be provided with comma separated strings.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "tags", In = ParameterLocation.Query, Required = true, Type = typeof(List<string>), Summary = "Tags to filter by", Description = "Tags to filter by", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Pet>), Summary = "successful operation", Description = "successful operation")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid tag value", Description = "Invalid tag value")]
        public static async Task<HttpResponseMessage> FindByTags(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "pet/findByTags")] HttpRequestMessage req,
            ILogger log)
        {
            return await Task.FromResult(req.CreateResponse(HttpStatusCode.OK)).ConfigureAwait(false);
        }

        [FunctionName(nameof(PetStoreHttpTrigger.GetPetById))]
        [OpenApiOperation(operationId: "getPetById", tags: new[] { "pet" }, Summary = "Find pet by ID", Description = "Returns a single pet.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "petId", In = ParameterLocation.Path, Required = true, Type = typeof(long), Summary = "ID of pet to return", Description = "ID of pet to return", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Pet), Summary = "successful operation", Description = "successful operation")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid ID supplied", Description = "Invalid ID supplied")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "Pet not found", Description = "Pet not found")]
        public static async Task<HttpResponseMessage> GetPetById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "pet/{petId}")] HttpRequestMessage req,
            long petId,
            ILogger log)
        {
            return await Task.FromResult(req.CreateResponse(HttpStatusCode.OK)).ConfigureAwait(false);
        }
    }
}
