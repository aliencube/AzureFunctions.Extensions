using System;
using System.Collections.Generic;

using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Enums;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Extensions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Visitors;

using FluentAssertions;

using Microsoft.OpenApi.Any;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Core.Tests.Visitors
{
    [TestClass]
    public class GuidObjectTypeVisitorTests
    {
        private IVisitor _visitor;
        private NamingStrategy _strategy;

        [TestInitialize]
        public void Init()
        {
            this._visitor = new GuidObjectTypeVisitor();
            this._strategy = new CamelCaseNamingStrategy();
        }

        [DataTestMethod]
        [DataRow(typeof(Guid), false)]
        public void Given_Type_When_IsNavigatable_Invoked_Then_It_Should_Return_Result(Type type, bool expected)
        {
            var result = this._visitor.IsNavigatable(type);

            result.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow(typeof(Guid), true)]
        [DataRow(typeof(int), false)]
        public void Given_Type_When_IsVisitable_Invoked_Then_It_Should_Return_Result(Type type, bool expected)
        {
            var result = this._visitor.IsVisitable(type);

            result.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow(typeof(Guid), true)]
        [DataRow(typeof(int), false)]
        public void Given_Type_When_IsParameterVisitable_Invoked_Then_It_Should_Return_Result(Type type, bool expected)
        {
            var result = this._visitor.IsParameterVisitable(type);

            result.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow(typeof(Guid), true)]
        [DataRow(typeof(int), false)]
        public void Given_Type_When_IsPayloadVisitable_Invoked_Then_It_Should_Return_Result(Type type, bool expected)
        {
            var result = this._visitor.IsPayloadVisitable(type);

            result.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow("string", "uuid")]
        public void Given_Type_When_Visit_Invoked_Then_It_Should_Return_Result(string dataType, string dataFormat)
        {
            var name = "hello";
            var acceptor = new OpenApiSchemaAcceptor();
            var type = new KeyValuePair<string, Type>(name, typeof(Guid));

            this._visitor.Visit(acceptor, type, this._strategy);

            acceptor.Schemas.Should().ContainKey(name);
            acceptor.Schemas[name].Type.Should().Be(dataType);
            acceptor.Schemas[name].Format.Should().Be(dataFormat);
        }

        [DataTestMethod]
        [DataRow(OpenApiVisibilityType.Advanced)]
        [DataRow(OpenApiVisibilityType.Important)]
        [DataRow(OpenApiVisibilityType.Internal)]
        public void Given_Attribute_When_Visit_Invoked_Then_It_Should_Return_Result(OpenApiVisibilityType visibility)
        {
            var name = "hello";
            var acceptor = new OpenApiSchemaAcceptor();
            var type = new KeyValuePair<string, Type>(name, typeof(Guid));
            var attribute = new OpenApiSchemaVisibilityAttribute(visibility);

            this._visitor.Visit(acceptor, type, this._strategy, attribute);

            acceptor.Schemas[name].Extensions.Should().ContainKey("x-ms-visibility");
            acceptor.Schemas[name].Extensions["x-ms-visibility"].Should().BeOfType<OpenApiString>();
            (acceptor.Schemas[name].Extensions["x-ms-visibility"] as OpenApiString).Value.Should().Be(visibility.ToDisplayName(this._strategy));
        }

        [DataTestMethod]
        [DataRow("string", "uuid")]
        public void Given_Type_When_ParameterVisit_Invoked_Then_It_Should_Return_Result(string dataType, string dataFormat)
        {
            var result = this._visitor.ParameterVisit(typeof(Guid), this._strategy);

            result.Type.Should().Be(dataType);
            result.Format.Should().Be(dataFormat);
        }

        [DataTestMethod]
        [DataRow("string", "uuid")]
        public void Given_Type_When_PayloadVisit_Invoked_Then_It_Should_Return_Result(string dataType, string dataFormat)
        {
            var result = this._visitor.PayloadVisit(typeof(Guid), this._strategy);

            result.Type.Should().Be(dataType);
            result.Format.Should().Be(dataFormat);
        }
    }
}
