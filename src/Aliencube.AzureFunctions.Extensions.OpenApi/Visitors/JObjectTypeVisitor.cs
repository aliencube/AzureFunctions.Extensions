using System;
using System.Collections.Generic;

using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Visitors
{
    /// <summary>
    /// This represents the type visitor for <see cref="JObject"/> or <see cref="JToken"/>.
    /// </summary>
    public class JObjectTypeVisitor : TypeVisitor
    {
        /// <inheritdoc />
        public override bool IsVisitable(Type type)
        {
            var isVisitable = this.IsVisitable(type, TypeCode.Object) && type.IsJObjectType();

            return isVisitable;
        }

        /// <inheritdoc />
        public override void Visit(IAcceptor acceptor, KeyValuePair<string, Type> type, NamingStrategy namingStrategy, params Attribute[] attributes)
        {
            var title = namingStrategy.GetPropertyName(type.Value.Name, hasSpecifiedName: false);
            this.Visit(acceptor, name: type.Key, title: title, dataType: "object", dataFormat: null, attributes: attributes);
        }

        /// <inheritdoc />
        public override bool IsParameterVisitable(Type type)
        {
            return false;
        }
    }
}
