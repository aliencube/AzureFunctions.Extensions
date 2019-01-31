using System;
using System.Net;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Attributes
{
    [TestClass]
    public class OpenApiResponseBodyAttributeTests
    {
        [TestMethod]
        public void Given_Null_Constructor_Should_Throw_Exception()
        {
            var statusCode = HttpStatusCode.OK;

            Action action = () => new OpenApiResponseBodyAttribute(statusCode, null, null);
            action.Should().Throw<ArgumentNullException>();

            action = () => new OpenApiResponseBodyAttribute(statusCode, "hello world", null);
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Given_Value_Property_Should_Return_Value()
        {
            var statusCode = HttpStatusCode.OK;
            var contentType = "Hello World";
            var bodyType = typeof(object);
            var attribute = new OpenApiResponseBodyAttribute(statusCode, contentType, bodyType);

            attribute.StatusCode.Should().Be(statusCode);
            attribute.ContentType.Should().BeEquivalentTo(contentType);
            attribute.BodyType.Should().Be(bodyType);
            attribute.Description.Should().BeNullOrWhiteSpace();
        }
    }
}
