using System;
using System.Linq;
using System.Reflection;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

using Microsoft.Azure.WebJobs;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Extensions
{
    /// <summary>
    /// This represents the extension entity for <see cref="MethodInfo"/>.
    /// </summary>
    public static class MethodInfoExtensions
    {
        /// <summary>
        /// Gets the <see cref="FunctionNameAttribute"/> instance.
        /// </summary>
        /// <param name="element"><see cref="MethodInfo"/> instance.</param>
        /// <returns><see cref="FunctionNameAttribute"/> instance.</returns>
        public static FunctionNameAttribute GetFunctionName(this MethodInfo element)
        {
            element.ThrowIfNullOrDefault();

            var function = element.GetCustomAttribute<FunctionNameAttribute>(inherit: false);

            return function;
        }

        /// <summary>
        /// Gets the <see cref="HttpTriggerAttribute"/> instance.
        /// </summary>
        /// <param name="element"><see cref="MethodInfo"/> instance.</param>
        /// <returns><see cref="HttpTriggerAttribute"/> instance.</returns>
        public static HttpTriggerAttribute GetHttpTrigger(this MethodInfo element)
        {
            element.ThrowIfNullOrDefault();

            var trigger = element.GetParameters()
                                 .First()
                                 .GetCustomAttribute<HttpTriggerAttribute>(inherit: false);

            return trigger;
        }

        /// <summary>
        /// Gets the <see cref="HttpTriggerAttribute"/> instance.
        /// </summary>
        /// <param name="element"><see cref="MethodInfo"/> instance.</param>
        /// <returns><see cref="HttpTriggerAttribute"/> instance.</returns>
        public static OpenApiOperationAttribute GetOpenApiOperation(this MethodInfo element)
        {
            element.ThrowIfNullOrDefault();

            var operation = element.GetCustomAttribute<OpenApiOperationAttribute>(inherit: false);

            return operation;
        }
    }
}