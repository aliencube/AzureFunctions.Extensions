using System;

using Aliencube.AzureFunctions.Extensions.Configuration.AppSettings;
using Aliencube.AzureFunctions.Extensions.Configuration.AppSettings.Extensions;
using Aliencube.AzureFunctions.Extensions.Configuration.AppSettings.Resolvers;
using Aliencube.AzureFunctions.Extensions.OpenApi.Resolvers;

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
            var basePath = this.GetBasePath();
            var host = HostJsonResolver.Resolve(this.Config, basePath);
            var openapi = OpenApiSettingsJsonResolver.Resolve(this.Config, basePath);

            this.OpenApiInfo = OpenApiInfoResolver.Resolve(host, openapi, this.Config);
            this.SwaggerAuthKey = ConfigurationResolver.GetValue<string>("OpenApi:ApiKey", this.Config);

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