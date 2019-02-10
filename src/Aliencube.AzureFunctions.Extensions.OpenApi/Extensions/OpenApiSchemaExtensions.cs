using System;
using System.Collections;
using System.Linq;
using System.Reflection;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Extensions
{
    /// <summary>
    /// This represents the extension entity for <see cref="OpenApiSchema"/>.
    /// </summary>
    public static class OpenApiSchemaExtensions
    {
        /// <summary>
        /// Converts <see cref="Type"/> to <see cref="OpenApiSchema"/>.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <param name="attribute"><see cref="OpenApiSchemaVisibilityAttribute"/> instance. Default is <c>null</c>.</param>
        /// <returns><see cref="OpenApiSchema"/> instance.</returns>
        /// <remarks>
        /// It runs recursively to build the entire object type. It only takes properties without <see cref="JsonIgnoreAttribute"/>.
        /// </remarks>
        public static OpenApiSchema ToOpenApiSchema(this Type type, OpenApiSchemaVisibilityAttribute attribute = null)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var typeCode = Type.GetTypeCode(type);
            var schema = new OpenApiSchema()
                             {
                                 Type = typeCode.ToDataType(),
                                 Format = typeCode.ToDataFormat()
                             };
            if (attribute != null)
            {
                var visibility = new OpenApiString(attribute.Visibility.ToDisplayName());

                schema.Extensions.Add("x-ms-visibility", visibility);
            }

            if (typeCode != TypeCode.Object)
            {
                return schema;
            }

            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                schema.AdditionalProperties = type.GetGenericArguments()[1].ToOpenApiSchema();

                return schema;
            }

            if (typeof(IList).IsAssignableFrom(type))
            {
                schema.Type = "array";
                schema.Items = type.GetGenericArguments()[0].ToOpenApiSchema();

                return schema;
            }

            var properties = type.GetProperties()
                                 .Where(p => !p.ExistsCustomAttribute<JsonIgnoreAttribute>());
            foreach (var property in properties)
            {
                var visiblity = property.GetCustomAttribute<OpenApiSchemaVisibilityAttribute>(inherit: false);

                schema.Properties[property.Name] = property.PropertyType.ToOpenApiSchema(visiblity);
            }

            return schema;
        }
    }
}