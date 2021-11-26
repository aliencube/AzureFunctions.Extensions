using FluentAssertions;

using Microsoft.OpenApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.AzureFunctions.Extensions.Common.Tests
{
    [TestClass]
    public class OpenApiFormatExtensionsTests
    {
        [DataTestMethod]
        [DataRow(OpenApiFormat.Json, ContentTypes.ApplicationJson)]
        [DataRow(OpenApiFormat.Yaml, ContentTypes.TextVndYaml)]
        public void Given_OpenApiFormat_When_GetContentType_Invoked_Then_It_Should_Return_Result(OpenApiFormat format, string expected)
        {
            var result = OpenApiFormatExtensions.GetContentType(format);

            result.Should().Be(expected);
        }
    }
}
