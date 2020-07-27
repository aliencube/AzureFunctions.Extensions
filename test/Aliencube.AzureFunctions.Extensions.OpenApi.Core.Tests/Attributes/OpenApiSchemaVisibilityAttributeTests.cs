using System;

using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Enums;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Core.Tests.Attributes
{
    [TestClass]
    public class OpenApiSchemaVisibilityAttributeTests
    {
        [TestMethod]
        public void Given_InvalidValue_Constructor_Should_Throw_Exception()
        {
            Action action = () => new OpenApiSchemaVisibilityAttribute(OpenApiVisibilityType.Undefined);
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void Given_Value_Property_Should_Return_Value()
        {
            var visibility = OpenApiVisibilityType.Important;

            var attribute = new OpenApiSchemaVisibilityAttribute(visibility);

            attribute.Visibility.Should().Be(visibility);
        }
    }
}
