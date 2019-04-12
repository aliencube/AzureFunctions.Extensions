using System;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Extensions
{
    [TestClass]
    public class EnumExtensionsTests
    {
        [TestMethod]
        public void Given_Enum_Method_Should_Return_Value()
        {
            var @enum = FakeEnum.Value1;
            var name = EnumExtensions.ToDisplayName(@enum);

            name.Should().BeEquivalentTo(@enum.ToString());

            @enum = FakeEnum.Value2;
            name = EnumExtensions.ToDisplayName(@enum);
            var expected = "lorem";

            name.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Given_TypeCode_ToDataType_Should_Throw_Exception()
        {
            Action action = () => EnumExtensions.ToDataType(null);
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Given_TypeCode_ToDataType_Should_Return_Value()
        {
            var dataType = EnumExtensions.ToDataType(typeof(Int16));

            dataType.Should().BeEquivalentTo("integer");

            dataType = EnumExtensions.ToDataType(typeof(Single));

            dataType.Should().BeEquivalentTo("number");

            dataType = EnumExtensions.ToDataType(typeof(Boolean));

            dataType.Should().BeEquivalentTo("boolean");

            dataType = EnumExtensions.ToDataType(typeof(DateTime));

            dataType.Should().BeEquivalentTo("string");

            dataType = EnumExtensions.ToDataType(typeof(Guid));

            dataType.Should().BeEquivalentTo("string");

            dataType = EnumExtensions.ToDataType(typeof(object));

            dataType.Should().BeEquivalentTo("object");
        }

        [TestMethod]
        public void Given_TypeCode_ToDataFormat_Should_Throw_Exception()
        {
            Action action = () => EnumExtensions.ToDataFormat(null);
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Given_TypeCode_ToDataFormat_Should_Return_Value()
        {
            var dataType = EnumExtensions.ToDataFormat(typeof(Int16));

            dataType.Should().BeEquivalentTo("int32");

            dataType = EnumExtensions.ToDataFormat(typeof(Int64));

            dataType.Should().BeEquivalentTo("int64");

            dataType = EnumExtensions.ToDataFormat(typeof(Single));

            dataType.Should().BeEquivalentTo("float");

            dataType = EnumExtensions.ToDataFormat(typeof(Double));

            dataType.Should().BeEquivalentTo("double");

            dataType = EnumExtensions.ToDataFormat(typeof(DateTime));

            dataType.Should().BeEquivalentTo("date-time");

            dataType = EnumExtensions.ToDataFormat(typeof(Guid));

            dataType.Should().BeEquivalentTo("uuid");

            dataType = EnumExtensions.ToDataFormat(typeof(object));

            dataType.Should().BeNull();
        }
    }

    public enum FakeEnum
    {
        Value1,

        [Display("lorem")]
        Value2
    }
}
