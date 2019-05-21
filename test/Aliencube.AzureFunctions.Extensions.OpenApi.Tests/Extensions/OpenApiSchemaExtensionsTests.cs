using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Extensions
{
    [TestClass]
    public class OpenApiSchemaExtensionsTests
    {
        [TestMethod]
        public void Given_GenericList_Should_Be_Array()
        {
            Type list = typeof(IList<string>);
            var result = list.ToOpenApiSchema();
            result.Type.Should().Be("array");
            result.Items.Type.Should().Be("string");
        }  
        
        [TestMethod]
        public void Given_Array_Should_Be_Array()
        {
            Type list = typeof(string[]);
            var result = list.ToOpenApiSchema();
            result.Type.Should().Be("array");
            result.Items.Type.Should().Be("string");
        }  
        
        [TestMethod]
        public void Given_Object_Should_Not_Be_Array()
        {
            Type list = typeof(String);
            var result = list.ToOpenApiSchema();
            result.Type.Should().NotBe("array");
            result.Items.Should().BeNull();
        }  
    }
}
