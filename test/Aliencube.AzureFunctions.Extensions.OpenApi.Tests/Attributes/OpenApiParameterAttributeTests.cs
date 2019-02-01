using System;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

using FluentAssertions;

using Microsoft.OpenApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Attributes
{
    [TestClass]
    public class OpenApiParameterAttributeTests
    {
        [TestMethod]
        public void Given_Null_Constructor_Should_Throw_Exception()
        {
            Action action = () => new OpenApiParameterAttribute(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Given_Value_Property_Should_Return_Value()
        {
            var name = "Hello World";
            var attribute = new OpenApiParameterAttribute(name);

            attribute.Name.Should().BeEquivalentTo(name);
            attribute.Description.Should().BeNullOrWhiteSpace();
            attribute.Type.Should().Be<string>();
            attribute.In.Should().Be(ParameterLocation.Path);
            attribute.Required.Should().Be(false);
        }
    }
}
