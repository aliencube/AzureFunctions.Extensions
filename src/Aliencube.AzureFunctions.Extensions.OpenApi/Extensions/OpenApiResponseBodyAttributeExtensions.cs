using System.Collections.Generic;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

using Microsoft.OpenApi.Models;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Extensions
{
    /// <summary>
    /// This represents the extension entity for <see cref="OpenApiResponseBodyAttribute"/>.
    /// </summary>
    public static class OpenApiResponseBodyAttributeExtensions
    {
        /// <summary>
        /// Converts <see cref="OpenApiResponseBodyAttribute"/> to <see cref="OpenApiResponse"/>.
        /// </summary>
        /// <param name="attribute"><see cref="OpenApiResponseBodyAttribute"/> instance.</param>
        /// <returns><see cref="OpenApiResponse"/> instance.</returns>
        public static OpenApiResponse ToOpenApiResponse(this OpenApiResponseBodyAttribute attribute)
        {
            var description = string.IsNullOrWhiteSpace(attribute.Description)
                                  ? $"Payload of {attribute.BodyType.Name}"
                                  : attribute.Description;
            var mediaType = attribute.ToOpenApiMediaType<OpenApiResponseBodyAttribute>();
            var content = new Dictionary<string, OpenApiMediaType>()
                              {
                                  { attribute.ContentType, mediaType }
                              };
            var response = new OpenApiResponse()
                               {
                                   Description = description,
                                   Content = content
                               };

            return response;
        }
    }
}