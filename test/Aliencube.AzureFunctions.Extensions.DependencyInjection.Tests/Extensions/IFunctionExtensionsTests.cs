using Aliencube.AzureFunctions.Extensions.DependencyInjection.Extensions;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests.Fakes;

using FluentAssertions;

using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="ConfigurationBinderExtensions"/> class.
    /// </summary>
    [TestClass]
    public class IFunctionExtensionsTests
    {
        [TestMethod]
        public void Given_Logger_AddLogger_Should_Return_Instance()
        {
            var logger = new Mock<ILogger>();

            var function = new FakeFunctionWithILogger()
                               .AddLogger(logger.Object);

            var result = function.Log;

            result.Should().BeAssignableTo<ILogger>();
        }
    }
}