using System;
#if NET461
using System.Net.Http;
#endif
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;

using FluentAssertions;

#if NETCOREAPP2_0
using Microsoft.AspNetCore.Http;
#endif
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Newtonsoft.Json.Linq;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void Given_Null_Constructor_Should_Throw_Exception()
        {
            Action action = () => new Document(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task Given_VersionAndFormat_RenderAsync_Should_Return_Result()
        {
            var helper = new Mock<IDocumentHelper>();
            var doc = new Document(helper.Object);

            var result = await doc.InitialiseDocument()
                                  .RenderAsync(OpenApiSpecVersion.OpenApi2_0, OpenApiFormat.Json);

            dynamic json = JObject.Parse(result);

            ((string)json?.swagger).Should().BeEquivalentTo("2.0");
        }

        [TestMethod]
        public async Task Given_Metadata_RenderAsync_Should_Return_Result()
        {
            var helper = new Mock<IDocumentHelper>();

            var title = "hello world";
            var info = new OpenApiInfo() { Title = title };

            var doc = new Document(helper.Object);

            var result = await doc.InitialiseDocument()
                                  .AddMetadata(info)
                                  .RenderAsync(OpenApiSpecVersion.OpenApi2_0, OpenApiFormat.Json);

            dynamic json = JObject.Parse(result);

            ((string)json?.info?.title).Should().BeEquivalentTo(title);
        }

        [TestMethod]
        public async Task Given_ServerDetails_RenderAsync_Should_Return_Result()
        {
            var helper = new Mock<IDocumentHelper>();

            var scheme = "https";
            var host = "localhost";
            var routePrefix = "api";
            var url = $"{scheme}://{host}";
#if NET461
            var uri = new Uri(url);
            var req = new HttpRequestMessage() { RequestUri = uri };
#elif NETCOREAPP2_0
            var req = new Mock<HttpRequest>();
            req.SetupGet(p => p.Scheme).Returns(scheme);
            req.SetupGet(p => p.Host).Returns(new HostString(host));
#endif
            var doc = new Document(helper.Object);

            var result = await doc.InitialiseDocument()
#if NET461
                                  .AddServer(req, routePrefix)
#elif NETCOREAPP2_0
                                  .AddServer(req.Object, routePrefix)
#endif
                                  .RenderAsync(OpenApiSpecVersion.OpenApi2_0, OpenApiFormat.Json);

            dynamic json = JObject.Parse(result);

            ((string)json?.host).Should().BeEquivalentTo(host);
            ((string)json?.basePath).Should().BeEquivalentTo($"/{routePrefix}");
            ((string)json?.schemes[0]).Should().BeEquivalentTo(scheme);
        }
    }
}
