using Aliencube.AzureFunctions.Extensions.Configuration.AppSettings.Resolvers;
using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;

using Microsoft.Extensions.Configuration;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Resolvers
{
    /// <summary>
    /// This represents the resolver entity for host.json.
    /// </summary>
    public class HostJsonResolver
    {
        /// <summary>
        /// Gets the <see cref="IConfiguration"/> instance from host.json
        /// </summary>
        /// <param name="config"><see cref="IConfiguration"/> instance from the environment variables - either local.settings.json or App Settings blade.</param>
        /// <param name="basePath">Base path of the executing Azure Functions assembly.</param>
        public static IConfiguration Resolve(IConfiguration config = null, string basePath = null)
        {
            if (config.IsNullOrDefault())
            {
                config = ConfigurationResolver.Resolve();
            }

            if (basePath.IsNullOrWhiteSpace())
            {
                basePath = ConfigurationResolver.GetBasePath(config);
            }

            var host = new ConfigurationBuilder()
                           .SetBasePath(basePath)
                           .AddJsonFile("host.json")
                           .Build();

            return host;
        }
    }
}
