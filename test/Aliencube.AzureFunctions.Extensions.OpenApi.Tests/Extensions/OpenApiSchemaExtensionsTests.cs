using System;

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
    }
}