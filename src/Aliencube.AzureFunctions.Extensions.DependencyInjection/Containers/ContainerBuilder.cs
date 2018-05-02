using System;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Extensions.DependencyInjection;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection
{
    /// <summary>
    /// This represents the builder entity for the IoC container.
    /// </summary>
    public class ContainerBuilder : IContainerBuilder
    {
        private readonly IServiceCollection _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerBuilder"/> class.
        /// </summary>
        public ContainerBuilder()
        {
            this._services = new ServiceCollection();
        }

        /// <inheritdoc />
        public IContainerBuilder RegisterModule(Module module = null)
        {
            if (module == null)
            {
                module = new Module();
            }

            module.Load(this._services);

            return this;
        }

        /// <inheritdoc />
        public IContainerBuilder RegisterModule(Type moduleType)
        {
            var module = (Module)Activator.CreateInstance(moduleType);

            return this.RegisterModule(module);
        }

        /// <inheritdoc />
        public IContainerBuilder RegisterModule<TModule>() where TModule : Module
        {
            var module = Activator.CreateInstance<TModule>();

            return this.RegisterModule(module);
        }

        /// <inheritdoc />
        public IServiceProvider Build()
        {
            var container = this._services.BuildServiceProvider();

            return container;
        }
    }
}