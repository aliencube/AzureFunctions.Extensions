#if NETFRAMEWORK
using System;
using System.Net;
using System.Net.Http;
#endif

using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionApp.Services;

#if !NETFRAMEWORK
using Microsoft.AspNetCore.Mvc;
#endif

using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionApp.Functions
{
    /// <summary>
    /// This represents the function entity for sample HTTP trigger.
    /// </summary>
    public class GetDummiesFunction : FunctionBase<ILogger>, IGetDummiesFunction
    {
        private readonly IDummyHttpService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDummiesFunction"/> class.
        /// </summary>
        /// <param name="dependency"><see cref="IDummyHttpService"/> instance.</param>
        public GetDummiesFunction(IDummyHttpService service)
        {
            this._service = service;
        }

        /// <inheritdoc />
        public override async Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null)
        {
            this.Log.LogInformation("C# HTTP trigger function processed a request.");

            var content = await this._service.GetDummies().ConfigureAwait(false);
#if NETFRAMEWORK
            var req = input as HttpRequestMessage;
            var result = req.CreateResponse(HttpStatusCode.OK, content);

            return (TOutput)Convert.ChangeType(result, typeof(TOutput));
#elif NETSTANDARD
            var result = new OkObjectResult(content);

            return (TOutput)(IActionResult)result;
#endif
        }
    }
}
