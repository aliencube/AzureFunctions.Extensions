using System.Net;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi;
using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;
using Aliencube.AzureFunctions.FunctionAppV2.Configurations;
using Aliencube.AzureFunctions.FunctionAppV2.Functions.FunctionOptions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionAppV2.Functions
{
    /// <summary>
    /// This represents the function entity to render Swagger UI.
    /// </summary>
    public class RenderSwaggerUIFunction : FunctionBase<ILogger>, IRenderSwaggerUIFunction
    {
        private readonly AppSettings _settings;
        private readonly ISwaggerUI _ui;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderSwaggerUIFunction"/> class.
        /// </summary>
        /// <param name="settings"><see cref="AppSettings"/> instance.</param>
        /// <param name="ui"><see cref="ISwaggerUI"/> instance.</param>
        public RenderSwaggerUIFunction(AppSettings settings, ISwaggerUI ui)
        {
            this._settings = settings;
            this._ui = ui;
        }

        /// <inheritdoc />
        public override async Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null)
        {
            this.Log.LogInformation("C# HTTP trigger function processed a request.");

            var req = input as HttpRequest;
            var opt = options as RenderSwaggerUIFunctionOptions;

            var result = await this._ui
                                   .AddMetadata(this._settings.OpenApiInfo)
                                   .AddServer(req, this._settings.HttpSettings.RoutePrefix)
                                   .BuildAsync(typeof(SwaggerUI).Assembly)
                                   .RenderAsync(opt.Endpoint, this._settings.SwaggerAuthKey)
                                   .ConfigureAwait(false);

            return (TOutput)(IActionResult)new ContentResult() { Content = result, ContentType = "text/html", StatusCode = (int)HttpStatusCode.OK };
        }
    }
}