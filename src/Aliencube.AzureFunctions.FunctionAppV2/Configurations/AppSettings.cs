using System;
using System.IO;

using Aliencube.AzureFunctions.Extensions.DependencyInjection;
using Aliencube.AzureFunctions.Extensions.OpenApi.Configurations;

using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace Aliencube.AzureFunctions.FunctionAppV2.Configurations
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
            var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            var host = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("host.json").Build();

            this.OpenApiInfo = config.Get<OpenApiInfo>("OpenApi:Info");

            var version = host.GetSection("version").Value;
            this.HttpSettings = version.Equals("2.0", StringComparison.CurrentCultureIgnoreCase)
                ? host.Get<HttpSettings>("extensions:http")
                : host.Get<HttpSettings>("http");
        }

        /// <summary>
        /// Gets the <see cref="Microsoft.OpenApi.Models.OpenApiInfo"/> instance.
        /// </summary>
        public virtual OpenApiInfo OpenApiInfo { get; }

        public virtual HttpSettings HttpSettings { get; }
    }
}