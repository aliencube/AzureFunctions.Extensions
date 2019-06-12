using System;
using System.Linq;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.OpenApi;
using Aliencube.AzureFunctions.Extensions.OpenApi.Configurations;
using Aliencube.AzureFunctions.FunctionAppCommon.Models;

using FluentAssertions;

using Microsoft.OpenApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.FunctionAppV2.Tests
{
    [TestClass]
    public class OpenApiHttpTriggerTests
    {
        [TestMethod]
        public async Task Given_Naming_Strategy_Should_Vary_Case()
        {
            var document = new Document(new DocumentHelper(new RouteConstraintFilter()));

            var assembly = typeof(SampleHttpTrigger).Assembly;
            var result = await document
                .InitialiseDocument()
                .Build(assembly, new DefaultNamingStrategy())
                .RenderAsync(OpenApiSpecVersion.OpenApi3_0, OpenApiFormat.Json)
                .ConfigureAwait(false);

            var testPropName = nameof(SampleResponseModel.DateTimeOffsetValue);

            // Check that the property is defined as "DateTimeOffsetValue" in pascal case.
            var pcResult = JObject.Parse(result);
            var pcModel = pcResult.SelectToken("$.." + typeof(SampleResponseModel).Name);
            var pcProperties = pcModel["properties"];
            pcProperties[testPropName].Should().NotBeNull();
            pcProperties[testPropName].HasValues.Should().BeTrue();

            // Now run with camel casing
            var ccResult = JObject.Parse(await document
                .InitialiseDocument()
                .Build(assembly, new CamelCaseNamingStrategy())
                .RenderAsync(OpenApiSpecVersion.OpenApi3_0, OpenApiFormat.Json)
                .ConfigureAwait(false));

            var ccModel = ccResult.SelectToken("$.." + typeof(SampleResponseModel).Name);
            var ccProperties = ccModel["properties"];

            var expectedName = char.ToLower(testPropName[0]) + testPropName.Substring(1);

            ccProperties[expectedName].Should().NotBeNull();
            ccProperties[expectedName].HasValues.Should().BeTrue();
        }

        [TestMethod]
        public async Task Given_Enum_Type_With_StringConverter_Spec_Should_Contain_Enum_Names()
        {
            var document = new Document(new DocumentHelper(new RouteConstraintFilter()));

            var assembly = typeof(SampleHttpTrigger).Assembly;
            var result = JObject.Parse(await document
                .InitialiseDocument()
                .Build(assembly, new DefaultNamingStrategy())
                .RenderAsync(OpenApiSpecVersion.OpenApi3_0, OpenApiFormat.Json)
                .ConfigureAwait(false));

            var enumPropToken = result.SelectToken(
                $"$..{typeof(SampleResponseModel).Name}.properties.{nameof(SampleResponseModel.EnumValueAsString)}");

            enumPropToken.Should().NotBeNull();

            var enumValues = enumPropToken["enum"];
            enumValues.Should().NotBeNull();

            enumValues.Children().Select(t => t.Value<string>()).Should()
                .BeEquivalentTo(Enum.GetNames(typeof(StringEnum)));
        }

        [TestMethod]
        public async Task Given_Enum_Type_WithOut_StringConverter_Spec_Should_Contain_Enum_Numbers()
        {
            var document = new Document(new DocumentHelper(new RouteConstraintFilter()));

            var assembly = typeof(SampleHttpTrigger).Assembly;
            var result = JObject.Parse(await document
                .InitialiseDocument()
                .Build(assembly, new DefaultNamingStrategy())
                .RenderAsync(OpenApiSpecVersion.OpenApi3_0, OpenApiFormat.Json)
                .ConfigureAwait(false));

            var enumPropToken = result.SelectToken(
                $"$..{typeof(SampleResponseModel).Name}.properties.{nameof(SampleResponseModel.EnumValueAsNumber)}");

            enumPropToken.Should().NotBeNull();

            var enumValues = enumPropToken["enum"];
            enumValues.Should().NotBeNull();

            enumValues.Children().Select(t => t.Value<string>()).Should()
                .BeEquivalentTo(Enum.GetValues(typeof(NumericEnum)));
        }
    }
}