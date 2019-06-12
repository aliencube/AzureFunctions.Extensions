using System.IO;

#if NET461
using System.Net.Http;
#endif

using System.Reflection;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;

#if NETSTANDARD2_0
using Microsoft.AspNetCore.Http;
#endif

using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.Extensions.OpenApi
{
    /// <summary>
    /// This represents the document entity handling Open API document.
    /// </summary>
    public class Document : IDocument
    {
        private readonly IDocumentHelper _helper;

        private OpenApiDocument _document;

        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class.
        /// </summary>
        public Document(IDocumentHelper helper)
        {
            this._helper = helper.ThrowIfNullOrDefault();
        }

        /// <inheritdoc />
        public IDocument InitialiseDocument()
        {
            this._document = new OpenApiDocument()
                                 {
                                     Components = new OpenApiComponents()
                                 };

            return this;
        }

        /// <inheritdoc />
        public IDocument AddMetadata(OpenApiInfo info)
        {
            this._document.Info = info;

            return this;
        }
#if NET461
        /// <inheritdoc />
        public IDocument AddServer(HttpRequestMessage req, string routePrefix)
        {
            var prefix = string.IsNullOrWhiteSpace(routePrefix) ? string.Empty : $"/{routePrefix}";
            var baseUrl = $"{req.RequestUri.Scheme}://{req.RequestUri.Authority}{prefix}";

            this._document.Servers.Add(new OpenApiServer { Url = baseUrl });

            return this;
        }
#elif NETSTANDARD2_0
        /// <inheritdoc />
        public IDocument AddServer(HttpRequest req, string routePrefix)
        {
            var prefix = string.IsNullOrWhiteSpace(routePrefix) ? string.Empty : $"/{routePrefix}";
            var baseUrl = $"{req.Scheme}://{req.Host}{prefix}";

            this._document.Servers.Add(new OpenApiServer { Url = baseUrl });

            return this;
        }
#endif
        /// <inheritdoc />
        public IDocument Build(Assembly assembly, NamingStrategy namingStrategy = null)
        {
            if (namingStrategy.IsNullOrDefault())
            {
                namingStrategy = new DefaultNamingStrategy();
            }

            var paths = new OpenApiPaths();

            var methods = this._helper.GetHttpTriggerMethods(assembly);
            foreach (var method in methods)
            {
                var trigger = this._helper.GetHttpTriggerAttribute(method);
                if (trigger.IsNullOrDefault())
                {
                    continue;
                }

                var function = this._helper.GetFunctionNameAttribute(method);
                if (function.IsNullOrDefault())
                {
                    continue;
                }

                var path = this._helper.GetHttpEndpoint(function, trigger);
                if (path.IsNullOrWhiteSpace())
                {
                    continue;
                }

                var verb = this._helper.GetHttpVerb(trigger);

                var item = this._helper.GetOpenApiPath(path, paths);
                var operations = item.Operations;

                var operation = this._helper.GetOpenApiOperation(method, function, verb);
                if (operation.IsNullOrDefault())
                {
                    continue;
                }

                operation.Parameters = this._helper.GetOpenApiParameters(method, trigger);
                operation.RequestBody = this._helper.GetOpenApiRequestBody(method);
                operation.Responses = this._helper.GetOpenApiResponseBody(method);

                operations[verb] = operation;
                item.Operations = operations;

                paths[path] = item;
            }

            this._document.Paths = paths;
            this._document.Components.Schemas = this._helper.GetOpenApiSchemas(methods, namingStrategy);
            this._document.Components.SecuritySchemes = this._helper.GetOpenApiSecuritySchemes();

            return this;
        }

        /// <inheritdoc />
        public async Task<string> RenderAsync(OpenApiSpecVersion version, OpenApiFormat format)
        {
            var result = await Task.Factory
                                   .StartNew(() => this.Render(version, format))
                                   .ConfigureAwait(false);

            return result;
        }

        private string Render(OpenApiSpecVersion version, OpenApiFormat format)
        {
            using (var sw = new StringWriter())
            {
                this._document.Serialise(sw, version, format);

                return sw.ToString();
            }
        }
    }
}