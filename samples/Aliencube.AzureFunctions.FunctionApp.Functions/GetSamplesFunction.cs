#if NET461
using System;
using System.Net;
using System.Net.Http;
#endif

using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionApp.Services;

#if !NET461
using Microsoft.AspNetCore.Mvc;
#endif

using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionApp.Functions
{
    /// <summary>
    /// This represents the function entity for sample HTTP trigger.
    /// </summary>
    public class GetSamplesFunction : FunctionBase<ILogger>, IGetSamplesFunction
    {
        private readonly ISampleHttpService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSamplesFunction"/> class.
        /// </summary>
        /// <param name="dependency"><see cref="ISampleHttpService"/> instance.</param>
        public GetSamplesFunction(ISampleHttpService service)
        {
            this._service = service;
        }

        /// <inheritdoc />
        public override async Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null)
        {
            this.Log.LogInformation("C# HTTP trigger function processed a request.");

            var content = await this._service.GetSamples().ConfigureAwait(false);
#if NET461
            var req = input as HttpRequestMessage;
            var result = req.CreateResponse(HttpStatusCode.OK, content);

            return (TOutput)Convert.ChangeType(result, typeof(TOutput));
#elif NETSTANDARD2_0
            var result = new OkObjectResult(content);

            return (TOutput)(IActionResult)result;
#endif
        }
    }
}
