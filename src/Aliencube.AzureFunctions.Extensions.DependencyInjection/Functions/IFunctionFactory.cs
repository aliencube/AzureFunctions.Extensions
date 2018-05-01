using System;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions
{
    /// <summary>
    /// This provides interfaces to the <see cref="FunctionFactory"/> instance.
    /// </summary>
    public interface IFunctionFactory
    {
        /// <summary>
        /// Gets or sets a result from the function invocation. This is particularly useful, if a trigger is <c>void</c> or returns <see cref="Type"/>.
        /// </summary>
        object ResultInvoked { get; set; }

        /// <summary>
        /// Creates a function instance from the IoC container.
        /// </summary>
        /// <typeparam name="TFunction">Type of function.</typeparam>
        /// <typeparam name="TLogger">Type of logger.</typeparam>
        /// <param name="log">Logger instance.</param>
        /// <returns>Returns the function instance.</returns>
        TFunction Create<TFunction, TLogger>(TLogger log) where TFunction : IFunction<TLogger>;
    }
}