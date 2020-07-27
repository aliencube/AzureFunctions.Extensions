using System;
using System.Collections.Generic;
using System.Linq;

using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;

using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Visitors
{
    /// <summary>
    /// This represents the type visitor for <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    public class DictionaryObjectTypeVisitor : TypeVisitor
    {
        /// <inheritdoc />
        public override bool IsVisitable(Type type)
        {
            var isVisitable = this.IsVisitable(type, TypeCode.Object) && type.IsOpenApiDictionary();

            return isVisitable;
        }

        /// <inheritdoc />
        public override void Visit(IAcceptor acceptor, KeyValuePair<string, Type> type, NamingStrategy namingStrategy, params Attribute[] attributes)
        {
            var name = this.Visit(acceptor, name: type.Key, title: null, dataType: "object", dataFormat: null, attributes: attributes);

            if (name.IsNullOrWhiteSpace())
            {
                return;
            }

            var instance = acceptor as OpenApiSchemaAcceptor;
            if (instance.IsNullOrDefault())
            {
                return;
            }

            // Gets the schema for the underlying type.
            var underlyingType = type.Value.GetGenericArguments()[1];
            var types = new Dictionary<string, Type>()
            {
                { underlyingType.GetOpenApiTypeName(namingStrategy), underlyingType }
            };
            var schemas = new Dictionary<string, OpenApiSchema>();

            var subAcceptor = new OpenApiSchemaAcceptor()
            {
                Types = types,
                RootSchemas = instance.RootSchemas,
                Schemas = schemas,
            };

            var collection = VisitorCollection.CreateInstance();
            subAcceptor.Accept(collection, namingStrategy);

            var properties = subAcceptor.Schemas.First().Value;

            // Adds the reference to the schema for the underlying type.
            if (this.IsReferential(underlyingType))
            {
                var reference = new OpenApiReference()
                {
                    Type = ReferenceType.Schema,
                    Id = underlyingType.GetOpenApiReferenceId(isDictionary: false, isList: false, namingStrategy)
                };

                properties.Reference = reference;
            }

            instance.Schemas[name].AdditionalProperties = properties;

            // Adds schemas to the root.
            var schemasToBeAdded = subAcceptor.Schemas
                                              .Where(p => !instance.Schemas.Keys.Contains(p.Key))
                                              .Where(p => p.Value.Type == "object" &&
                                                          p.Value.Format.IsNullOrWhiteSpace() &&
                                                          p.Value.Items.IsNullOrDefault() &&
                                                          p.Value.AdditionalProperties.IsNullOrDefault())
                                              .ToDictionary(p => p.Key, p => p.Value);

            if (!schemasToBeAdded.Any())
            {
                return;
            }

            foreach (var schemaToBeAdded in schemasToBeAdded)
            {
                if (instance.RootSchemas.ContainsKey(schemaToBeAdded.Key))
                {
                    continue;
                }

                instance.RootSchemas.Add(schemaToBeAdded.Key, schemaToBeAdded.Value);
            }
        }

        /// <inheritdoc />
        public override bool IsParameterVisitable(Type type)
        {
            return false;
        }

        /// <inheritdoc />
        public override bool IsPayloadVisitable(Type type)
        {
            var isVisitable = this.IsVisitable(type);

            return isVisitable;
        }

        /// <inheritdoc />
        public override OpenApiSchema PayloadVisit(Type type, NamingStrategy namingStrategy)
        {
            var schema = this.PayloadVisit(dataType: "object", dataFormat: null);

            // Gets the schema for the underlying type.
            var underlyingType = type.GetGenericArguments()[1];
            var collection = VisitorCollection.CreateInstance();
            var properties = collection.PayloadVisit(underlyingType, namingStrategy);

            // Adds the reference to the schema for the underlying type.
            var reference = new OpenApiReference()
            {
                Type = ReferenceType.Schema,
                Id = underlyingType.GetOpenApiReferenceId(isDictionary: false, isList: false, namingStrategy)
            };

            properties.Reference = reference;

            schema.AdditionalProperties = properties;

            return schema;
        }
    }
}
