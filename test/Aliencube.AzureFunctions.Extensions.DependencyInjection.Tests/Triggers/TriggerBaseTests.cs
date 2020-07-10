using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests.Fakes;

using FluentAssertions;

using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="TriggerBase{TLogger}"/> class.
    /// </summary>
    [TestClass]
    public class TriggerBaseTests
    {
        [TestMethod]
        public async Task Given_TraceWriter_Trigger_Should_Be_Invoked()
        {
            var logger = new FakeTraceWriter();

            var trigger = new FakeTriggerWithTraceWriter();

            await trigger.RunAsync(null, logger).ConfigureAwait(false);

            trigger.ResultInvoked.Should().BeOfType<bool>().And.Be(true);
        }

        [TestMethod]
        public async Task Given_ILogger_Trigger_Should_Be_Invoked()
        {
            var logger = new Mock<ILogger>();

            var trigger = new FakeTriggerWithILogger();

            await trigger.RunAsync(null, logger.Object);

            trigger.ResultInvoked.Should().BeOfType<bool>().And.Be(true);
        }
    }
}
