using Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests.Fakes;

using FluentAssertions;

using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="LoggerTypeValidator"/> class.
    /// </summary>
    [TestClass]
    public class LoggerTypeValidatorTests
    {
        [TestMethod]
        public void Given_InvalidType_ValidateLoggerType_ShouldReturn_False()
        {
            var logger = new FakeClass();

            var result = LoggerTypeValidator.ValidateLoggerType(logger);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void Given_TraceWriter_Property_ShouldNotThrow_Exception()
        {
            var logger = new FakeTraceWriter();

            var result = LoggerTypeValidator.ValidateLoggerType(logger);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void Given_ILogger_Property_ShouldNotThrow_Exception()
        {
            var logger = new Mock<ILogger>();

            var result = LoggerTypeValidator.ValidateLoggerType(logger.Object);

            result.Should().BeTrue();
        }
    }
}