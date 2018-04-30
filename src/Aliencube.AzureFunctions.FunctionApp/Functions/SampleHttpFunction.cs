using System.IO;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionApp.Dependencies;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.FunctionApp.Functions
{
    public class SampleHttpFunction : FunctionBase<ILogger>, ISampleHttpFunction
    {
        private readonly IMyDependency _dependency;

        public SampleHttpFunction(IMyDependency dependency)
        {
            this._dependency = dependency;
        }

        public override async Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null)
        {
            this.Log.LogInformation("C# HTTP trigger function processed a request.");

            var req = input as HttpRequest;

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync().ConfigureAwait(false);
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            this._dependency.Name = name;

            this.Log.LogInformation($"input: {this._dependency.Name}");

            return name != null
                ? (TOutput)(IActionResult)new OkObjectResult($"Hello, {name}")
                : (TOutput)(IActionResult)new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}