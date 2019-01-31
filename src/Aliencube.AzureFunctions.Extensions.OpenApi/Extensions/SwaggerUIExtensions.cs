using System;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Extensions
{
    /// <summary>
    /// This represents the extension entity for <see cref="SwaggerUI"/>.
    /// </summary>
    public static class SwaggerUIExtensions
    {
        /// <summary>
        /// Renders the Open API UI in HTML.
        /// </summary>
        /// <param name="ui"><see cref="ISwaggerUI"/> instance.</param>
        /// <param name="endpoint">The endpoint of the Swagger document.</param>
        /// <returns>The Open API UI in HTML.</returns>
        public static async Task<string> RenderAsync(this Task<ISwaggerUI> ui, string endpoint)
        {
            if (ui == null)
            {
                throw new ArgumentNullException(nameof(ui));
            }

            var instance = await ui.ConfigureAwait(false);

            return await instance.RenderAsync(endpoint).ConfigureAwait(false);
        }
    }
}