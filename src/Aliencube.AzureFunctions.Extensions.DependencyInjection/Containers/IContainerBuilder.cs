using System;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions
{
    /// <summary>
    /// This provides interfaces to the <see cref="ContainerBuilder"/> class.
    /// </summary>
    public interface IContainerBuilder
    {
        /// <summary>
        /// Registers <see cref="Module"/> instance.
        /// </summary>
        /// <param name="module"><see cref="Module"/> instance.</param>
        /// <returns>Returns <see cref="IContainerBuilder"/> instance.</returns>
        IContainerBuilder RegisterModule(Module module = null);

        /// <summary>
        /// Registers <see cref="Module"/> instance.
        /// </summary>
        /// <param name="moduleType">Type of module.</param>
        /// <returns>Returns <see cref="IContainerBuilder"/> instance.</returns>
        IContainerBuilder RegisterModule(Type moduleType);

        /// <summary>
        /// Registers <see cref="Module"/> instance.
        /// </summary>
        /// <typeparam name="TModule">Type of module.</typeparam>
        /// <returns>Returns <see cref="IContainerBuilder"/> instance.</returns>
        IContainerBuilder RegisterModule<TModule>() where TModule : Module;

        /// <summary>
        /// Builds the IoC container.
        /// </summary>
        /// <returns>Returns <see cref="IServiceProvider"/> instance.</returns>
        IServiceProvider Build();
    }
}