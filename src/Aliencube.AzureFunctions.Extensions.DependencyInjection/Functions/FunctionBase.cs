using System;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions
{
    /// <summary>
    /// This represents the base function entity for other functions to inherit.
    /// </summary>
    /// <typeparam name="TLogger">Type of logger.</typeparam>
    public abstract class FunctionBase<TLogger> : IFunction<TLogger>
    {
        private TLogger _logger;

        /// <inheritdoc />
        public TLogger Log
        {
            get => this._logger;

            set
            {
                if (!LoggerTypeValidator.ValidateLoggerType(value))
                {
                    throw new InvalidOperationException("Invalid logger type");
                }

                this._logger = value;
            }
        }

        /// <inheritdoc />
        public virtual Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null) => Task.FromResult(default(TOutput));
    }
}