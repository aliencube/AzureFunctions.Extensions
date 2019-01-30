using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionAppV1.Dependencies;
using Aliencube.AzureFunctions.FunctionAppV1.Models;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.FunctionAppV1.Functions
{
    /// <summary>
    /// This represents the function entity for <see cref="SampleHttpTrigger"/>.
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

            var req = input as HttpRequestMessage;
            var request = (SampleRequestModel)null;

            var serialised = await req.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!string.IsNullOrWhiteSpace(serialised))
            {
                request = JsonConvert.DeserializeObject<SampleRequestModel>(serialised);
            }

            string name = req.GetQueryNameValuePairs()
                             .SingleOrDefault(p => p.Key.Equals("name", StringComparison.CurrentCultureIgnoreCase))
                             .Value;

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

            var content = new StringContent(JsonConvert.SerializeObject(result), Encoding.UTF8, "application/json");
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };

            return (TOutput)Convert.ChangeType(response, typeof(TOutput));
        }
    }
}