using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;

using FluentAssertions;

using Microsoft.OpenApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public async Task Given_VersionAndFormat_RenderAsync_Should_Return_Result()
        {
            var helper = new Mock<IDocumentHelper>();
            var doc = new Document(helper.Object);

            var result = await doc.InitialiseDocument()
                                  .RenderAsync(OpenApiSpecVersion.OpenApi2_0, OpenApiFormat.Json);

            result.Should().StartWithEquivalent("{");
        }
    }
}
