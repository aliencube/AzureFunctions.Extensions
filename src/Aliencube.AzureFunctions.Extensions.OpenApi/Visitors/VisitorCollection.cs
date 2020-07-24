using System;
using System.Collections.Generic;
using System.Linq;

using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;

using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Visitors
{
    /// <summary>
    /// This represents the collection entity for <see cref="IVisitor"/> instances.
    /// </summary>
    public class VisitorCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisitorCollection"/> class.
        /// </summary>
        public VisitorCollection()
        {
            this.Visitors = new List<IVisitor>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitorCollection"/> class.
        /// </summary>
        /// <param name="visitors">List of <see cref="IVisitor"/> instances.</param>
        public VisitorCollection(List<IVisitor> visitors)
        {
            this.Visitors = visitors.ThrowIfNullOrDefault();
        }

        /// <summary>
        /// Gets the list of <see cref="IVisitor"/> instances.
        /// </summary>
        public List<IVisitor> Visitors { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="VisitorCollection"/> class.
        /// </summary>
        /// <returns>Returns the <see cref="VisitorCollection"/> instance.</returns>
        public static VisitorCollection CreateInstance()
        {
            var visitors = typeof(IVisitor).Assembly
                                           .GetTypes()
                                           .Where(p => p.Name.EndsWith("Visitor") && p.IsClass && !p.IsAbstract)
                                           .Select(p => (IVisitor)Activator.CreateInstance(p))
                                           .ToList();
            var collection = new VisitorCollection(visitors);

            return collection;
        }

        /// <summary>
        /// Processes the parameter type.
        /// </summary>
        /// <param name="type">Type of the parameter.</param>
        /// <param name="namingStrategy"><see cref="NamingStrategy"/> instance.</param>
        /// <returns>Returns <see cref="OpenApiSchema"/> instance.</returns>
        public OpenApiSchema ParameterVisit(Type type, NamingStrategy namingStrategy)
        {
            var schema = default(OpenApiSchema);
            foreach (var visitor in this.Visitors)
            {
                if (!visitor.IsParameterVisitable(type))
                {
                    continue;
                }

                schema = visitor.ParameterVisit(type, namingStrategy);
                break;
            }

            return schema;
        }

        /// <summary>
        /// Processes the request/response payload type.
        /// </summary>
        /// <param name="type">Type of the payload.</param>
        /// <param name="namingStrategy"><see cref="NamingStrategy"/> instance.</param>
        /// <returns>Returns <see cref="OpenApiSchema"/> instance.</returns>
        public OpenApiSchema PayloadVisit(Type type, NamingStrategy namingStrategy)
        {
            var schema = default(OpenApiSchema);
            foreach (var visitor in this.Visitors)
            {
                if (!visitor.IsPayloadVisitable(type))
                {
                    continue;
                }

                schema = visitor.PayloadVisit(type, namingStrategy);
                break;
            }

            return schema;
        }
    }
}
