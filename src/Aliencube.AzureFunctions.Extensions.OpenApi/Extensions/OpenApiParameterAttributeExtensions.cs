using System;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

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
            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

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

            return parameter;
        }
    }
}