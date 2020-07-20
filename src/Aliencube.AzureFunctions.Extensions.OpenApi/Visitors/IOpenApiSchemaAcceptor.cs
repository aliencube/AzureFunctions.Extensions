using System;
using System.Collections.Generic;

using Microsoft.OpenApi.Models;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Visitors
{
    /// <summary>
    /// This provides interfaces to the <see cref="OpenApiSchemaAcceptor"/> class.
    /// </summary>
    public interface IOpenApiSchemaAcceptor : IAcceptor
    {
        /// <summary>
        /// Gets the list of <see cref="OpenApiSchema"/> instances as key/value pair representing the root schemas.
        /// </summary>
        Dictionary<string, OpenApiSchema> RootSchemas { get; set; }

        /// <summary>
        /// Gets the list of <see cref="OpenApiSchema"/> instances as key/value pair.
        /// </summary>
        Dictionary<string, OpenApiSchema> Schemas { get; set; }

        /// <summary>
        /// Gets the list of <see cref="Type"/> objects.
        /// </summary>
        Dictionary<string, Type> Types { get; set; }
    }
}
