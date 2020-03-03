using System;
using System.Collections.Generic;
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
            return ToOpenApiSchemas(type, namingStrategy, attribute, true).First().Value;
        }
        public static Dictionary<string, OpenApiSchema> ToOpenApiSchemas(this Type type, NamingStrategy namingStrategy, OpenApiSchemaVisibilityAttribute attribute = null, bool returnSingleSchema=false)
        {
            type.ThrowIfNullOrDefault();

            var schema = (OpenApiSchema)null;
            var schemeName = type.IsGenericType ? type.GetOpenApiGenericRootName() : type.Name;

            if (type.IsJObjectType())
            {
                schema = typeof(object).ToOpenApiSchemas(namingStrategy, null, true).First().Value;

                return new Dictionary<string, OpenApiSchema>() { { schemeName, schema } };
            }

            var unwrappedValueType = Nullable.GetUnderlyingType(type);
            if (!unwrappedValueType.IsNullOrDefault())
            {
                schema = unwrappedValueType.ToOpenApiSchemas(namingStrategy, null, true).First().Value;
                schema.Nullable = true;
                return new Dictionary<string, OpenApiSchema>() { { schemeName, schema } };
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
                return new Dictionary<string, OpenApiSchema>() { { schemeName, schema } };
            }

            if (type.IsOpenApiDictionary())
            {
                schema.AdditionalProperties = type.GetGenericArguments()[1].ToOpenApiSchemas(namingStrategy, null, true).First().Value;

                return new Dictionary<string, OpenApiSchema>() { { schemeName, schema } };
            }

            if (type.IsOpenApiArray())
            {
                schema.Type = "array";
                schema.Items = (type.GetElementType() ?? type.GetGenericArguments()[0]).ToOpenApiSchemas(namingStrategy, null, true).First().Value;

                return new Dictionary<string, OpenApiSchema>() { { schemeName, schema } };
            }

            var allProperties = type.IsInterface
                                    ? new[] { type }.Concat(type.GetInterfaces()).SelectMany(i => i.GetProperties())
                                    : type.GetProperties();
            var properties = allProperties.Where(p => !p.ExistsCustomAttribute<JsonIgnoreAttribute>());
            var retVal = new Dictionary<string, OpenApiSchema>();
            foreach (var property in properties)
            {
                var visiblity = property.GetCustomAttribute<OpenApiSchemaVisibilityAttribute>(inherit: false);
                var propertyName = property.GetJsonPropertyName();

                var ts = property.DeclaringType.GetGenericArguments();
                if (!ts.Any())
                {
                    if (property.PropertyType.IsSimpleType() || returnSingleSchema)                      
                    {
                        schema.Properties[namingStrategy.GetPropertyName(property.Name, false)] = property.PropertyType.ToOpenApiSchemas(namingStrategy, visiblity, true).First().Value;

                    }
                    else if (property.PropertyType.IsOpenApiDictionary())
                    {
                        var elementType = property.PropertyType.GetGenericArguments()[1];
                        if (elementType.IsSimpleType() || elementType.IsOpenApiDictionary() || elementType.IsOpenApiArray())
                            schema.Properties[namingStrategy.GetPropertyName(property.Name, false)] = property.PropertyType.ToOpenApiSchemas(namingStrategy, visiblity, true).First().Value;
                        else
                        {
                            var recur1 = elementType.ToOpenApiSchemas(namingStrategy, visiblity);
                            retVal.AddRange(recur1);
                            var elementReference = new OpenApiReference()
                            {
                                Type = ReferenceType.Schema,
                                Id = elementType.GetOpenApiReferenceId(false, false)
                            };
                            
                            var dictionarySchema = new OpenApiSchema()
                            {
                                Type = "object",
                                AdditionalProperties = new OpenApiSchema() { Reference = elementReference }
                            };
                            schema.Properties[namingStrategy.GetPropertyName(property.Name, false)] = dictionarySchema;
                        }
                    }
                    else if (property.PropertyType.IsOpenApiArray())
                    {
                        var elementType = (property.PropertyType.GetElementType() ?? property.PropertyType.GetGenericArguments()[0]);
                        if(elementType.IsSimpleType() || elementType.IsOpenApiDictionary() || elementType.IsOpenApiArray())
                            schema.Properties[namingStrategy.GetPropertyName(property.Name, false)] = property.PropertyType.ToOpenApiSchemas(namingStrategy, visiblity, true).First().Value;
                        else
                        {
                            var elementReference = elementType.ToOpenApiSchemas(namingStrategy, visiblity);
                            retVal.AddRange(elementReference);
                            var reference1 = new OpenApiReference()
                            {
                                Type = ReferenceType.Schema,
                                Id = elementType.GetOpenApiReferenceId(false, false)
                            };
                            var arraySchema = new OpenApiSchema() {
                                Type = "array",
                                Items = new OpenApiSchema() {
                                    Reference = reference1
                                }
                            };
                            schema.Properties[namingStrategy.GetPropertyName(property.Name, false)] = arraySchema;
                        }
                        
                    }
                    else
                    {
                        var recur1 = property.PropertyType.ToOpenApiSchemas(namingStrategy, visiblity);
                        retVal.AddRange(recur1);
                        var reference1 = new OpenApiReference()
                        {
                            Type = ReferenceType.Schema,
                            Id = property.PropertyType.GetOpenApiReferenceId(false, false)
                        };
                        var schema1 = new OpenApiSchema() { Reference = reference1 };
                        schema.Properties[namingStrategy.GetPropertyName(property.Name, false)] = schema1;                        
                    }
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
                        schema.Properties[namingStrategy.GetPropertyName(property.Name, false)] = dictionarySchema;

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
                        schema.Properties[namingStrategy.GetPropertyName(property.Name, false)] = arraySchema;

                        continue;
                    }

                    schema.Properties[namingStrategy.GetPropertyName(property.Name, false)] = property.PropertyType.ToOpenApiSchemas(namingStrategy, visiblity, true).First().Value;

                    continue;
                }

                schema.Properties[namingStrategy.GetPropertyName(property.Name, false)] = referenceSchema;
            }
          
            retVal.Add(schemeName, schema);
            return retVal;
        }
    }
}
