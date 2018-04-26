using Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests.Fixtures;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="ContainerBuilder"/> class.
    /// </summary>
    [TestClass]
    public class ContainerBuilderTests
    {
        [TestMethod]
        public void Given_NullModule_Container_Should_Resolve_Nothing()
        {
            var container = new ContainerBuilder().RegisterModule(null).Build();

            var resolved = container.GetService<IFakeInterface>();

            resolved.Should().BeNull();
        }

        [TestMethod]
        public void Given_FakeModule_Container_Should_Resolve_Instance()
        {
            var container = new ContainerBuilder().RegisterModule(new FakeModule()).Build();

            var resolved = container.GetService<IFakeInterface>();

            resolved.Should().NotBeNull();
            resolved.Should().BeAssignableTo<IFakeInterface>();
        }
    }
}
