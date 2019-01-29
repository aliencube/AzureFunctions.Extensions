namespace Aliencube.AzureFunctions.Extensions.OpenApi.Configurations
{
    /// <summary>
    /// This represents the settings entity for extensions in "host.json".
    /// </summary>
    public class ExtensionsSettings
    {
        /// <summary>
        /// Gets or sets the <see cref="HttpSettings"/> instance.
        /// </summary>
        public virtual HttpSettings Http { get; set; } = new HttpSettings();
    }
}