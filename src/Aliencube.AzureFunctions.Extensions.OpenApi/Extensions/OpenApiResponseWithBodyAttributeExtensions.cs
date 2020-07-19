using System.Collections.Generic;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Extensions
{
    /// <summary>
    /// This represents the extension entity for <see cref="OpenApiResponseWithBodyAttribute"/>.
    /// </summary>
    public static class OpenApiResponseWithBodyAttributeExtensions
    {
        /// <summary>
        /// Converts <see cref="OpenApiResponseWithBodyAttribute"/> to <see cref="OpenApiResponse"/>.
        /// </summary>
        /// <param name="attribute"><see cref="OpenApiResponseWithBodyAttribute"/> instance.</param>
        /// <param name="namingStrategy"><see cref="NamingStrategy"/> instance to create the JSON schema from .NET Types.</param>
        /// <returns><see cref="OpenApiResponse"/> instance.</returns>
        public static OpenApiResponse ToOpenApiResponse(this OpenApiResponseWithBodyAttribute attribute, NamingStrategy namingStrategy = null)
        {
            attribute.ThrowIfNullOrDefault();

            var description = string.IsNullOrWhiteSpace(attribute.Description)
                                  ? $"Payload of {attribute.BodyType.GetOpenApiDescription()}"
                                  : attribute.Description;
            var mediaType = attribute.ToOpenApiMediaType<OpenApiResponseWithBodyAttribute>(namingStrategy);
            var content = new Dictionary<string, OpenApiMediaType>()
                              {
                                  { attribute.ContentType, mediaType }
                              };
            var response = new OpenApiResponse()
            {
                Description = description,
                Content = content
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
