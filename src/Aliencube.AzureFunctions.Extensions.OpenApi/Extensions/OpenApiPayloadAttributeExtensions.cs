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
            attribute.ThrowIfNullOrDefault();

            bool isJObject = attribute.BodyType.IsJObject();
            bool isDictionary = attribute.BodyType.IsOpenApiDictionary();
            bool isList = attribute.BodyType.IsOpenApiArray();
            bool isSimpleType = (isDictionary || isList)
                                ? attribute.BodyType.GetOpenApiSubType().IsSimpleType()
                                : attribute.BodyType.IsSimpleType();

            var reference = new OpenApiReference()
                                {
                                    Type = ReferenceType.Schema,
                                    Id = attribute.BodyType.GetOpenApiReferenceId(isDictionary, isList)
                                };

            var schema = new OpenApiSchema() { Reference = reference };

            if (isJObject)
            {
                schema = new OpenApiSchema()
                             {
                                 Type = "object"
                             };
            }
            else if (isDictionary)
            {
                schema = new OpenApiSchema()
                             {
                                 Type = "object",
                                 AdditionalProperties = isSimpleType
                                                        ? attribute.BodyType.GetOpenApiSubType().ToOpenApiSchema()
                                                        : schema
                             };
            }
            else if (isList)
            {
                schema = new OpenApiSchema()
                             {
                                 Type = "array",
                                 Items = isSimpleType
                                         ? attribute.BodyType.GetOpenApiSubType().ToOpenApiSchema()
                                         : schema
                             };
            }
            else if (isSimpleType)
            {
                schema = attribute.BodyType.ToOpenApiSchema();
            }

            var mediaType = new OpenApiMediaType() { Schema = schema };

            return mediaType;
        }
    }
}