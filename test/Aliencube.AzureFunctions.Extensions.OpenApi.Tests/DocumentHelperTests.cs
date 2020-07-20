using System;

using Aliencube.AzureFunctions.Extensions.OpenApi.Configurations;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests
{
    [TestClass]
    public class DocumentHelperTests
    {
        [TestMethod]
        public void Given_Null_Constructor_Should_Throw_Exception()
        {
            Action action = () => new DocumentHelper(null, null);
            action.Should().Throw<ArgumentNullException>();

            var filter = new RouteConstraintFilter();

            action = () => new DocumentHelper(filter, null);
            action.Should().Throw<ArgumentNullException>();
        }
    }
}
