using System;
using System.Collections.Generic;

using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json.Linq;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Extensions
{
    [TestClass]
    public class OpenApiSchemaExtensionsTests
    {
        [TestMethod]
        public void Given_Type_Null_It_Should_Throw_Exception()
        {
            Action action = () => OpenApiSchemaExtensions.ToOpenApiSchema(null, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Given_Type_JObject_It_Should_Return_Result()
        {
            var type = typeof(JObject);

            var schema = OpenApiSchemaExtensions.ToOpenApiSchema(type);

            schema.Type.Should().BeEquivalentTo("object");
        }

        [TestMethod]
        public void Given_Type_Nullable_It_Should_Return_Result()
        {
            var type = typeof(int?);

            var schema = OpenApiSchemaExtensions.ToOpenApiSchema(type);

            schema.Nullable.Should().BeTrue();
            schema.Type.Should().BeEquivalentTo("integer");
            schema.Format.Should().BeEquivalentTo("int32");
        }

        [TestMethod]
        public void Given_GenericIList_Should_Be_Array_With_Matching_Type()
        {
            var list = typeof(IList<string>);

            var result = list.ToOpenApiSchema();

            result.Type.Should().Be("array");
            result.Items.Type.Should().Be("string");
        }

        [TestMethod]
        public void Given_GenericList_Should_Be_Array_With_Matching_Type()
        {
            var list = typeof(List<string>);

            var result = list.ToOpenApiSchema();

            result.Type.Should().Be("array");
            result.Items.Type.Should().Be("string");
        }

        [TestMethod]
        public void Given_UntypedList_Should_Be_Array_With_Object_Type()
        {
            var list = typeof(List<>);

            var result = list.ToOpenApiSchema();

            result.Type.Should().Be("array");
            result.Items.Type.Should().Be("object");
        }

        [TestMethod]
        public void Given_Array_Should_Be_Array()
        {
            var list = typeof(string[]);

            var result = list.ToOpenApiSchema();

            result.Type.Should().Be("array");
            result.Items.Type.Should().Be("string");
        }

        [TestMethod]
        public void Given_Object_Should_Not_Be_Array()
        {
            var list = typeof(string);

            var result = list.ToOpenApiSchema();

            result.Type.Should().NotBe("array");
            result.Items.Should().BeNull();
        }
    }
}