using System;

using Aliencube.AzureFunctions.Extensions.Configuration.AppSettings;
using Aliencube.AzureFunctions.Extensions.Configuration.AppSettings.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Configurations
{
    /// <summary>
    /// This represents the base settings entity from the configurations for Open API.
    /// </summary>
    public abstract class OpenApiAppSettingsBase : AppSettingsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiAppSettingsBase"/> class.
        /// </summary>
        protected OpenApiAppSettingsBase()
            : base()
        {
            var basePath = GetBasePath();
            var host = new ConfigurationBuilder()
                           .SetBasePath(basePath)
                           .AddJsonFile("host.json")
                           .Build();

            this.OpenApiInfo = this.Config.Get<OpenApiInfo>("OpenApi:Info");
            this.SwaggerAuthKey = this.Config.GetValue<string>("OpenApi:ApiKey");

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
        /// Gets the Function API key for Open API document.
        /// </summary>
        public virtual string SwaggerAuthKey { get; }

        /// <summary>
        /// Gets the <see cref="Configurations.HttpSettings"/> instance.
        /// </summary>
        public virtual HttpSettings HttpSettings { get; }
    }
}