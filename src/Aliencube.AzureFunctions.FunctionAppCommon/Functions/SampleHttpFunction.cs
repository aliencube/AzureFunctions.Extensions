using System;

#if NETSTANDARD2_0
using System.IO;
#endif

#if NET461
using System.Linq;
#endif

using System.Net;

#if NET461
using System.Net.Http;
using System.Text;
#endif

using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionAppCommon.Dependencies;
using Aliencube.AzureFunctions.FunctionAppCommon.Models;

#if NETSTANDARD2_0
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
#endif

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.FunctionAppCommon.Functions
{
    /// <summary>
    /// This represents the function entity for sample HTTP trigger.
    /// </summary>
    public class SampleHttpFunction : FunctionBase<ILogger>, ISampleHttpFunction
    {
        private readonly IMyDependency _dependency;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleHttpFunction"/> class.
        /// </summary>
        /// <param name="dependency"><see cref="IMyDependency"/> instance.</param>
        public SampleHttpFunction(IMyDependency dependency)
        {
            this._dependency = dependency;
        }

        /// <inheritdoc />
        public override async Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null)
        {
            this.Log.LogInformation("C# HTTP trigger function processed a request.");
#if NET461
            var req = input as HttpRequestMessage;
#elif NETSTANDARD2_0
            var req = input as HttpRequest;
#endif
            var request = (SampleRequestModel)null;

#if NET461
            var serialised = await req.Content.ReadAsStringAsync().ConfigureAwait(false);
#elif NETSTANDARD2_0
            var serialised = await new StreamReader(req.Body).ReadToEndAsync().ConfigureAwait(false);
#endif
            if (!string.IsNullOrWhiteSpace(serialised))
            {
                request = JsonConvert.DeserializeObject<SampleRequestModel>(serialised);
            }

#if NET461
            string name = req.GetQueryNameValuePairs()
                             .SingleOrDefault(p => p.Key.Equals("name", StringComparison.CurrentCultureIgnoreCase))
                             .Value;
#elif NETSTANDARD2_0
            string name = req.Query["name"];
#endif

            this._dependency.Name = name;

            this.Log.LogInformation($"input: {this._dependency.Name}");

            var result = new SampleResponseModel()
                               {
                                   Id = request == null ? Guid.NewGuid() : Guid.Parse(request.Id),
                                   Name = string.IsNullOrWhiteSpace(name) ? "Sample" : name,
                                   Description = "Ignored",
                                   Sub = new SubSampleResponseModel() { Value = int.MaxValue },
                                   Collection = { { "hello", "world" } },
                                   Items = { { int.MinValue } }
                               };
#if NET461
            var content = new StringContent(JsonConvert.SerializeObject(result), Encoding.UTF8, "application/json");
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };

            return (TOutput)Convert.ChangeType(response, typeof(TOutput));
#elif NETSTANDARD2_0
            var content = new ContentResult()
                              {
                                  Content = JsonConvert.SerializeObject(result),
                                  ContentType = "application/json",
                                  StatusCode = (int)HttpStatusCode.OK
                              };

            return (TOutput)(IActionResult)content;
#endif
        }
    }
}