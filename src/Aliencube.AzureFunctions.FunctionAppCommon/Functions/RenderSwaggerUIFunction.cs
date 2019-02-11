#if NET461
using System;
#endif

using System.Net;
using System.Reflection;

#if NET461
using System.Net.Http;
#endif

#if NET461
using System.Text;
#endif

using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi;
using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;
using Aliencube.AzureFunctions.FunctionAppCommon.Configurations;
using Aliencube.AzureFunctions.FunctionAppCommon.Functions.FunctionOptions;

#if NETSTANDARD2_0
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
#endif

using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionAppCommon.Functions
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
#if NET461
            var req = input as HttpRequestMessage;
#elif NETSTANDARD2_0
            var req = input as HttpRequest;
#endif
            var opt = options as RenderSwaggerUIFunctionOptions;

            var result = await this._ui
                                   .AddMetadata(this._settings.OpenApiInfo)
                                   .AddServer(req, this._settings.HttpSettings.RoutePrefix)
                                   .BuildAsync()
                                   .RenderAsync(opt.Endpoint, this._settings.SwaggerAuthKey)
                                   .ConfigureAwait(false);
#if NET461
            var content = new StringContent(result, Encoding.UTF8, "text/html");
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };

            return (TOutput)Convert.ChangeType(response, typeof(TOutput));
#elif NETSTANDARD2_0
            var content = new ContentResult()
                              {
                                  Content = result,
                                  ContentType = "text/html",
                                  StatusCode = (int)HttpStatusCode.OK
                              };

            return (TOutput)(IActionResult)content;
#endif
        }
    }
}