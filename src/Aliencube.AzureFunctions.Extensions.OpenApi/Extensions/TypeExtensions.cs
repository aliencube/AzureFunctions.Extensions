using System;
using System.Collections.Generic;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Extensions
{
    /// <summary>
    ///  This represents the extension entity for <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks whether the given type is array or not, from the Open API perspective.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <returns>Returns <c>True</c>, if the type is identified as array; otherwise returns <c>False</c>.</returns>
        public static bool IsOpenApiArray(this Type type)
        {
            if (type.IsNullOrDefault())
            {
                return false;
            }

            if (type.BaseType == typeof(Array))
            {
                return true;
            }

            return type.IsGenericTypeOf(typeof(List<>)) ||
                   type.IsGenericTypeOf(typeof(IList<>));
        }

        /// <summary>
        /// Checks whether the given type is generic type of
        /// </summary>
        /// <param name="t1"><see cref="Type"/> instance.</param>
        /// <param name="t2"><see cref="Type"/> instance to compare.</param>
        /// <returns>Returns <c>True</c>, if the given type is generic; otherwise returns <c>False</c>.</returns>
        public static bool IsGenericTypeOf(this Type t1, Type t2)
        {
            return t1.IsGenericType && t2.GetGenericTypeDefinition() == t2;
        }

        /// <summary>
        /// Gets the Open API description from the given <see cref="Type"/>.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <returns>Returns the Open API description from the given <see cref="Type"/>.</returns>
        public static string ToOpenApiDescription(this Type type)
        {
            return type.IsOpenApiArray()
                   ? $"Array of {type.ToOpenApiSubType()}"
                   : type.Name;
        }

        /// <summary>
        /// Gets the sub type of the given <see cref="Type"/>.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <returns>Returns the sub type of the given <see cref="Type"/>.</returns>
        public static string ToOpenApiSubType(this Type type)
        {
            if (type.BaseType == typeof(Array))
            {
                return type.GetElementType().Name;
            }

            if (type.IsGenericTypeOf(typeof(List<>)))
            {
                return type.GetGenericArguments()[0].Name;
            }

            if (type.IsGenericTypeOf(typeof(IList<>)))
            {
                return type.GetGenericArguments()[0].Name;
            }

            return null;
        }
    }
}