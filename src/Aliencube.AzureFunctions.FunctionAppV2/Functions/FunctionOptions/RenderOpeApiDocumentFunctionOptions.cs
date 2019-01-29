using System;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.OpenApi;

namespace Aliencube.AzureFunctions.FunctionAppV2.Functions.FunctionOptions
{
    /// <summary>
    /// This represents the options entity for <see cref="RenderOpeApiDocumentFunction"/>.
    /// </summary>
    public class RenderOpeApiDocumentFunctionOptions : FunctionOptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderOpeApiDocumentFunctionOptions"/> class.
        /// </summary>
        /// <param name="version">Open API version. This MUST be either "v2" or "v3".</param>
        /// <param name="format">Open API document format. This MUST be either "json" or "yaml".</param>
        public RenderOpeApiDocumentFunctionOptions(string version, string format)
        {
            this.Version = this.GetVersion(version);
            this.Format = this.GetFormat(format);
        }

        /// <summary>
        /// Gets or sets the <see cref="OpenApiSpecVersion"/> value.
        /// </summary>
        public OpenApiSpecVersion Version { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="OpenApiFormat"/> value.
        /// </summary>
        public OpenApiFormat Format { get; set; }

        private OpenApiSpecVersion GetVersion(string version)
        {
            if (version.Equals("v2", StringComparison.CurrentCultureIgnoreCase))
            {
                return OpenApiSpecVersion.OpenApi2_0;
            }

            if (version.Equals("v3", StringComparison.CurrentCultureIgnoreCase))
            {
                return OpenApiSpecVersion.OpenApi3_0;
            }

            throw new InvalidOperationException("Invalid Open API version");
        }

        private OpenApiFormat GetFormat(string format)
        {
            return Enum.TryParse<OpenApiFormat>(format, true, out OpenApiFormat result)
                       ? result
                       : throw new InvalidOperationException("Invalid Open API format");
        }
    }
}