using System;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

namespace Aliencube.AzureFunctions.FunctionAppV2.Functions.FunctionOptions
{
    /// <summary>
    /// This represents the options entity for <see cref="RenderSwaggerUIFunction"/>.
    /// </summary>
    public class RenderSwaggerUIFunctionOptions : FunctionOptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderOpeApiDocumentFunctionOptions"/> class.
        /// </summary>
        /// <param name="version">Open API version. This MUST be either "v2" or "v3".</param>
        /// <param name="format">Open API document format. This MUST be either "json" or "yaml".</param>
        public RenderSwaggerUIFunctionOptions(string endpoint = "swagger.json")
        {
            this.Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
        }

        /// <summary>
        /// Gets or sets the endpoint of the Swagger document.
        /// </summary>
        public string Endpoint { get; set; }
    }
}