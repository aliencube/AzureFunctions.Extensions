using System.Net.Http;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionAppV1.Functions;
using Aliencube.AzureFunctions.FunctionAppV1.Modules;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionAppV1
{
    public static class SampleHttpTrigger
    {
        public static IFunctionFactory Factory = new FunctionFactory<AppModule>();

        [FunctionName(nameof(GetSample))]
        public static async Task<HttpResponseMessage> GetSample([HttpTrigger(AuthorizationLevel.Function, "get", Route = "samples")]HttpRequestMessage req, ILogger log)
        {
            var result = await Factory.Create<ISampleHttpFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequestMessage, HttpResponseMessage>(req)
                                      .ConfigureAwait(false);

            return result;
        }

        [FunctionName(nameof(PostSample))]
        public static async Task<HttpResponseMessage> PostSample([HttpTrigger(AuthorizationLevel.Function, "post", Route = "samples")]HttpRequestMessage req, ILogger log)
        {
            var result = await Factory.Create<ISampleHttpFunction, ILogger>(log)
                                      .InvokeAsync<HttpRequestMessage, HttpResponseMessage>(req)
                                      .ConfigureAwait(false);

            return result;
        }
    }
}