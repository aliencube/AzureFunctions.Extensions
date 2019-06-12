using System;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Extensions
{
    /// <summary>
    ///  This represents the extension entity for <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks whether the given type is Json.NET related <see cref="JObject"/>, <see cref="JToken"/> or not.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <returns>Returns <c>True</c>, if the type is identified as either <see cref="JObject"/> or <see cref="JToken"/>; otherwise returns <c>False</c>.</returns>
        public static bool IsJObject(this Type type)
        {
            if (type.IsNullOrDefault())
            {
                return false;
            }

            if (type == typeof(JObject))
            {
                return true;
            }

            if (type == typeof(JToken))
            {
                return true;
            }

            return false;
        }

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

            return type.IsArrayType();
        }

        /// <summary>
        /// Checks whether the given type is array or not, from the Open API perspective.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <returns>Returns <c>True</c>, if the type is identified as array; otherwise returns <c>False</c>.</returns>
        public static bool IsOpenApiDictionary(this Type type)
        {
            if (type.IsNullOrDefault())
            {
                return false;
            }

            return type.IsDictionaryType();
        }

        /// <summary>
        /// Checks whether the given type is simple type or not.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSimpleType(this Type type)
        {
            var @enum = Type.GetTypeCode(type);
            switch (@enum)
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.Boolean:
                case TypeCode.DateTime:
                case TypeCode.String:
                    return true;

                case TypeCode.Object:
                    if (type == typeof(Guid))
                    {
                        return true;
                    }
                    else if (type == typeof(DateTime))
                    {
                        return true;
                    }
                    else if (type == typeof(DateTimeOffset))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case TypeCode.Empty:
                case TypeCode.DBNull:
                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the Open API Reference ID.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <param name="isDictionary">Value indicating whether the type is <see cref="Dictionary{TKey, TValue}"/> or not.</param>
        /// <param name="isList">Value indicating whether the type is <see cref="List{T}"/> or not.</param>
        /// <returns>Returns the Open API Reference ID.</returns>
        public static string GetOpenApiReferenceId(this Type type, bool isDictionary, bool isList)
        {
            if (isDictionary || isList)
            {
                return type.GetOpenApiSubTypeName();
            }

            return type.Name;
        }

        /// <summary>
        /// Checks whether the given type is generic type of
        /// </summary>
        /// <param name="t1"><see cref="Type"/> instance.</param>
        /// <param name="t2"><see cref="Type"/> instance to compare.</param>
        /// <returns>Returns <c>True</c>, if the given type is generic; otherwise returns <c>False</c>.</returns>
        public static bool IsGenericTypeOf(this Type t1, Type t2)
        {
            return t1.IsGenericType && t1.GetGenericTypeDefinition() == t2;
        }

        /// <summary>
        /// Gets the Open API description from the given <see cref="Type"/>.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <returns>Returns the Open API description from the given <see cref="Type"/>.</returns>
        public static string GetOpenApiDescription(this Type type)
        {
            if (type.IsOpenApiDictionary())
            {
                return $"Dictionary of {type.GetOpenApiSubTypeName()}";
            }

            if (type.IsOpenApiArray())
            {
                return $"Array of {type.GetOpenApiSubTypeName()}";
            }

            return type.Name;
        }

        /// <summary>
        /// Gets the sub type of the given <see cref="Type"/>.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <returns>Returns the sub type of the given <see cref="Type"/>.</returns>
        public static Type GetOpenApiSubType(this Type type)
        {
            if (type.IsDictionaryType())
            {
                return type.GetGenericArguments()[1];
            }

            if (type.BaseType == typeof(Array))
            {
                return type.GetElementType();
            }

            if (type.IsArrayType())
            {
                return type.GetGenericArguments()[0];
            }

            return null;
        }

        /// <summary>
        /// Gets the name of the sub type of the given <see cref="Type"/>.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <returns>Returns the name of the sub type of the given <see cref="Type"/>.</returns>
        public static string GetOpenApiSubTypeName(this Type type)
        {
            if (type.IsDictionaryType())
            {
                return type.GetGenericArguments()[1].Name;
            }

            if (type.BaseType == typeof(Array))
            {
                return type.GetElementType().Name;
            }

            if (type.IsArrayType())
            {
                return type.GetGenericArguments()[0].Name;
            }

            return null;
        }

        private static bool IsArrayType(this Type type)
        {
            if (type.BaseType == typeof(Array))
            {
                return true;
            }

            if (type.IsGenericTypeOf(typeof(List<>)))
            {
                return true;
            }

            if (type.IsGenericTypeOf(typeof(IList<>)))
            {
                return true;
            }

            return false;
        }

        private static bool IsDictionaryType(this Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            if (type.Name.Equals("Dictionary`2", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }

            if (type.Name.Equals("IDictionary`2", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}