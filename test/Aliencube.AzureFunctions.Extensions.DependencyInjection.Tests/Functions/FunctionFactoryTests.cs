using Aliencube.AzureFunctions.Tests.Fakes;

using FluentAssertions;

using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="FunctionFactory"/> class.
    /// </summary>
    [TestClass]
    public class FunctionFactoryTests
    {
        [TestMethod]
        public void Given_TraceWriter_Property_ShouldNotThrow_Exception()
        {
            var logger = new FakeTraceWriter();

            var factory = new FunctionFactory(new FakeModule());

            var service = factory.Create<IFakeFunctionWithTraceWriter, TraceWriter>(logger);

            service.Should().NotBeNull();
            service.Should().BeAssignableTo<IFakeFunctionWithTraceWriter>();
            service.Log.Should().BeAssignableTo<TraceWriter>();
        }

        [TestMethod]
        public void Given_ILogger_Property_ShouldNotThrow_Exception()
        {
            var logger = new Mock<ILogger>();

            var factory = new FunctionFactory(new FakeModule());

            var service = factory.Create<IFakeFunctionWithILogger, ILogger>(logger.Object);

            service.Should().NotBeNull();
            service.Should().BeAssignableTo<IFakeFunctionWithILogger>();
            service.Log.Should().BeAssignableTo<ILogger>();
        }

        [TestMethod]
        public void Given_ModuleType_Property_ShouldNotThrow_Exception()
        {
            var logger = new Mock<ILogger>();

            var factory = new FunctionFactory(typeof(FakeModule));

            var service = factory.Create<IFakeFunctionWithILogger, ILogger>(logger.Object);

            service.Should().NotBeNull();
            service.Should().BeAssignableTo<IFakeFunctionWithILogger>();
            service.Log.Should().BeAssignableTo<ILogger>();
        }

        [TestMethod]
        public void Given_ModuleTypeGeneric_Property_ShouldNotThrow_Exception()
        {
            var logger = new Mock<ILogger>();

            var factory = new FunctionFactory<FakeModule>();

            var service = factory.Create<IFakeFunctionWithILogger, ILogger>(logger.Object);

            service.Should().NotBeNull();
            service.Should().BeAssignableTo<IFakeFunctionWithILogger>();
            service.Log.Should().BeAssignableTo<ILogger>();
        }
    }
}