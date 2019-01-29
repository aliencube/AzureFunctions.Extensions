using System.Threading.Tasks;

using Microsoft.OpenApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var helper = new DocumentHelper();
            var doc = new Document(helper);
            var result = await doc.RenderAsync(OpenApiSpecVersion.OpenApi2_0, OpenApiFormat.Json);
        }
    }
}
