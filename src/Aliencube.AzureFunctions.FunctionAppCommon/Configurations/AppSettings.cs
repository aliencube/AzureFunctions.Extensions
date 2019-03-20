using Aliencube.AzureFunctions.Extensions.OpenApi.Configurations;

namespace Aliencube.AzureFunctions.FunctionAppCommon.Configurations
{
    /// <summary>
    /// This represents the settings entity from the configurations.
    /// </summary>
    public class AppSettings : OpenApiAppSettingsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettings"/> class.
        /// </summary>
        public AppSettings()
            : base()
        {
        }
    }
}