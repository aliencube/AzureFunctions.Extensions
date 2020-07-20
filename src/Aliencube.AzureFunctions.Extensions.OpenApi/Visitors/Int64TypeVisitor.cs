using System;
using System.Collections.Generic;

using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;

using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Visitors
{
    /// <summary>
    /// This represents the type visitor for <see cref="long"/>.
    /// </summary>
    public class Int64TypeVisitor : TypeVisitor
    {
        /// <inheritdoc />
        public override bool IsVisitable(Type type)
        {
            var isVisitable = this.IsVisitable(type, TypeCode.Int64) &&
                              !type.IsUnflaggedEnumType();

            return isVisitable;
        }

        /// <inheritdoc />
        public override void Visit(IAcceptor acceptor, KeyValuePair<string, Type> type, NamingStrategy namingStrategy, params Attribute[] attributes)
        {
            this.Visit(acceptor, name: type.Key, title: null, dataType: "integer", dataFormat: "int64", attributes: attributes);
        }
    }
}
