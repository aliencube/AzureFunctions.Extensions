using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;

using Microsoft.OpenApi.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Visitors
{
    /// <summary>
    /// This represents the type visitor for <see cref="object"/> that contains recursive properties.
    /// </summary>
    public class RecursiveObjectTypeVisitor : TypeVisitor
    {
        /// <inheritdoc />
        public override bool IsVisitable(Type type)
        {
            var isVisitable = this.IsVisitable(type, TypeCode.Object) && type.HasRecursiveProperty();
            if (type == typeof(Guid))
            {
                isVisitable = false;
            }
            if (type == typeof(DateTime))
            {
                isVisitable = false;
            }
            if (type == typeof(DateTimeOffset))
            {
                isVisitable = false;
            }
            if (type.IsOpenApiNullable())
            {
                isVisitable = false;
            }
            if (type.IsUnflaggedEnumType())
            {
                isVisitable = false;
            }
            if (type.IsJObjectType())
            {
                isVisitable = false;
            }

            return isVisitable;
        }

        /// <inheritdoc />
        public override void Visit(IAcceptor acceptor, KeyValuePair<string, Type> type, NamingStrategy namingStrategy, params Attribute[] attributes)
        {
            var title = namingStrategy.GetPropertyName(type.Value.Name, hasSpecifiedName: false);
            var name = this.Visit(acceptor, name: type.Key, title: title, dataType: "object", dataFormat: null, attributes: attributes);

            if (name.IsNullOrWhiteSpace())
            {
                return;
            }

            if (!this.IsNavigatable(type.Value))
            {
                return;
            }

            var instance = acceptor as OpenApiSchemaAcceptor;
            if (instance.IsNullOrDefault())
            {
                return;
            }

            // Processes non-recursive properties
            var properties = type.Value
                                 .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => !p.ExistsCustomAttribute<JsonIgnoreAttribute>())
                                 .Where(p => p.PropertyType != type.Value)
                                 .ToDictionary(p => p.GetJsonPropertyName(namingStrategy), p => p);

            this.ProcessProperties(instance, name, properties, namingStrategy);

            // Processes recursive properties
            var recursiveProperties = type.Value
                                          .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                          .Where(p => !p.ExistsCustomAttribute<JsonIgnoreAttribute>())
                                          .Where(p => p.PropertyType == type.Value)
                                          .ToDictionary(p => p.GetJsonPropertyName(namingStrategy), p => p);
            var recursiveSchemas = recursiveProperties.ToDictionary(p => p.Key,
                                                                    p => new OpenApiSchema()
                                                                    {
                                                                        Type = "object",
                                                                        Reference = new OpenApiReference()
                                                                        {
                                                                            Type = ReferenceType.Schema,
                                                                            Id = p.Value.PropertyType.GetOpenApiReferenceId(isDictionary: false, isList: false, namingStrategy)
                                                                        }
                                                                    });
            foreach (var recursiveSchema in recursiveSchemas)
            {
                instance.Schemas[name].Properties.Add(recursiveSchema);
            }

            var reference = new OpenApiReference()
            {
                Type = ReferenceType.Schema,
                Id = type.Value.GetOpenApiReferenceId(isDictionary: false, isList: false, namingStrategy)
            };

            instance.Schemas[name].Reference = reference;
        }

        /// <inheritdoc />
        public override bool IsNavigatable(Type type)
        {
            if (type.IsJObjectType())
            {
                return false;
            }

            if (type.IsOpenApiNullable())
            {
                return false;
            }

            if (type.IsOpenApiArray())
            {
                return false;
            }

            if (type.IsOpenApiDictionary())
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public override bool IsParameterVisitable(Type type)
        {
            return false;
        }

        private void ProcessProperties(IOpenApiSchemaAcceptor instance, string schemaName, Dictionary<string, PropertyInfo> properties, NamingStrategy namingStrategy)
        {
            var schemas = new Dictionary<string, OpenApiSchema>();

            var subAcceptor = new OpenApiSchemaAcceptor()
            {
                Properties = properties,
                RootSchemas = instance.RootSchemas,
                Schemas = schemas,
            };

            var collection = VisitorCollection.CreateInstance();
            subAcceptor.Accept(collection, namingStrategy);

            // Add required properties to schema.
            var jsonPropertyAttributes = properties.Where(p => !p.Value.GetCustomAttribute<JsonPropertyAttribute>(inherit: false).IsNullOrDefault())
                                                   .Select(p => new KeyValuePair<string, JsonPropertyAttribute>(p.Key, p.Value.GetCustomAttribute<JsonPropertyAttribute>(inherit: false)))
                                                   .Where(p => p.Value.Required == Required.Always || p.Value.Required == Required.DisallowNull);
            foreach (var attribute in jsonPropertyAttributes)
            {
                instance.Schemas[schemaName].Required.Add(attribute.Key);
            }

            var jsonRequiredAttributes = properties.Where(p => !p.Value.GetCustomAttribute<JsonRequiredAttribute>(inherit: false).IsNullOrDefault())
                                                   .Select(p => new KeyValuePair<string, JsonRequiredAttribute>(p.Key, p.Value.GetCustomAttribute<JsonRequiredAttribute>(inherit: false)));
            foreach (var attribute in jsonRequiredAttributes)
            {
                var attributeName = namingStrategy.GetPropertyName(attribute.Key, hasSpecifiedName: false);
                if (instance.Schemas[schemaName].Required.Contains(attributeName))
                {
                    continue;
                }

                instance.Schemas[schemaName].Required.Add(attributeName);
            }

            instance.Schemas[schemaName].Properties = subAcceptor.Schemas;

            var schemasToBeAdded = subAcceptor.Schemas
                                              .Where(p => !instance.Schemas.Keys.Contains(p.Key))
                                              .Where(p => p.Value.Type == "object" &&
                                                          p.Value.Format.IsNullOrWhiteSpace() &&
                                                          p.Value.Items.IsNullOrDefault() &&
                                                          p.Value.AdditionalProperties.IsNullOrDefault())
                                              .ToDictionary(p => p.Value.Title, p => p.Value);

            foreach (var schema in schemasToBeAdded.Where(p => p.Key != "jObject" && p.Key != "jToken"))
            {
                if (instance.RootSchemas.ContainsKey(schema.Key))
                {
                    continue;
                }

                instance.RootSchemas.Add(schema.Key, schema.Value);
            }

            // Removes title of each property.
            var subSchemas = instance.Schemas[schemaName].Properties;
            subSchemas = subSchemas.Select(p =>
                                    {
                                        p.Value.Title = null;
                                        return new KeyValuePair<string, OpenApiSchema>(p.Key, p.Value);
                                    })
                                   .ToDictionary(p => p.Key, p => p.Value);

            instance.Schemas[schemaName].Properties = subSchemas;
        }
    }
}
