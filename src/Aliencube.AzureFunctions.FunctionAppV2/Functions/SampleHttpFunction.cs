using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionAppV2.Dependencies;
using Aliencube.AzureFunctions.FunctionAppV2.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.FunctionAppV2.Functions
{
    /// <summary>
    /// This represents the function entity for <see cref="SampleHttpTrigger"/>.
    /// </summary>
    public class SampleHttpFunction : FunctionBase<ILogger>, ISampleHttpFunction
    {
        private readonly IMyDependency _dependency;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleHttpFunction"/> class.
        /// </summary>
        /// <param name="dependency"><see cref="IMyDependency"/> instance.</param>
        public SampleHttpFunction(IMyDependency dependency)
        {
            this._dependency = dependency;
        }

        /// <inheritdoc />
        public override async Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null)
        {
            this.Log.LogInformation("C# HTTP trigger function processed a request.");

            var req = input as HttpRequest;
            var request = (SampleRequestModel)null;

            var serialised = await new StreamReader(req.Body).ReadToEndAsync().ConfigureAwait(false);
            if (!string.IsNullOrWhiteSpace(serialised))
            {
                request = JsonConvert.DeserializeObject<SampleRequestModel>(serialised);
            }

            string name = req.Query["name"];

            this._dependency.Name = name;

            this.Log.LogInformation($"input: {this._dependency.Name}");

            var response = new SampleResponseModel()
                               {
                                   Id = request == null ? Guid.NewGuid() : Guid.Parse(request.Id),
                                   Name = string.IsNullOrWhiteSpace(name) ? "Sample" : name,
                                   Description = "Ignored",
                                   Sub = new SubSampleResponseModel() { Value = int.MaxValue },
                                   Collection = { { "hello", "world" } },
                                   Items = { { int.MinValue } }
                               };

            return (TOutput)(IActionResult)new ContentResult() { StatusCode = (int)HttpStatusCode.OK, ContentType = "application/json", Content = JsonConvert.SerializeObject(response) };
        }
    }
}