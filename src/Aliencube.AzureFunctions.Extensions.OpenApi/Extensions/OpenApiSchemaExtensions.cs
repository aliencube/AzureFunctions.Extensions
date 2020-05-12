using System;
using System.Linq;
using System.Reflection;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        /// <param name="namingStrategy"><see cref="NamingStrategy"/> instance to create the JSON schema from .NET Types.</param>
        /// <param name="attribute"><see cref="OpenApiSchemaVisibilityAttribute"/> instance. Default is <c>null</c>.</param>
        /// <returns><see cref="OpenApiSchema"/> instance.</returns>
        /// <remarks>
        /// It runs recursively to build the entire object type. It only takes properties without <see cref="JsonIgnoreAttribute"/>.
        /// </remarks>
        public static OpenApiSchema ToOpenApiSchema(this Type type, NamingStrategy namingStrategy, OpenApiSchemaVisibilityAttribute attribute = null)
        {
            type.ThrowIfNullOrDefault();

            var schema = (OpenApiSchema)null;

            if (type.IsJObjectType())
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

            if (type.IsUnflaggedEnumType())
            {
                var converterAttribute = type.GetCustomAttribute<JsonConverterAttribute>();
                if (!converterAttribute.IsNullOrDefault()
                    && typeof(StringEnumConverter).IsAssignableFrom(converterAttribute.ConverterType))
                {
                    schema.Type = "string";
                    schema.Format = null;
                    schema.Enum = type.ToOpenApiStringCollection(namingStrategy);
                }
                else
                {
                    schema.Enum = type.ToOpenApiIntegerCollection();
                }
            }

            if (type.IsSimpleType())
            {
                return schema;
            }

            if (type.IsOpenApiDictionary())
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

            var allProperties = type.IsInterface
                                    ? new[] { type }.Concat(type.GetInterfaces()).SelectMany(i => i.GetProperties())
                                    : type.GetProperties();
            var properties = allProperties.Where(p => !p.ExistsCustomAttribute<JsonIgnoreAttribute>());

            foreach (var property in properties)
            {
                var visiblity = property.GetCustomAttribute<OpenApiSchemaVisibilityAttribute>(inherit: false);
                var propertyName = property.GetJsonPropertyName();

                var ts = property.DeclaringType.GetGenericArguments();
                if (!ts.Any())
                {
                    schema.Properties[namingStrategy.GetPropertyName(propertyName, false)] = property.PropertyType.ToOpenApiSchema(namingStrategy, visiblity);

                    continue;
                }

                var reference = new OpenApiReference()
                                    {
                                        Type = ReferenceType.Schema,
                                        Id = property.PropertyType.GetOpenApiRootReferenceId()
                                    };
                var referenceSchema = new OpenApiSchema() { Reference = reference };

                if (!ts.Contains(property.PropertyType))
                {
                    if (property.PropertyType.IsOpenApiDictionary())
                    {
                        reference.Id = property.PropertyType.GetOpenApiReferenceId(true, false);
                        var dictionarySchema = new OpenApiSchema()
                                                   {
                                                       Type = "object",
                                                       AdditionalProperties = referenceSchema
                                                   };
                        schema.Properties[namingStrategy.GetPropertyName(propertyName, false)] = dictionarySchema;

                        continue;
                    }

                    if (property.PropertyType.IsOpenApiArray())
                    {
                        reference.Id = property.PropertyType.GetOpenApiReferenceId(false, true);
                        var arraySchema = new OpenApiSchema()
                                              {
                                                  Type = "array",
                                                  Items = referenceSchema
                                              };
                        schema.Properties[namingStrategy.GetPropertyName(propertyName, false)] = arraySchema;

                        continue;
                    }

                    schema.Properties[namingStrategy.GetPropertyName(propertyName, false)] = property.PropertyType.ToOpenApiSchema(namingStrategy, visiblity);

                    continue;
                }

                schema.Properties[namingStrategy.GetPropertyName(propertyName, false)] = referenceSchema;
            }

            return schema;
        }
    }
}
