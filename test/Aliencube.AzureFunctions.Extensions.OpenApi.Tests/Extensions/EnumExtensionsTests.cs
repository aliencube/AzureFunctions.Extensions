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
            var typeCode = TypeCode.Empty;

            Action action = () => EnumExtensions.ToDataType(typeCode);
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Given_TypeCode_ToDataType_Should_Return_Value()
        {
            var typeCode = TypeCode.Int16;
            var dataType = EnumExtensions.ToDataType(typeCode);

            dataType.Should().BeEquivalentTo("integer");

            typeCode = TypeCode.Single;
            dataType = EnumExtensions.ToDataType(typeCode);

            dataType.Should().BeEquivalentTo("number");

            typeCode = TypeCode.Boolean;
            dataType = EnumExtensions.ToDataType(typeCode);

            dataType.Should().BeEquivalentTo("boolean");

            typeCode = TypeCode.DateTime;
            dataType = EnumExtensions.ToDataType(typeCode);

            dataType.Should().BeEquivalentTo("string");

            typeCode = TypeCode.Object;
            dataType = EnumExtensions.ToDataType(typeCode);

            dataType.Should().BeEquivalentTo("object");
        }

        [TestMethod]
        public void Given_TypeCode_ToDataFormat_Should_Throw_Exception()
        {
            var typeCode = TypeCode.Empty;

            Action action = () => EnumExtensions.ToDataFormat(typeCode);
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Given_TypeCode_ToDataFormat_Should_Return_Value()
        {
            var typeCode = TypeCode.Int16;
            var dataType = EnumExtensions.ToDataFormat(typeCode);

            dataType.Should().BeEquivalentTo("int32");

            typeCode = TypeCode.Int64;
            dataType = EnumExtensions.ToDataFormat(typeCode);

            dataType.Should().BeEquivalentTo("int64");

            typeCode = TypeCode.Single;
            dataType = EnumExtensions.ToDataFormat(typeCode);

            dataType.Should().BeEquivalentTo("float");

            typeCode = TypeCode.Double;
            dataType = EnumExtensions.ToDataFormat(typeCode);

            dataType.Should().BeEquivalentTo("double");

            typeCode = TypeCode.DateTime;
            dataType = EnumExtensions.ToDataFormat(typeCode);

            dataType.Should().BeEquivalentTo("date-time");

            typeCode = TypeCode.Object;
            dataType = EnumExtensions.ToDataFormat(typeCode);

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
