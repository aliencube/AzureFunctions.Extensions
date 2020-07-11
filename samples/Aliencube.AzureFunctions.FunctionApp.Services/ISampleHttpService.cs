using System.Collections.Generic;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.FunctionApp.Models;

namespace Aliencube.AzureFunctions.FunctionApp.Services
{
    /// <summary>
    /// This provides interfaces to <see cref="SampleHttpService"/>.
    /// </summary>
    public interface ISampleHttpService
    {
        /// <summary>
        /// Gets the list of <see cref="SampleResponseModel"/> objects.
        /// </summary>
        /// <returns>Returns the list of <see cref="SampleResponseModel"/> objects.</returns>
        Task<List<SampleResponseModel>> GetSamples();
    }
}
