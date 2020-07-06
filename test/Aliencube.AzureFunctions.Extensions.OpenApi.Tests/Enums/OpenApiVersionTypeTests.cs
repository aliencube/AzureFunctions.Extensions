using System.Linq;
using System.Reflection;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Enums
{
    [TestClass]
    public class OpenApiVersionTypeTests
    {
        [DataTestMethod]
        [DataRow("V2")]
        [DataRow("V3")]
        public void Given_Enum_Should_Have_Member(string memberName)
        {
            var members = typeof(OpenApiVersionType).GetMembers().Select(p => p.Name);

            members.Should().Contain(memberName);
        }

        [DataTestMethod]
        [DataRow("V2", "v2")]
        [DataRow("V3", "v3")]
        public void Given_Enum_Should_Have_Decorator(string memberName, string displayName)
        {
            var member = this.GetMemberInfo(memberName);
            var attribute = member.GetCustomAttribute<DisplayAttribute>(inherit: false);

            attribute.Should().NotBeNull();
            attribute.Name.Should().Be(displayName);
        }

        private MemberInfo GetMemberInfo(string name)
        {
            var member = typeof(OpenApiVersionType).GetMember(name).First();

            return member;
        }
    }
}
