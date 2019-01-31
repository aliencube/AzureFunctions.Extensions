using System;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Extensions
{
    [TestClass]
    public class SwaggerUIExtensionsTests
    {
        [TestMethod]
        public void Given_Null_Method_Should_Throw_Exception()
        {
            Func<Task> func = async () => await SwaggerUIExtensions.RenderAsync(null, null).ConfigureAwait(false);
            func.Should().Throw<ArgumentNullException>();
        }
    }
}
