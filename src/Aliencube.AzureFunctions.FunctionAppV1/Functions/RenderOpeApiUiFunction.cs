using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;

using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionAppV1.Functions
{
    public class RenderOpeApiUiFunction : FunctionBase<ILogger>, IRenderOpeApiUiFunction
    {
        private readonly ISwaggerUI _ui;

        public RenderOpeApiUiFunction(ISwaggerUI ui)
        {
            this._ui = ui;
        }

        public override async Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null)
        {
            this.Log.LogInformation("C# HTTP trigger function processed a request.");

            var req = input as HttpRequestMessage;

            var result = await this._ui.RenderAsync("swagger.json").ConfigureAwait(false);

            return (TOutput)Convert.ChangeType(req.CreateResponse(HttpStatusCode.OK, result, "text/html"), typeof(TOutput));
        }
    }
}