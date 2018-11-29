using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionAppV1.Dependencies;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.FunctionAppV1.Functions
{
    public class SampleHttpFunction : FunctionBase<ILogger>, ISampleHttpFunction
    {
        private readonly IMyDependency _dependency;

        public SampleHttpFunction(IMyDependency dependency)
        {
            this._dependency = dependency;
        }

        public override async Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null)
        {
            this.Log.LogInformation("C# HTTP trigger function processed a request.");

            var req = input as HttpRequestMessage;

            string name = req.GetQueryNameValuePairs()
                             .SingleOrDefault(p => p.Key.Equals("name", StringComparison.CurrentCultureIgnoreCase))
                             .Value;

            string requestBody = await req.Content.ReadAsStringAsync().ConfigureAwait(false);
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            this._dependency.Name = name;

            this.Log.LogInformation($"input: {this._dependency.Name}");

            return name != null
                ? (TOutput)Convert.ChangeType(req.CreateResponse(HttpStatusCode.OK, $"Hello, {name}"), typeof(TOutput))
                : (TOutput)Convert.ChangeType(req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body"), typeof(TOutput));
        }
    }
}