using System;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Extensions
{
    /// <summary>
    /// This represents the extension entity for <see cref="OpenApiParameterAttribute"/>.
    /// </summary>
    public static class OpenApiParameterAttributeExtensions
    {
        /// <summary>
        /// Converts <see cref="OpenApiParameterAttribute"/> to <see cref="OpenApiParameter"/>.
        /// </summary>
        /// <param name="attribute"><see cref="OpenApiParameterAttribute"/> instance.</param>
        /// <returns><see cref="OpenApiParameter"/> instance.</returns>
        public static OpenApiParameter ToOpenApiParameter(this OpenApiParameterAttribute attribute)
        {
            attribute.ThrowIfNullOrDefault();

            var typeCode = Type.GetTypeCode(attribute.Type);
            var schema = new OpenApiSchema()
                             {
                                 Type = typeCode.ToDataType(),
                                 Format = typeCode.ToDataFormat()
                             };
            var parameter = new OpenApiParameter()
                                {
                                    Name = attribute.Name,
                                    Description = attribute.Description,
                                    Required = attribute.Required,
                                    In = attribute.In,
                                    Schema = schema
                                };

            if (!string.IsNullOrWhiteSpace(attribute.Summary))
            {
                var summary = new OpenApiString(attribute.Summary);

                parameter.Extensions.Add("x-ms-summary", summary);
            }

            if (attribute.Visibility != OpenApiVisibilityType.Undefined)
            {
                var visibility = new OpenApiString(attribute.Visibility.ToDisplayName());

                parameter.Extensions.Add("x-ms-visibility", visibility);
            }

            return parameter;
        }
    }
}