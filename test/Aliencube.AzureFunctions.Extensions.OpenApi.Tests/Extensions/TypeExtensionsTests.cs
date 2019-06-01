using System.Collections.Generic;

using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json.Linq;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Extensions
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void Given_IList_Should_Return_True() =>
            typeof(IList<string>).IsOpenApiArray().Should().BeTrue();

        [TestMethod]
        public void Given_List_Should_Return_True() =>
            typeof(List<string>).IsOpenApiArray().Should().BeTrue();

        [TestMethod]
        public void Given_Array_Method_Should_Return_True() =>
            typeof(string[]).IsOpenApiArray().Should().BeTrue();

        [TestMethod]
        public void Given_Object_That_Extends_List_Should_Return_False() =>
            typeof(JObject).IsOpenApiArray().Should().BeFalse();

        [TestMethod]
        public void Given_String_Method_Should_Return_False() =>
            typeof(string).IsOpenApiArray().Should().BeFalse();
    }
}