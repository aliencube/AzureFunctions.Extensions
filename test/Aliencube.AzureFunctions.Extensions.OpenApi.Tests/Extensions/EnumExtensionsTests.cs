using System;

using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;
using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Fakes;

using FluentAssertions;

using Microsoft.OpenApi;
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

        //[DataTestMethod]
        //[DataRow(typeof(FakeClass))]
        //public void Given_Type_When_ToEnumList_Invoked_Then_It_Should_Return_Result(Type type)
        //{
        //    var result = EnumExtensions.ToEnumList(type);

        //    result.Should().BeNull();
        //}

        //[DataTestMethod]
        //[DataRow(typeof(FakeEnum))]
        //public void Given_Enum_When_ToEnumList_Invoked_Then_It_Should_Return_Result(Type type)
        //{
        //    var values = Enum.GetNames(type).Select(p => Enum.Parse(type, p).ToString()).ToList();

        //    var results = EnumExtensions.ToEnumList(type).OfType<OpenApiString>().ToList();

        //    for (var i = 0; i < results.Count; i++)
        //    {
        //        results[i].Value.Should().BeEquivalentTo(values[i]);
        //    }
        //}

        [DataTestMethod]
        [DataRow(OpenApiFormat.Json, "application/json")]
        [DataRow(OpenApiFormat.Yaml, "application/yaml")]
        public void Given_OpenApiFormat_When_GetContentType_Invoked_Then_It_Should_Return_Result(OpenApiFormat format, string expected)
        {
            var result = EnumExtensions.GetContentType(format);

            result.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow(OpenApiVersionType.V2, OpenApiSpecVersion.OpenApi2_0)]
        [DataRow(OpenApiVersionType.V3, OpenApiSpecVersion.OpenApi3_0)]
        public void Given_OpenApiVersionType_When_ToOpenApiSpecVersion_Invoked_Then_It_Should_Return_Result(OpenApiVersionType version, OpenApiSpecVersion expected)
        {
            var result = EnumExtensions.ToOpenApiSpecVersion(version);

            result.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow(OpenApiFormatType.Json, OpenApiFormat.Json)]
        [DataRow(OpenApiFormatType.Yaml, OpenApiFormat.Yaml)]
        public void Given_OpenApiFormatType_When_ToOpenApiFormat_Invoked_Then_It_Should_Return_Result(OpenApiFormatType format, OpenApiFormat expected)
        {
            var result = EnumExtensions.ToOpenApiFormat(format);

            result.Should().Be(expected);
        }
    }
}
