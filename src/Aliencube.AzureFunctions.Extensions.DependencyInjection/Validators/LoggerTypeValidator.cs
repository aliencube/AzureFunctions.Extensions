using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection
{
    /// <summary>
    /// This represents the validator entity for logger type.
    /// </summary>
    public static class LoggerTypeValidator
    {
        /// <summary>
        /// Validates the logger type.
        /// </summary>
        /// <typeparam name="TLogger">Type of the logger.</typeparam>
        /// <param name="log">Logger instance</param>
        /// <returns>Returns <c>True</c>, if the logger type is valid; otherwise returns <c>False</c>.</returns>
        public static bool ValidateLoggerType<TLogger>(TLogger log)
        {
            if (log.GetType() == typeof(TraceWriter))
            {
                return true;
            }

            if (log.GetType() == typeof(ILogger))
            {
                return true;
            }

            return false;
        }
    }
}