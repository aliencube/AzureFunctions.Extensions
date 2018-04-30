using System;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.DependencyInjection;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection
{
    /// <summary>
    /// This represents the factory entity for functions.
    /// </summary>
    public class FunctionFactory : IFunctionFactory
    {
        private readonly IServiceProvider _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionFactory"/> class.
        /// </summary>
        /// <param name="module"><see cref="Module"/> instance.</param>
        public FunctionFactory(Module module = null)
        {
            this._container = new ContainerBuilder()
                                  .RegisterModule(module)
                                  .Build();
        }

        /// <inheritdoc />
        public object ResultInvoked { get; set; }

        /// <inheritdoc />
        public TFunction Create<TFunction, TLogger>(TLogger log) where TFunction : IFunction<TLogger>
        {
            if (log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            if (!LoggerTypeValidator.ValidateLoggerType(log))
            {
                throw new ArgumentException("Invalid logger type");
            }

            var function = this._container.GetService<TFunction>();
            function.Log = log;

            return function;
        }
    }
}