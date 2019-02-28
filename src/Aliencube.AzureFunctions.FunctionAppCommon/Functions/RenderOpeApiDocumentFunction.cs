#if NET461
using System;
#endif

using System.Net;

#if NET461
using System.Net.Http;
#endif

using System.Reflection;

#if NET461
using System.Text;
#endif

using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
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
#if NET461
            var req = input as HttpRequestMessage;
#elif NETSTANDARD2_0
            var req = input as HttpRequest;
#endif
            var opt = options as RenderOpeApiDocumentFunctionOptions;

            var contentType = opt.Format.GetContentType();
            var result = await this._document
                                   .InitialiseDocument()
                                   .AddMetadata(this._settings.OpenApiInfo)
                                   .AddServer(req, this._settings.HttpSettings.RoutePrefix)
                                   .Build(opt.Assembly)
                                   .RenderAsync(opt.Version, opt.Format)
                                   .ConfigureAwait(false);

#if NET461
            var content = new StringContent(result, Encoding.UTF8, contentType);
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };

            return (TOutput)Convert.ChangeType(response, typeof(TOutput));
#elif NETSTANDARD2_0
            var content = new ContentResult()
                              {
                                  Content = result,
                                  ContentType = contentType,
                                  StatusCode = (int)HttpStatusCode.OK
                              };

            return (TOutput)(IActionResult)content;
#endif
        }
    }
}