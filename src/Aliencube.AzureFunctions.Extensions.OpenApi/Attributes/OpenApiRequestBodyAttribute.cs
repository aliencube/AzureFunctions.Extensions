using System;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Attributes
{
    /// <summary>
    /// This represents the attribute entity for HTTP triggers to define request body payload.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class OpenApiRequestBodyAttribute : OpenApiPayloadAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiRequestBodyAttribute"/> class.
        /// </summary>
        /// <param name="contentType">Content type.</param>
        /// <param name="bodyType">Type of payload.</param>
        public OpenApiRequestBodyAttribute(string contentType, Type bodyType)
            : base(contentType, bodyType)
        {
        }
    }
}
