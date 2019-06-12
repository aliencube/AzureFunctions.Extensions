using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Configurations;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;
using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.Extensions.OpenApi
{
    /// <summary>
    /// This represents the helper entity for the <see cref="Document"/> class.
    /// </summary>
    public class DocumentHelper : IDocumentHelper
    {
        private readonly RouteConstraintFilter _filter;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentHelper"/> class.
        /// </summary>
        /// <param name="filter"><see cref="RouteConstraintFilter"/> instance.</param>
        public DocumentHelper(RouteConstraintFilter filter)
        {
            this._filter = filter.ThrowIfNullOrDefault();
        }

        /// <inheritdoc />
        public List<MethodInfo> GetHttpTriggerMethods(Assembly assembly)
        {
            var methods = assembly.GetTypes()
                                  .SelectMany(p => p.GetMethods())
                                  .Where(p => p.ExistsCustomAttribute<FunctionNameAttribute>())
                                  .Where(p => p.ExistsCustomAttribute<OpenApiOperationAttribute>())
                                  .Where(p => !p.ExistsCustomAttribute<OpenApiIgnoreAttribute>())
                                  .Where(p => p.GetParameters().FirstOrDefault(q => q.ExistsCustomAttribute<HttpTriggerAttribute>()) != null)
                                  .ToList();

            return methods;
        }

        /// <inheritdoc />
        public HttpTriggerAttribute GetHttpTriggerAttribute(MethodInfo element)
        {
            var trigger = element.GetHttpTrigger();

            return trigger;
        }

        /// <inheritdoc />
        public FunctionNameAttribute GetFunctionNameAttribute(MethodInfo element)
        {
            var function = element.GetFunctionName();

            return function;
        }

        /// <inheritdoc />
        public string GetHttpEndpoint(FunctionNameAttribute function, HttpTriggerAttribute trigger)
        {
            var endpoint = $"/{(string.IsNullOrWhiteSpace(trigger.Route) ? function.Name : this.FilterRoute(trigger.Route)).Trim('/')}";

            return endpoint;
        }

        /// <inheritdoc />
        public OperationType GetHttpVerb(HttpTriggerAttribute trigger)
        {
            var verb = Enum.TryParse<OperationType>(trigger.Methods.First(), true, out OperationType ot)
                           ? ot
                           : throw new InvalidOperationException();

            return verb;
        }

        /// <inheritdoc />
        public OpenApiPathItem GetOpenApiPath(string path, OpenApiPaths paths)
        {
            var item = paths.ContainsKey(path) ? paths[path] : new OpenApiPathItem();

            return item;
        }

        /// <inheritdoc />
        public OpenApiOperation GetOpenApiOperation(MethodInfo element, FunctionNameAttribute function, OperationType verb)
        {
            var op = element.GetOpenApiOperation();
            if (op.IsNullOrDefault())
            {
                return null;
            }

            var operation = new OpenApiOperation()
                                {
                                    OperationId = string.IsNullOrWhiteSpace(op.OperationId) ? $"{function.Name}_{verb}" : op.OperationId,
                                    Tags = op.Tags.Select(p => new OpenApiTag() { Name = p }).ToList(),
                                    Summary = op.Summary,
                                    Description = op.Description
                                };

            if (op.Visibility != OpenApiVisibilityType.Undefined)
            {
                var visibility = new OpenApiString(op.Visibility.ToDisplayName());

                operation.Extensions.Add("x-ms-visibility", visibility);
            }

            return operation;
        }

        /// <inheritdoc />
        public List<OpenApiParameter> GetOpenApiParameters(MethodInfo element, HttpTriggerAttribute trigger)
        {
            var parameters = element.GetCustomAttributes<OpenApiParameterAttribute>(inherit: false)
                                    .Select(p => p.ToOpenApiParameter())
                                    .ToList();

            if (trigger.AuthLevel != AuthorizationLevel.Anonymous)
            {
                parameters.AddOpenApiParameter<string>("code", @in: ParameterLocation.Query, required: false);
            }

            return parameters;
        }

        /// <inheritdoc />
        public OpenApiRequestBody GetOpenApiRequestBody(MethodInfo element, NamingStrategy namingStrategy = null)
        {
            var contents = element.GetCustomAttributes<OpenApiRequestBodyAttribute>(inherit: false)
                                  .ToDictionary(p => p.ContentType, p => p.ToOpenApiMediaType());

            if (contents.Any())
            {
                return new OpenApiRequestBody() { Content = contents };
            }

            return null;
        }

        /// <inheritdoc />
        public OpenApiResponses GetOpenApiResponseBody(MethodInfo element, NamingStrategy namingStrategy = null)
        {
            var responses = element.GetCustomAttributes<OpenApiResponseBodyAttribute>(inherit: false)
                                   .ToDictionary(p => ((int)p.StatusCode).ToString(), p => p.ToOpenApiResponse(namingStrategy))
                                   .ToOpenApiResponses();

            return responses;
        }

        /// <inheritdoc />
        public Dictionary<string, OpenApiSchema> GetOpenApiSchemas(List<MethodInfo> elements, NamingStrategy namingStrategy)
        {
            var requests = elements.SelectMany(p => p.GetCustomAttributes<OpenApiRequestBodyAttribute>(inherit: false))
                                   .Select(p => p.BodyType);
            var responses = elements.SelectMany(p => p.GetCustomAttributes<OpenApiResponseBodyAttribute>(inherit: false))
                                    .Select(p => p.BodyType);
            var types = requests.Union(responses)
                                .Select(p => p.IsOpenApiArray() ? p.GetOpenApiSubType() : p )
                                .Distinct()
                                .Where(p => !p.IsSimpleType())
                                .Where(p => p != typeof(JObject))
                                .Where(p => p != typeof(JToken))
                                .Where(p => !typeof(Array).IsAssignableFrom(p))
                                .Where(p => !p.IsGenericType)
                                ;
            var schemas = types.ToDictionary(p => p.Name, p => p.ToOpenApiSchema(namingStrategy)); // schemaGenerator.Generate(p)

            return schemas;
        }

        /// <inheritdoc />
        public Dictionary<string, OpenApiSecurityScheme> GetOpenApiSecuritySchemes()
        {
            var scheme = new OpenApiSecurityScheme()
                             {
                                 Name = "x-functions-key",
                                 Type = SecuritySchemeType.ApiKey,
                                 In = ParameterLocation.Header
                             };
            var schemes = new Dictionary<string, OpenApiSecurityScheme>()
                              {
                                  { "authKey", scheme }
                              };

            return schemes;
        }

        private string FilterRoute(string route)
        {
            var segments = route.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => this._filter.Filter.Replace(p, this._filter.Replacement));

            return string.Join("/", segments);
        }
    }
}