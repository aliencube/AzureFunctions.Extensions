using System.Linq;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

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
        /// <param name="namingStrategy"></param>
        /// <returns><see cref="OpenApiParameter"/> instance.</returns>
        public static OpenApiParameter ToOpenApiParameter(this OpenApiParameterAttribute attribute, NamingStrategy namingStrategy = null)
        {
            attribute.ThrowIfNullOrDefault();

            var type = attribute.Type;

            var schema = new OpenApiSchema()
            {
                Type = type.ToDataType(),
                Format = type.ToDataFormat(),
            };

            if (type.IsOpenApiArray())
            {
                schema.Type = "array";
                schema.Format = null;
                schema.Items = (type.GetElementType() ?? type.GetGenericArguments()[0]).ToOpenApiSchema(namingStrategy);
            }

            if (type.IsUnflaggedEnumType())
            {
                if (type.HasJsonConverterAttribute<StringEnumConverter>())
                {
                    var enums = type.ToOpenApiStringCollection(namingStrategy);

                    schema.Type = "string";
                    schema.Format = null;
                    schema.Enum = enums;
                    schema.Default = enums.First();
                }
                else
                {
                    var enums = type.ToOpenApiIntegerCollection();

                    schema.Enum = enums;
                    schema.Default = enums.First();
                }
            }

            var parameter = new OpenApiParameter()
            {
                Name = attribute.Name,
                Description = attribute.Description,
                Required = attribute.Required,
                In = attribute.In,
                Schema = schema
            };

            if (type.IsOpenApiArray())
            {
                if (attribute.In == ParameterLocation.Path)
                {
                    parameter.Style = ParameterStyle.Simple;
                    parameter.Explode = false;
                }

                if (attribute.In == ParameterLocation.Query)
                {
                    parameter.Style = attribute.CollectionDelimiter == OpenApiParameterCollectionDelimiterType.Comma
                                      ? ParameterStyle.Form
                                      : (attribute.CollectionDelimiter == OpenApiParameterCollectionDelimiterType.Space
                                         ? ParameterStyle.SpaceDelimited
                                         : ParameterStyle.PipeDelimited);
                    parameter.Explode = attribute.CollectionDelimiter == OpenApiParameterCollectionDelimiterType.Comma
                                        ? attribute.Explode
                                        : false;
                }
            }

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
