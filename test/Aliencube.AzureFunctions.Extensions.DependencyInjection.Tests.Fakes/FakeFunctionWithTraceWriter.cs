using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Azure.WebJobs.Host;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests.Fakes
{
    /// <summary>
    /// This represents the function entity.
    /// </summary>
    public class FakeFunctionWithTraceWriter : FunctionBase<TraceWriter>, IFakeFunctionWithTraceWriter
    {
        /// <inheritdoc />
        public async override Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null)
        {
            return await base.InvokeAsync<TInput, TOutput>(input, options).ConfigureAwait(false);
        }
    }
}
