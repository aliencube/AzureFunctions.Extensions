using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.Configuration;

using OperatingSystem = Aliencube.AzureFunctions.Extensions.Configuration.AppSettings.Extensions.OperationSystem;

namespace Aliencube.AzureFunctions.Extensions.Configuration.AppSettings
{
    /// <summary>
    /// This represents the base app settings entity.
    /// </summary>
    public abstract class AppSettingsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsBase"/> class.
        /// </summary>
        protected AppSettingsBase()
        {
            this.Config = new ConfigurationBuilder()
                              .AddEnvironmentVariables()
                              .Build();
        }

        /// <summary>
        /// Gets the <see cref="IConfiguration"/> instance.
        /// </summary>
        protected virtual IConfiguration Config { get; }

        /// <summary>
        /// Gets the base path
        /// </summary>
        /// <returns></returns>
        protected string GetBasePath()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var segments = location.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var basePath = string.Join(Path.DirectorySeparatorChar.ToString(), segments.Take(segments.Count - 2));

            if (!OperatingSystem.IsWindows())
            {
                basePath = $"/{basePath}";
            }
#if NET461
            var scriptRootPath = this.Config.GetValue<string>("AzureWebJobsScriptRoot");
            if (!string.IsNullOrWhiteSpace(scriptRootPath))
            {
                basePath = scriptRootPath;
            }
#endif
            return basePath;
        }
    }
}
