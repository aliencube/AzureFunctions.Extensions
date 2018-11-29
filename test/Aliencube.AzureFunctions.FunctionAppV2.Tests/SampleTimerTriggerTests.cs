using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.FunctionAppV2.Functions;

using FluentAssertions;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Timers;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Aliencube.AzureFunctions.FunctionAppV2.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="SampleTimerTrigger"/> class.
    /// </summary>
    [TestClass]
    public class SampleTimerTriggerTests
    {
        [TestMethod]
        public async Task Given_Timer_Run_Should_Return_Result()
        {
            var result = true;

            var function = new Mock<ISampleTimerFunction>();
            function.Setup(p => p.InvokeAsync<TimerInfo, bool>(It.IsAny<TimerInfo>(), It.IsAny<FunctionOptionsBase>())).ReturnsAsync(result);

            var factory = new Mock<IFunctionFactory>();
            factory.Setup(p => p.Create<ISampleTimerFunction, ILogger>(It.IsAny<ILogger>())).Returns(function.Object);
            factory.SetupProperty(p => p.ResultInvoked);

            SampleTimerTrigger.Factory = factory.Object;

            var schedule = new Mock<TimerSchedule>();
            var timer = new TimerInfo(schedule.Object, new ScheduleStatus());
            var collector = new Mock<IAsyncCollector<string>>();
            var log = new Mock<ILogger>();

            await SampleTimerTrigger.Run(timer, collector.Object, log.Object).ConfigureAwait(false);

            factory.Object.ResultInvoked.Should().BeOfType<bool>().And.Be(true);
        }
    }
}