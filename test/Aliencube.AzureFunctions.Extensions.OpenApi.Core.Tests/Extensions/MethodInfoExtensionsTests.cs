using System;
using System.Reflection;

using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Tests.Fakes;

using FluentAssertions;

using Microsoft.Azure.WebJobs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MethodInfoExtensions = Aliencube.AzureFunctions.Extensions.OpenApi.Core.Extensions.MethodInfoExtensions;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Extensions
{
    [TestClass]
    public class MethodInfoExtensionsTests
    {
        [TestMethod]
        public void Given_Null_When_GetFunctionName_Invoked_Then_It_Should_Throw_Exception()
        {
            Action action = () => MethodInfoExtensions.GetFunctionName(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [DataTestMethod]
        [DataRow("DoSomething", "FakeFunction")]
        public void Given_MemberInfo_When_ExistsCustomAttribute_Invoked_Then_It_Should_Return_Result(string methodName, string expected)
        {
            var method = typeof(FakeHttpTrigger).GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);

            var result = MethodInfoExtensions.GetFunctionName(method);

            result.Should().BeOfType<FunctionNameAttribute>();
            result.Name.Should().Be(expected);
        }

        [TestMethod]
        public void Given_Null_When_GetHttpTrigger_Invoked_Then_It_Should_Throw_Exception()
        {
            Action action = () => MethodInfoExtensions.GetHttpTrigger(null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
