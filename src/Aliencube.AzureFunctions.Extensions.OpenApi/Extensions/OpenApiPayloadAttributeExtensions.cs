using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Lists;
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
            attribute.ThrowIfNullOrDefault();

            bool isList = Generics.IsList(attribute.BodyType);

            var reference = new OpenApiReference()
            {
                Type = ReferenceType.Schema,
                Id = isList ? attribute.BodyType.GetGenericArguments()[0].Name :
                              attribute.BodyType.Name
            };
            var schema = new OpenApiSchema() { Reference = reference };

            if (isList)
            {
                schema = new OpenApiSchema()
                {
                    Type = "array",
                    Items = schema
                };
            }

            var mediaType = new OpenApiMediaType() { Schema = schema };
            return mediaType;
        }
    }
}