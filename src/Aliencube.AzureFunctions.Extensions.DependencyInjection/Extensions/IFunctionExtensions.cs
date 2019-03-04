using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Extensions
{
    /// <summary>
    /// This represents the extension entity for <see cref="IFunction{TLogger}"/> instance.
    /// </summary>
    public static class IFunctionExtensions
    {
        /// <summary>
        /// Adds the logger instance to the given function instance.
        /// </summary>
        /// <typeparam name="TFunction">Type of function instance, inheriting <see cref="IFunction{TLogger}"/>.</typeparam>
        /// <typeparam name="TLogger">Type of the logger.</typeparam>
        /// <param name="function">The function instance.</param>
        /// <param name="log">The logger instance.</param>
        /// <returns>The function instance.</returns>
        public static TFunction AddLogger<TFunction, TLogger>(this TFunction function, TLogger log)
            where TFunction : IFunction<TLogger>
        {
            function.Log = log;

            return function;
        }
    }
}
