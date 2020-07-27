using System;

using Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Fakes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Visitors;

using FluentAssertions;

using Microsoft.OpenApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Visitors
{
    [TestClass]
    public class TypeVisitorTests
    {
        private IVisitor _visitor;
        private NamingStrategy _strategy;

        [TestInitialize]
        public void Init()
        {
            this._visitor = new FakeTypeVisitor();
            this._strategy = new CamelCaseNamingStrategy();
        }

        [DataTestMethod]
        [DataRow(typeof(string), false)]
        [DataRow(typeof(int), false)]
        public void Given_Type_When_IsNavigatable_Invoked_Then_It_Should_Return_Result(Type type, bool expected)
        {
            var result = this._visitor.IsNavigatable(type);

            result.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow(typeof(string), false)]
        [DataRow(typeof(int), false)]
        public void Given_Type_When_IsVisitable_Invoked_Then_It_Should_Return_Result(Type type, bool expected)
        {
            var result = this._visitor.IsVisitable(type);

            result.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow(typeof(string), false)]
        [DataRow(typeof(int), false)]
        public void Given_Type_When_IsParameterVisitable_Invoked_Then_It_Should_Return_Result(Type type, bool expected)
        {
            var result = this._visitor.IsParameterVisitable(type);

            result.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow(typeof(string), false)]
        [DataRow(typeof(int), false)]
        public void Given_Type_When_IsPayloadVisitable_Invoked_Then_It_Should_Return_Result(Type type, bool expected)
        {
            var result = this._visitor.IsPayloadVisitable(type);

            result.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow(typeof(string), default(OpenApiSchema))]
        public void Given_Type_When_ParameterVisit_Invoked_Then_It_Should_Return_Result(Type type, OpenApiSchema expected)
        {
            var result = this._visitor.ParameterVisit(type, this._strategy);

            result.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow(typeof(string), default(OpenApiSchema))]
        public void Given_Type_When_PayloadVisit_Invoked_Then_It_Should_Return_Null(Type type, OpenApiSchema expected)
        {
            var result = this._visitor.PayloadVisit(type, this._strategy);

            result.Should().Be(expected);
        }
    }
}
