using System;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.CLI.Tests
{
    [TestClass]
    public class ProjectInfoTests
    {
        [TestMethod]
        public void Given_Null_Parameters_When_Instntiated_Then_It_Should_Throw_Exception()
        {
            Action action = () => new ProjectInfo(null, null, null);
            action.Should().Throw<ArgumentNullException>();

            action = () => new ProjectInfo("abc", null, null);
            action.Should().Throw<ArgumentNullException>();

            action = () => new ProjectInfo("abc", "abc", null);
            action.Should().Throw<ArgumentNullException>();
        }
    }
}
