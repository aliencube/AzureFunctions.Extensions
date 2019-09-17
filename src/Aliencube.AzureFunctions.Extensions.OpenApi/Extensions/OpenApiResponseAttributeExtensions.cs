using System.Collections.Generic;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Extensions
{
    /// <summary>
    /// This represents the extension entity for <see cref="OpenApiResponseBodyAttribute"/>.
    /// </summary>
    public static class OpenApiResponseAttributeExtensions
    {
        /// <summary>
        /// Converts <see cref="OpenApiResponseBodyAttribute"/> to <see cref="OpenApiResponse"/>.
        /// </summary>
        /// <param name="attribute"><see cref="OpenApiResponseBodyAttribute"/> instance.</param>
        /// <param name="namingStrategy"><see cref="NamingStrategy"/> instance to create the JSON schema from .NET Types.</param>
        /// <returns><see cref="OpenApiResponse"/> instance.</returns>
        public static OpenApiResponse ToOpenApiResponse(this OpenApiResponseAttribute attribute, NamingStrategy namingStrategy = null)
        {
            attribute.ThrowIfNullOrDefault();

            var description = !string.IsNullOrWhiteSpace(attribute.Description) ? attribute.Description : "No description";
            var response = new OpenApiResponse()
            {
                Description = description,
            };

            if (!string.IsNullOrWhiteSpace(attribute.Summary))
            {
                var summary = new OpenApiString(attribute.Summary);

                response.Extensions.Add("x-ms-summary", summary);
            }

            return response;
        }
    }
}