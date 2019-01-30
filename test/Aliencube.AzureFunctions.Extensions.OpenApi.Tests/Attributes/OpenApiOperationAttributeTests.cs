using System.Linq;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Attributes
{
    [TestClass]
    public class OpenApiOperationAttributeTests
    {
        [TestMethod]
        public void Given_Value_Properties_Should_Return_Value()
        {
            var opId = "Lorem Ipsum";
            var tag1 = "hello";
            var tag2 = "world";
            var attribute = new OpenApiOperationAttribute(opId, tag1, tag2);

            attribute.OperationId.Should().BeEquivalentTo(opId);
            attribute.Tags.First().Should().BeEquivalentTo(tag1);
            attribute.Tags.Last().Should().BeEquivalentTo(tag2);
        }
    }
}
