using System;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

using Microsoft.OpenApi.Models;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Extensions
{
    /// <summary>
    /// This represents the extension entity for <see cref="OpenApiPayloadAttribute"/>.
    /// </summary>
    public static class OpenApiPayloadAttributeExtensions
    {
        /// <summary>
        /// Converts <see cref="OpenApiPayloadAttribute"/> to <see cref="OpenApiMediaType"/>.
        /// </summary>
        /// <typeparam name="T">Type of payload attribute inheriting <see cref="OpenApiPayloadAttribute"/>.</typeparam>
        /// <param name="attribute">OpenApi payload attribute.</param>
        /// <returns><see cref="OpenApiMediaType"/> instance.</returns>
        public static OpenApiMediaType ToOpenApiMediaType<T>(this T attribute) where T : OpenApiPayloadAttribute
        {
            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            var reference = new OpenApiReference()
                                {
                                    Type = ReferenceType.Schema,
                                    Id = attribute.BodyType.Name
                                };
            var schema = new OpenApiSchema() { Reference = reference };
            var mediaType = new OpenApiMediaType() { Schema = schema };

            return mediaType;
        }
    }
}