using System;
using System.Net;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Attributes
{
    /// <summary>
    /// This represents the attribute entity for HTTP triggers to define response body payload.
    /// </summary>

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class OpenApiResponseBodyAttribute : OpenApiPayloadAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiResponseBodyAttribute"/> class.
        /// </summary>
        /// <param name="statusCode">HTTP status code.</param>
        /// <param name="contentType">Content type.</param>
        /// <param name="bodyType">Type of payload.</param>
        public OpenApiResponseBodyAttribute(HttpStatusCode statusCode, string contentType, Type bodyType)
            : base(contentType, bodyType)
        {
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Gets the HTTP status code value.
        /// </summary>
        public virtual HttpStatusCode StatusCode { get; }
    }
}
