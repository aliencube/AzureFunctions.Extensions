using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.Tests.Fakes
{
    /// <summary>
    /// This represents the function entity.
    /// </summary>
    public class FakeFunctionWithILogger : FunctionBase<ILogger>, IFakeFunctionWithILogger
    {
        /// <inheritdoc />
        public async override Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null)
        {
            return await base.InvokeAsync<TInput, TOutput>(input, options).ConfigureAwait(false);
        }
    }
}
