using Aliencube.AzureFunctions.Extensions.OpenApi.Configurations;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Configurations
{
    [TestClass]
    public class HttpSettingsTests
    {
        [TestMethod]
        public void Given_Value_Property_Should_Return_Value()
        {
            var settings = new HttpSettings();

            settings.RoutePrefix.Should().BeEquivalentTo("api");
        }
    }
}
