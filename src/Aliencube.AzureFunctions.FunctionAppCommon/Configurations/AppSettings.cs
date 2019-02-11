using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Aliencube.AzureFunctions.Extensions.DependencyInjection;
using Aliencube.AzureFunctions.Extensions.OpenApi.Configurations;

using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace Aliencube.AzureFunctions.FunctionAppCommon.Configurations
{
    /// <summary>
    /// This represents the settings entity from the configurations.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettings"/> class.
        /// </summary>
        public AppSettings()
        {
            var config = new ConfigurationBuilder()
                             .AddEnvironmentVariables()
                             .Build();

            var basePath = GetBasePath();
            var host = new ConfigurationBuilder()
                           .SetBasePath(basePath)
                           .AddJsonFile("host.json")
                           .Build();

            this.OpenApiInfo = config.Get<OpenApiInfo>("OpenApi:Info");
            this.SwaggerAuthKey = config.GetValue<string>("OpenApi:ApiKey");

            var version = host.GetSection("version").Value;
            this.HttpSettings = string.IsNullOrWhiteSpace(version)
                                    ? host.Get<HttpSettings>("http")
                                    : (version.Equals("2.0", StringComparison.CurrentCultureIgnoreCase)
                                           ? host.Get<HttpSettings>("extensions:http")
                                           : host.Get<HttpSettings>("http"));
        }

        /// <summary>
        /// Gets the <see cref="Microsoft.OpenApi.Models.OpenApiInfo"/> instance.
        /// </summary>
        public virtual OpenApiInfo OpenApiInfo { get; }

        /// <summary>
        /// Gets the <see cref="Extensions.OpenApi.Configurations.HttpSettings"/> instance.
        /// </summary>
        public virtual HttpSettings HttpSettings { get; }

        /// <summary>
        /// Gets the Function API key for Open API document.
        /// </summary>
        public virtual string SwaggerAuthKey { get; }

        private static string GetBasePath()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var segments = location.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var basePath = string.Join(Path.DirectorySeparatorChar.ToString(), segments.Take(segments.Count - 2));

            return basePath;
        }
    }
}