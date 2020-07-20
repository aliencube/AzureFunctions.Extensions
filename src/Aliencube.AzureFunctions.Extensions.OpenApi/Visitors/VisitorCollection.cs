using System;
using System.Collections.Generic;
using System.Linq;

using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;

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
    }
}
