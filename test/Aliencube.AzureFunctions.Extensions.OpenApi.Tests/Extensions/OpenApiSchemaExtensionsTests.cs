﻿using System;
using System.Collections.Generic;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;
using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;
using Aliencube.AzureFunctions.Tests.Fakes;

using FluentAssertions;

using Microsoft.OpenApi.Any;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Extensions
{
    [TestClass]
    public class OpenApiSchemaExtensionsTests
    {
        [TestMethod]
        public void Given_Type_Null_It_Should_Throw_Exception()
        {
            Action action = () => OpenApiSchemaExtensions.ToOpenApiSchema(null, null, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Given_Type_JObject_It_Should_Return_Result()
        {
            var type = typeof(JObject);
            var strategy = new CamelCaseNamingStrategy();

            var schema = OpenApiSchemaExtensions.ToOpenApiSchema(type, strategy);

            schema.Type.Should().BeEquivalentTo("object");
        }

        [TestMethod]
        public void Given_Type_JToken_It_Should_Return_Result()
        {
            var type = typeof(JToken);
            var strategy = new CamelCaseNamingStrategy();

            var schema = OpenApiSchemaExtensions.ToOpenApiSchema(type, strategy);

            schema.Type.Should().BeEquivalentTo("object");
        }

        [TestMethod]
        public void Given_Type_Nullable_It_Should_Return_Result()
        {
            var type = typeof(int?);
            var strategy = new CamelCaseNamingStrategy();

            var schema = OpenApiSchemaExtensions.ToOpenApiSchema(type, strategy);

            schema.Nullable.Should().BeTrue();
            schema.Type.Should().BeEquivalentTo("integer");
            schema.Format.Should().BeEquivalentTo("int32");
        }

        [TestMethod]
        public void Given_Type_Simple_It_Should_Return_Result()
        {
            var type = typeof(int);
            var strategy = new CamelCaseNamingStrategy();

            var schema = OpenApiSchemaExtensions.ToOpenApiSchema(type, strategy);

            schema.Type.Should().BeEquivalentTo("integer");
            schema.Format.Should().BeEquivalentTo("int32");
        }

        [TestMethod]
        public void Given_Visibility_It_Should_Return_Result()
        {
            var type = typeof(int);
            var strategy = new CamelCaseNamingStrategy();
            var visibilityType = OpenApiVisibilityType.Important;
            var visibility = new OpenApiSchemaVisibilityAttribute(visibilityType);

            var schema = OpenApiSchemaExtensions.ToOpenApiSchema(type, strategy, visibility);

            schema.Extensions.ContainsKey("x-ms-visibility").Should().BeTrue();
            schema.Extensions["x-ms-visibility"].Should().BeEquivalentTo(new OpenApiString(visibilityType.ToDisplayName()));
            schema.Type.Should().BeEquivalentTo("integer");
            schema.Format.Should().BeEquivalentTo("int32");
        }

        [TestMethod]
        public void Given_Dictionary_It_Should_Return_Result()
        {
            var type = typeof(Dictionary<string, int>);
            var strategy = new CamelCaseNamingStrategy();

            var schema = OpenApiSchemaExtensions.ToOpenApiSchema(type, strategy);

            schema.Type.Should().BeEquivalentTo("object");
            schema.AdditionalProperties.Type.Should().BeEquivalentTo("integer");
            schema.AdditionalProperties.Format.Should().BeEquivalentTo("int32");
        }

        [TestMethod]
        public void Given_IDictionary_It_Should_Return_Result()
        {
            var type = typeof(Dictionary<string, int>);
            var strategy = new CamelCaseNamingStrategy();

            var schema = OpenApiSchemaExtensions.ToOpenApiSchema(type, strategy);

            schema.Type.Should().BeEquivalentTo("object");
            schema.AdditionalProperties.Type.Should().BeEquivalentTo("integer");
            schema.AdditionalProperties.Format.Should().BeEquivalentTo("int32");
        }

        [TestMethod]
        public void Given_IDictionaryWithFakeModel_It_Should_Return_Result()
        {
            var type = typeof(Dictionary<string, FakeModel>);
            var strategy = new CamelCaseNamingStrategy();

            var schema = OpenApiSchemaExtensions.ToOpenApiSchema(type, strategy);

            schema.Type.Should().BeEquivalentTo("object");
            schema.AdditionalProperties.Type.Should().BeEquivalentTo("object");
        }

        [TestMethod]
        public void Given_GenericIList_Should_Be_Array_With_Matching_Type()
        {
            var list = typeof(IList<string>);
            var strategy = new CamelCaseNamingStrategy();

            var result = list.ToOpenApiSchema(strategy);

            result.Type.Should().Be("array");
            result.Items.Type.Should().Be("string");
        }

        [TestMethod]
        public void Given_GenericList_Should_Be_Array_With_Matching_Type()
        {
            var list = typeof(List<string>);
            var strategy = new CamelCaseNamingStrategy();

            var result = list.ToOpenApiSchema(strategy);

            result.Type.Should().Be("array");
            result.Items.Type.Should().Be("string");
        }

        [TestMethod]
        public void Given_UntypedList_Should_Be_Array_With_Object_Type()
        {
            var list = typeof(List<>);
            var strategy = new CamelCaseNamingStrategy();

            var result = list.ToOpenApiSchema(strategy);

            result.Type.Should().Be("array");
            result.Items.Type.Should().Be("object");
        }

        [TestMethod]
        public void Given_Array_Should_Be_Array()
        {
            var list = typeof(string[]);
            var strategy = new CamelCaseNamingStrategy();

            var result = list.ToOpenApiSchema(strategy);

            result.Type.Should().Be("array");
            result.Items.Type.Should().Be("string");
        }

        [TestMethod]
        public void Given_Object_Should_Not_Be_Array()
        {
            var list = typeof(string);
            var strategy = new CamelCaseNamingStrategy();

            var result = list.ToOpenApiSchema(strategy);

            result.Type.Should().NotBe("array");
            result.Items.Should().BeNull();
        }

        [TestMethod]
        public void Given_Interface_With_Inheritance_Should_Contain_All_Properties()
        {
            var interfaceType = typeof(IFakeInheritedInterface);
            var strategy = new CamelCaseNamingStrategy();

            var result = interfaceType.ToOpenApiSchema(strategy);

            result.Properties.Count.Should().Be(4);
        }

        [TestMethod]
        public void Given_FakeModel_It_Should_Return_Result()
        {
            var type = typeof(FakeModel);
            var strategy = new CamelCaseNamingStrategy();

            var schemas = OpenApiSchemaExtensions.ToOpenApiSchemas(type, strategy);
            schemas.Count.Should().Be(3);
            var fmSchema = schemas[type.Name];
            var fsmType = typeof(FakeSubModel);
            var fsmSchema = schemas[fsmType.Name];
            var feType = typeof(FakeEnum);
            var feSchema = schemas[feType.Name];
            fmSchema.Type.Should().Be("object");
            fmSchema.Properties["fakeProperty"].Type.Should().BeEquivalentTo("string");
            fmSchema.Properties["nullableInt"].Type.Should().BeEquivalentTo("integer");
            fmSchema.Properties["nullableInt"].Nullable.Should().BeTrue();
            fmSchema.Properties["subProperty"].Reference.Id.Should().BeEquivalentTo(fsmType.Name);
            fmSchema.Properties["enumProperty"].Reference.Id.Should().BeEquivalentTo(feType.Name);

            fsmSchema.Type.Should().Be("object");
            fsmSchema.Properties["fakeSubModelProperty"].Type.Should().BeEquivalentTo("integer");

            feSchema.Type.Should().Be("integer");
            feSchema.Enum.Count.Should().Be(2);


        }

        [TestMethod]
        public void Given_FakeModelWithList_It_Should_Return_Result()
        {
            var type = typeof(FakeModelWithList);
            var strategy = new CamelCaseNamingStrategy();

            var schemas = OpenApiSchemaExtensions.ToOpenApiSchemas(type, strategy);
            schemas.Count.Should().Be(2);
            var fmSchema = schemas[type.Name];
            var fsmType = typeof(FakeSubModel);
            var fsmSchema = schemas[fsmType.Name];
            fmSchema.Type.Should().Be("object");
            fmSchema.Properties["parent"].Reference.Id.Should().BeEquivalentTo(fsmType.Name);
            fmSchema.Properties["items"].Type.Should().BeEquivalentTo("array");
            fmSchema.Properties["items"].Items.Reference.Id.Should().BeEquivalentTo(fsmType.Name);

            fsmSchema.Type.Should().Be("object");
            fsmSchema.Properties["fakeSubModelProperty"].Type.Should().BeEquivalentTo("integer");


        }
        [TestMethod]
        public void Given_FakeModelWithCircularRef_It_Should_Return_Result()
        {
            var type = typeof(FakeModelWithCircularRef);
            var strategy = new CamelCaseNamingStrategy();

            var schemas = OpenApiSchemaExtensions.ToOpenApiSchemas(type, strategy);
            schemas.Count.Should().Be(2);
            var fmSchema = schemas[type.Name];
            var fsmType = typeof(FakeModelWithCircularRefSub);
            var fsmSchema = schemas[fsmType.Name];
            fmSchema.Type.Should().Be("object");
            fmSchema.Properties["subProperty"].Reference.Id.Should().BeEquivalentTo(fsmType.Name);

            fsmSchema.Type.Should().Be("object");
            fsmSchema.Properties["circle"].Reference.Id.Should().BeEquivalentTo(type.Name);


        }
    }
}