using System;
using System.Collections.Generic;

using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Visitors
{
    /// <summary>
    /// This provides interfaces to visitor classes.
    /// </summary>
    public interface IVisitor
    {
        /// <summary>
        /// Checks whether the type is visitable or not.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>Returns <c>True</c>, if the type is visitable; otherwise returns <c>False</c>.</returns>
        bool IsVisitable(Type type);

        /// <summary>
        /// Visits and process the acceptor.
        /// </summary>
        /// <param name="acceptor"><see cref="IAcceptor"/> instance.</param>
        /// <param name="type">Type to check.</param>
        /// <param name="namingStrategy"><see cref="NamingStrategy"/> instance.</param>
        /// <param name="attributes">List of attribute instances.</param>
        void Visit(IAcceptor acceptor, KeyValuePair<string, Type> type, NamingStrategy namingStrategy, params Attribute[] attributes);

        /// <summary>
        /// Checks whether the type is navigatable to its sub-type or not.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>Returns <c>True</c>, if the type is navigatable; otherwise returns <c>False</c>.</returns>
        bool IsNavigatable(Type type);
    }
}
