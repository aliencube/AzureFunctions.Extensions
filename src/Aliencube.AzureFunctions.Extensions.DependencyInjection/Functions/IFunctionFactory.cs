namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions
{
    /// <summary>
    /// This provides interfaces to the <see cref="FunctionFactory"/> instance.
    /// </summary>
    public interface IFunctionFactory
    {
        /// <summary>
        /// Creates a function instance from the IoC container.
        /// </summary>
        /// <typeparam name="TFunction">Type of function.</typeparam>
        /// <typeparam name="TLogger">Type of logger.</typeparam>
        /// <param name="log"><see cref="TLogger"/> instance.</param>
        /// <returns>Returns the function instance.</returns>
        TFunction Create<TFunction, TLogger>(TLogger log) where TFunction : IFunction<TLogger>;
    }
}