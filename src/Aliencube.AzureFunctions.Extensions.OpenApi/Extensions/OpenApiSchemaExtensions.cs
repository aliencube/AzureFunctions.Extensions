using System;
using System.Collections;
using System.Linq;
using System.Reflection;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

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
        /// <param name="namingStrategy"><see cref="NamingStrategy"/> insance to create the JSON schema from .NET Types.</param>
        /// <param name="attribute"><see cref="OpenApiSchemaVisibilityAttribute"/> instance. Default is <c>null</c>.</param>
        /// <returns><see cref="OpenApiSchema"/> instance.</returns>
        /// <remarks>
        /// It runs recursively to build the entire object type. It only takes properties without <see cref="JsonIgnoreAttribute"/>.
        /// </remarks>
        public static OpenApiSchema ToOpenApiSchema(this Type type, NamingStrategy namingStrategy, OpenApiSchemaVisibilityAttribute attribute = null)
        {
            type.ThrowIfNullOrDefault();

            var schema = (OpenApiSchema)null;

            if (type == typeof(JObject))
            {
                schema = typeof(object).ToOpenApiSchema(namingStrategy);

                return schema;
            }

            if (type == typeof(JToken))
            {
                schema = typeof(object).ToOpenApiSchema(namingStrategy);

                return schema;
            }

            var unwrappedValueType = Nullable.GetUnderlyingType(type);
            if (!unwrappedValueType.IsNullOrDefault())
            {
                schema = unwrappedValueType.ToOpenApiSchema(namingStrategy);
                schema.Nullable = true;

                return schema;
            }

            schema = new OpenApiSchema()
                         {
                             Type = type.ToDataType(),
                             Format = type.ToDataFormat()
                         };

            if (!attribute.IsNullOrDefault())
            {
                var visibility = new OpenApiString(attribute.Visibility.ToDisplayName());

                schema.Extensions.Add("x-ms-visibility", visibility);
            }

            if (typeof(Enum).IsAssignableFrom(type))
            {
                var isFlags = type.IsDefined(typeof(FlagsAttribute), false);
                if (!isFlags)
                {
                    var converterAttribute = type.GetCustomAttribute<JsonConverterAttribute>();
                    if (!converterAttribute.IsNullOrDefault()
                        && typeof(StringEnumConverter).IsAssignableFrom(converterAttribute.ConverterType))
                    {
                        var names = Enum.GetNames(type);
                        schema.Enum = names.Select(p => (IOpenApiAny)new OpenApiString(namingStrategy.GetPropertyName(p, false))).ToList();
                        schema.Type = "string";
                        schema.Format = null;
                    }
                    else if (type.GetEnumUnderlyingType() == typeof(short))
                    {
                        var values = Enum.GetValues(type);
                        schema.Enum = values.Cast<short>().Select(p => (IOpenApiAny)new OpenApiInteger(p)).ToList();
                    }
                    else if (type.GetEnumUnderlyingType() == typeof(int))
                    {
                        var values = Enum.GetValues(type);
                        schema.Enum = values.Cast<int>().Select(p => (IOpenApiAny)new OpenApiInteger(p)).ToList();
                    }
                    else if (type.GetEnumUnderlyingType() == typeof(long))
                    {
                        var values = Enum.GetValues(type);
                        schema.Enum = values.Cast<long>().Select(p => (IOpenApiAny)new OpenApiLong(p)).ToList();
                    }
                }
            }

            if (type.IsSimpleType())
            {
                return schema;
            }

            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                schema.AdditionalProperties = type.GetGenericArguments()[1].ToOpenApiSchema(namingStrategy);

                return schema;
            }

            if (type.IsOpenApiArray())
            {
                schema.Type = "array";
                schema.Items = (type.GetElementType() ?? type.GetGenericArguments()[0]).ToOpenApiSchema(namingStrategy);

                return schema;
            }

            var properties = type.GetProperties().Where(p => !p.ExistsCustomAttribute<JsonIgnoreAttribute>());
            foreach (var property in properties)
            {
                var visiblity = property.GetCustomAttribute<OpenApiSchemaVisibilityAttribute>(inherit: false);
                var propertyName = property.GetJsonPropertyName();

                schema.Properties[namingStrategy.GetPropertyName(property.Name, false)] = property.PropertyType.ToOpenApiSchema(namingStrategy, visiblity);
            }

            return schema;
        }
    }
}
