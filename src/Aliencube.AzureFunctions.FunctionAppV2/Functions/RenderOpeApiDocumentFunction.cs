using System.Net;
using System.Reflection;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
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
    /// This represents the function entity to render Open API document.
    /// </summary>
    public class RenderOpeApiDocumentFunction : FunctionBase<ILogger>, IRenderOpeApiDocumentFunction
    {
        private readonly AppSettings _settings;
        private readonly IDocument _document;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderOpeApiDocumentFunction"/> class.
        /// </summary>
        /// <param name="settings"><see cref="AppSettings"/> instance.</param>
        /// <param name="document"><see cref="IDocument"/> instance.</param>
        public RenderOpeApiDocumentFunction(AppSettings settings, IDocument document)
        {
            this._settings = settings;
            this._document = document;
        }

        /// <inheritdoc />
        public override async Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null)
        {
            this.Log.LogInformation("C# HTTP trigger function processed a request.");

            var req = input as HttpRequest;
            var opt = options as RenderOpeApiDocumentFunctionOptions;

            var contentType = opt.Format.GetContentType();
            var result = await this._document
                                   .AddMetadata(this._settings.OpenApiInfo)
                                   .AddServer(req, this._settings.HttpSettings.RoutePrefix)
                                   .Build(Assembly.GetExecutingAssembly())
                                   .RenderAsync(opt.Version, opt.Format)
                                   .ConfigureAwait(false);

            return (TOutput)(IActionResult)new ContentResult() { Content = result, ContentType = contentType, StatusCode = (int)HttpStatusCode.OK };
        }
    }
}