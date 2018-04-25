using Microsoft.Extensions.DependencyInjection;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions
{
    /// <summary>
    /// This represents the module entity to register dependencies.
    /// </summary>
    public class Module
    {
        /// <summary>
        /// Registers dependencies.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
        public virtual void Load(IServiceCollection services)
        {
            return;
        }
    }
}
