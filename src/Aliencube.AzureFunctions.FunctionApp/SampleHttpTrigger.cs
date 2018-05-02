using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionApp.Functions;
using Aliencube.AzureFunctions.FunctionApp.Modules;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionApp
{
    public static class SampleHttpTrigger
    {
        public static IFunctionFactory Factory = new FunctionFactory<AppModule>();

        [FunctionName("SampleHttpTrigger")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            var result = await Factory.Create<ISampleHttpFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequest, IActionResult>(req)
                                      .ConfigureAwait(false);

            return result;
        }
    }
}