using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.Common.Tests.Models;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.Extensions.Common.Tests
{
    [TestClass]
    public class HttpRequestExtensionsTests
    {
        [DataTestMethod]
        [DataRow(SourceFrom.None)]
        public void Given_InvalidRequestSource_When_To_Invoked_Then_It_Should_Throw_Exception(SourceFrom source)
        {
            var req = new Mock<HttpRequest>();

            Func<Task> func = async () => await HttpRequestExtensions.To<IHeaderDictionary>(req.Object, source).ConfigureAwait(false);

            func.Should().ThrowAsync<InvalidOperationException>();
        }

        [DataTestMethod]
        [DataRow(SourceFrom.Header)]
        [DataRow(SourceFrom.Query)]
        public void Given_InvalidType_When_To_Invoked_Then_It_Should_Throw_Exception(SourceFrom source)
        {
            var req = new Mock<HttpRequest>();

            Func<Task> func = async () => await HttpRequestExtensions.To<IDictionary>(req.Object, source).ConfigureAwait(false);

            func.Should().ThrowAsync<InvalidOperationException>();
        }

        [DataTestMethod]
        [DataRow("x-secret-key", "hello-world")]
        public async Task Given_Header_When_To_Invoked_Then_It_Should_Return_Result(string key, string value)
        {
            var store = new Dictionary<string, StringValues>() { { key, value } };
            var headers = new HeaderDictionary(store);

            var req = new Mock<HttpRequest>();
            req.SetupGet(p => p.Headers).Returns(headers);

            var result = await HttpRequestExtensions.To<FakeRequestHeader>(req.Object, SourceFrom.Header).ConfigureAwait(false);

            result.Should().NotBeNull();
            result.SecretKey.Should().Be(value);
        }

        [DataTestMethod]
        [DataRow("key", "value")]
        public async Task Given_Query_When_To_Invoked_Then_It_Should_Return_Result(string key, string value)
        {
            var store = new Dictionary<string, StringValues>() { { key, value } };
            var queries = new QueryCollection(store);

            var req = new Mock<HttpRequest>();
            req.SetupGet(p => p.Query).Returns(queries);

            var result = await HttpRequestExtensions.To<FakeRequestQuery>(req.Object, SourceFrom.Query).ConfigureAwait(false);

            result.Should().NotBeNull();
            result.Key.Should().Be(value);
        }

        [DataTestMethod]
        [DataRow("hello world")]
        public async Task Given_Payload_When_To_Invoked_Then_It_Should_Return_Result(string message)
        {
            var payload = new FakeRequestModel() { Message = message };
            var serialised = JsonConvert.SerializeObject(payload);
            var bytes = Encoding.UTF8.GetBytes(serialised);
            var stream = new MemoryStream();
            await stream.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
            stream.Position = 0;

            var req = new Mock<HttpRequest>();
            req.SetupGet(p => p.Body).Returns(stream);

            var result = await HttpRequestExtensions.To<FakeRequestModel>(req.Object, SourceFrom.Body).ConfigureAwait(false);

            result.Message.Should().Be(message);

            await stream.DisposeAsync().ConfigureAwait(false);
        }

        [TestMethod]
        public async Task Given_Null_Payload_When_ToMultipartFormDataContent_Invoked_Then_It_Should_Return_Null()
        {
            var files = new FormFileCollection();

            var form = new Mock<IFormCollection>();
            form.SetupGet(p => p.Files).Returns(files);

            var req = new Mock<HttpRequest>();
            req.SetupGet(p => p.Form).Returns(form.Object);

            var result = await HttpRequestExtensions.ToMultipartFormDataContent(req.Object).ConfigureAwait(false);

            result.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("hello", "world.jpg")]
        public async Task Given_Payload_When_ToMultipartFormDataContent_Invoked_Then_It_Should_Return_Result(string name, string filename)
        {
            var file = new Mock<IFormFile>();
            file.SetupGet(p => p.Name).Returns(name);
            file.SetupGet(p => p.FileName).Returns(filename);
            file.Setup(p => p.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var files = new FormFileCollection
            {
                file.Object
            };

            var form = new Mock<IFormCollection>();
            form.SetupGet(p => p.Files).Returns(files);

            var req = new Mock<HttpRequest>();
            req.SetupGet(p => p.Form).Returns(form.Object);

            var result = await HttpRequestExtensions.ToMultipartFormDataContent(req.Object).ConfigureAwait(false);

            result.Should().NotBeNull();
        }
    }
}
