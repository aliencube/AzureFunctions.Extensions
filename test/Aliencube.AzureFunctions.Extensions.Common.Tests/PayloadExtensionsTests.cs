using Aliencube.AzureFunctions.Extensions.Common.Tests.Models;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.Extensions.Common.Tests
{
    [TestClass]
    public class PayloadExtensionsTests
    {
        [TestMethod]
        public void Given_Null_When_ToJson_Invoked_Then_It_Should_Return_Null()
        {
            var result = PayloadExtensions.ToJson<FakeRequestModel>(null!);

            result.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("hello world")]
        public void Given_Payload_When_ToJson_Invoked_Then_It_Should_Return_Result(string message)
        {
            var payload = new FakeRequestModel() { Message = message };
            var serialised = JsonConvert.SerializeObject(payload);

            var result = PayloadExtensions.ToJson(payload);

            result.Should().Be(serialised);
        }

        [TestMethod]
        public void Given_Null_When_FromJson_Invoked_Then_It_Should_Return_Null()
        {
            var result = PayloadExtensions.FromJson<FakeRequestModel>(null);

            result.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("hello world")]
        public void Given_Payload_When_FromJson_Invoked_Then_It_Should_Return_Result(string message)
        {
            var payload = new FakeRequestModel() { Message = message };
            var serialised = JsonConvert.SerializeObject(payload);

            var result = PayloadExtensions.FromJson<FakeRequestModel>(serialised);

            result.Message.Should().Be(message);
        }
    }
}
