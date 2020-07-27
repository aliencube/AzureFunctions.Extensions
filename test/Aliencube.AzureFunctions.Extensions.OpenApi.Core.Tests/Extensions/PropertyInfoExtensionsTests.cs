using System.Reflection;

using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Tests.Fakes;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json.Serialization;

using PropertyInfoExtensions = Aliencube.AzureFunctions.Extensions.OpenApi.Core.Extensions.PropertyInfoExtensions;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Core.Tests.Extensions
{
    [TestClass]
    public class PropertyInfoExtensionsTests
    {
        [TestMethod]
        public void Given_Property_When_GetJsonPropertyName_Invoked_Then_It_Should_Return_PropertyName()
        {
            var name = "FakeProperty";
            var jsonPropertyName = "FakeProperty";
            var property = typeof(FakeModel).GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            var namingStrategy = new DefaultNamingStrategy();

            var result = PropertyInfoExtensions.GetJsonPropertyName(property, namingStrategy);

            result.Should().Be(jsonPropertyName);
        }

        [TestMethod]
        public void Given_Property_When_GetJsonPropertyName_Invoked_Then_It_Should_Return_JsonPropertyName()
        {
            var name = "FakeProperty2";
            var jsonPropertyName = "anotherFakeProperty";
            var property = typeof(FakeModel).GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            var namingStrategy = new DefaultNamingStrategy();

            var result = PropertyInfoExtensions.GetJsonPropertyName(property, namingStrategy);

            result.Should().Be(jsonPropertyName);
        }
    }
}
