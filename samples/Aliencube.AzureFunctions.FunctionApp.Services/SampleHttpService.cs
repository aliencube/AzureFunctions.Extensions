using System.Collections.Generic;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.FunctionApp.Models;

namespace Aliencube.AzureFunctions.FunctionApp.Services
{
    /// <summary>
    /// This represents the service entity for the sample HTTP trigger.
    /// </summary>
    public class SampleHttpService : ISampleHttpService
    {
        /// <inheritdoc />
        public async Task<List<SampleResponseModel>> GetSamples()
        {
            var result = new List<SampleResponseModel>()
            {
                new SampleResponseModel(),
            };

            return await Task.FromResult(result).ConfigureAwait(false);
        }
    }
}
