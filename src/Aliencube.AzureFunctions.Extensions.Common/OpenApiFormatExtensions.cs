using System;

using Microsoft.OpenApi;

namespace Aliencube.AzureFunctions.Extensions.Common
{
    /// <summary>
    /// This represents the extension entity for the <see cref="OpenApiFormat"/> class.
    /// </summary>
    public static class OpenApiFormatExtensions
    {
        /// <summary>
        /// Gets the content type.
        /// </summary>
        /// <param name="format"><see cref="OpenApiFormat"/> value.</param>
        /// <returns>The content type.</returns>
        public static string GetContentType(this OpenApiFormat format)
        {
            switch (format)
            {
                case OpenApiFormat.Json:
                    return ContentTypes.ApplicationJson;

                case OpenApiFormat.Yaml:
                    return ContentTypes.TextVndYaml;

                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidOpenApiFormat);
            }
        }
    }
}
