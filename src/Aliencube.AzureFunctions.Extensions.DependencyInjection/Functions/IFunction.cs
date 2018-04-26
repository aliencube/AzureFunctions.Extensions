using System.Threading.Tasks;

using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions
{
    /// <summary>
    /// This provides interfaces to functions.
    /// </summary>
    /// <typeparam name="TLogger">Type of logger.</typeparam>
    public interface IFunction<TLogger>
    {
        /// <summary>
        /// Gets or sets the <see cref="TLogger"/> instance. This MUST be either <see cref="TraceWriter"/> or <see cref="ILogger"/> type.
        /// </summary>
        TLogger Log { get; set; }

        /// <summary>
        /// Invokes the function.
        /// </summary>
        /// <typeparam name="TInput">Type of input.</typeparam>
        /// <typeparam name="TOutput">Type of output.</typeparam>
        /// <param name="input">Input instance.</param>
        /// <param name="options"><see cref="FunctionOptionsBase"/> instance.</param>
        /// <returns>Returns output instance.</returns>
        Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null);
    }
}