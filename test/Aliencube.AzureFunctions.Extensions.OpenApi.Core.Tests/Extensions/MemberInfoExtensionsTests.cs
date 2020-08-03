using System;
using System.Linq;
using System.Reflection;

using Aliencube.AzureFunctions.Extensions.DependencyInjection.Tests.Fakes;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MemberInfoExtensions = Aliencube.AzureFunctions.Extensions.OpenApi.Core.Extensions.MemberInfoExtensions;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Core.Tests.Extensions
{
    [TestClass]
    public class MemberInfoExtensionsTests
    {
        [TestMethod]
        public void Given_Null_When_ExistsCustomAttribute_Invoked_Then_It_Should_Throw_Exception()
        {
            Action action = () => MemberInfoExtensions.ExistsCustomAttribute<Attribute>(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [DataTestMethod]
        [DataRow("DoSomething", true)]
        [DataRow("DoOtherThing", false)]
        public void Given_MemberInfo_When_ExistsCustomAttribute_Invoked_Then_It_Should_Return_Result(string methodName, bool expected)
        {
            var member = typeof(FakeClass).GetMember(methodName, BindingFlags.Public | BindingFlags.Instance).First();

            var result = MemberInfoExtensions.ExistsCustomAttribute<FakeMethodAttribute>(member);

            result.Should().Be(expected);
        }
    }
}
