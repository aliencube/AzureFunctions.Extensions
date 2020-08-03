using System.Linq;
using System.Reflection;

using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Enums;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Core.Tests.Enums
{
    [TestClass]
    public class OpenApiFormatTypeTests
    {
        [DataTestMethod]
        [DataRow("Json")]
        [DataRow("Yaml")]
        public void Given_Enum_Should_Have_Member(string memberName)
        {
            var members = typeof(OpenApiFormatType).GetMembers().Select(p => p.Name);

            members.Should().Contain(memberName);
        }

        [DataTestMethod]
        [DataRow("Json", "json")]
        [DataRow("Yaml", "yaml")]
        public void Given_Enum_Should_Have_Decorator(string memberName, string displayName)
        {
            var member = this.GetMemberInfo(memberName);
            var attribute = member.GetCustomAttribute<DisplayAttribute>(inherit: false);

            attribute.Should().NotBeNull();
            attribute.Name.Should().Be(displayName);
        }

        private MemberInfo GetMemberInfo(string name)
        {
            var member = typeof(OpenApiFormatType).GetMember(name).First();

            return member;
        }
    }
}
