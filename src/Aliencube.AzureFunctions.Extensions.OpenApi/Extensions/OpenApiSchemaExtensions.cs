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
    using System.Collections.Generic;

    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Schema;
    using Newtonsoft.Json.Serialization;

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
        public static OpenApiSchema ToOpenApiSchema(
            this Type type,
            NamingStrategy namingStrategy,
            OpenApiSchemaVisibilityAttribute attribute = null)
        {
            type.ThrowIfNullOrDefault();
            OpenApiSchema schema = null;

            var unwrappedValueType = Nullable.GetUnderlyingType(type);
            if (unwrappedValueType != null)
            {
                schema = unwrappedValueType.ToOpenApiSchema(namingStrategy);
                schema.Nullable = true;
                return schema;
            }

            schema = new OpenApiSchema() { Type = type.ToDataType(), Format = type.ToDataFormat() };
            if (attribute != null)
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
                    if (converterAttribute != null 
                        && typeof(StringEnumConverter).IsAssignableFrom(converterAttribute.ConverterType))
                    {
                        var names = Enum.GetNames(type);
                        schema.Enum = names.Select(n => (IOpenApiAny)new OpenApiString(namingStrategy.GetPropertyName(n,false))).ToList();
                        schema.Type = "string";
                        schema.Format = null;
                    }
                    else if (type.GetEnumUnderlyingType() == typeof(short))
                    {
                        var values = Enum.GetValues(type);
                        schema.Enum = values.Cast<short>().Select(n => (IOpenApiAny)new OpenApiInteger(n)).ToList();
                    }
                    else if (type.GetEnumUnderlyingType() == typeof(int))
                    {
                        var values = Enum.GetValues(type);
                        schema.Enum = values.Cast<int>().Select(n => (IOpenApiAny)new OpenApiInteger(n)).ToList();
                    }
                    else if (type.GetEnumUnderlyingType() == typeof(long))
                    {
                        var values = Enum.GetValues(type);
                        schema.Enum = values.Cast<long>().Select(n => (IOpenApiAny)new OpenApiLong(n)).ToList();
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

            if (typeof(IList).IsAssignableFrom(type))
            {
                schema.Type = "array";
                schema.Items = (type.GetElementType() ?? type.GetGenericArguments()[0]).ToOpenApiSchema(namingStrategy);

                return schema;
            }

            var properties = type.GetProperties().Where(p => !p.ExistsCustomAttribute<JsonIgnoreAttribute>());
            foreach (var property in properties)
            {
                var visiblity = property.GetCustomAttribute<OpenApiSchemaVisibilityAttribute>(inherit: false);

                schema.Properties[namingStrategy.GetPropertyName(property.Name, false)] = property.PropertyType.ToOpenApiSchema(namingStrategy, visiblity);
            }

            return schema;
        }
    }
}
