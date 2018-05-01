using Aliencube.AzureFunctions.Tests.Fakes;

using FluentAssertions;

using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="FunctionBase{TLogger}"/> class.
    /// </summary>
    [TestClass]
    public class FunctionBaseTests
    {
        [TestMethod]
        public void Given_TraceWriter_Property_ShouldNotThrow_Exception()
        {
            var logger = new FakeTraceWriter();

            var function = new FakeFunctionWithTraceWriter() { Log = logger };

            function.Log.Should().BeAssignableTo<TraceWriter>();
        }

        [TestMethod]
        public void Given_ILogger_Property_ShouldNotThrow_Exception()
        {
            var logger = new Mock<ILogger>();

            var function = new FakeFunctionWithILogger() { Log = logger.Object };

            function.Log.Should().BeAssignableTo<ILogger>();
        }
    }
}