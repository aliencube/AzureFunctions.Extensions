using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.OpenApi.Any;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Extensions
{
    /// <summary>
    ///  This represents the extension entity for <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks whether the given type is simple type or not.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Returns <c>True</c>, if simple type; otherwise returns <c>False</c>.</returns>
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
        /// Checks whether the given type is Json.NET related <see cref="JObject"/>, <see cref="JToken"/> or not.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <returns>Returns <c>True</c>, if the type is identified as either <see cref="JObject"/> or <see cref="JToken"/>; otherwise returns <c>False</c>.</returns>
        public static bool IsJObjectType(this Type type)
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
        /// Checks whether the given type is enum without flags or not.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <returns>Returns <c>True</c>, if the type is identified as enum without flags; otherwise returns <c>False</c>.</returns>
        public static bool IsUnflaggedEnumType(this Type type)
        {
            var isEnum = typeof(Enum).IsAssignableFrom(type);
            if (!isEnum)
            {
                return false;
            }

            var isFlagged = type.IsDefined(typeof(FlagsAttribute), false);
            if (isFlagged)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Converts the given <see cref="Type"/> instance to the list of underlying enum name.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <param name="namingStrategy"><see cref="NamingStrategy"/> instance.</param>
        /// <returns>Returns the list of underlying enum name.</returns>
        public static List<IOpenApiAny> ToOpenApiStringCollection(this Type type, NamingStrategy namingStrategy)
        {
            if (!type.IsUnflaggedEnumType())
            {
                return null;
            }

            var names = Enum.GetNames(type);

            return names.Select(p => (IOpenApiAny)new OpenApiString(namingStrategy.GetPropertyName(p, false)))
                        .ToList();
        }

        /// <summary>
        /// Converts the given <see cref="Type"/> instance to the list of underlying enum value.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <returns>Returns the list of underlying enum value.</returns>
        public static List<IOpenApiAny> ToOpenApiIntegerCollection(this Type type)
        {
            if (!type.IsUnflaggedEnumType())
            {
                return null;
            }

            var values = Enum.GetValues(type);
            if (type.GetEnumUnderlyingType() == typeof(short))
            {
                return values.Cast<short>()
                             .Select(p => (IOpenApiAny)new OpenApiInteger(p))
                             .ToList();
            }

            if (type.GetEnumUnderlyingType() == typeof(int))
            {
                return values.Cast<int>()
                             .Select(p => (IOpenApiAny)new OpenApiInteger(p))
                             .ToList();
            }

            if (type.GetEnumUnderlyingType() == typeof(long))
            {
                return values.Cast<long>()
                             .Select(p => (IOpenApiAny)new OpenApiLong(p))
                             .ToList();
            }

            return null;
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
        /// Gets the Open API reference ID.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <param name="isDictionary">Value indicating whether the type is <see cref="Dictionary{TKey, TValue}"/> or not.</param>
        /// <param name="isList">Value indicating whether the type is <see cref="List{T}"/> or not.</param>
        /// <returns>Returns the Open API reference ID.</returns>
        public static string GetOpenApiReferenceId(this Type type, bool isDictionary, bool isList)
        {
            if (isDictionary || isList)
            {
                return type.GetOpenApiSubTypeName();
            }

            return type.Name;
        }

        /// <summary>
        /// Gets the Open API root reference ID.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <returns>Returns the Open API root reference ID.</returns>
        public static string GetOpenApiRootReferenceId(this Type type)
        { 
            if (!type.IsGenericType)
            {
                return type.Name;
            }

            return type.GetOpenApiGenericRootName();
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
        /// <returns>Returns the recursively generated Open API description from the given <see cref="Type"/>.</returns>
        public static string GetOpenApiDescription(this Type type)
        {
            if (type.IsOpenApiDictionary())
            {
                return $"Dictionary of {type.GetOpenApiSubType().GetOpenApiDescription()}";
            }

            if (type.IsOpenApiArray())
            {
                return $"Array of {type.GetOpenApiSubType().GetOpenApiDescription()}";
            }

            if (type.IsGenericType)
            {
                string description = type.GetOpenApiGenericRootName() + "containing";
                var subtypes = type.GetGenericArguments();
                for (int i = 0; i < subtypes.Count() - 1; i++){
                    description += subtypes[i].GetOpenApiDescription();
                    description += ", ";
                }
                description += subtypes[subtypes.Count() - 1].GetOpenApiDescription();

                return description;
            }

            return type.Name;
        }

        /// <summary>
        /// Returns recursively the full name including all subtypes of the given type
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <returns>Returns the full name of the given type</returns>
        public static string GetOpenApiFullName(this Type type)
        {
            if (type.IsOpenApiDictionary() || type.IsOpenApiArray())
            {
                return type.GetOpenApiGenericRootName() + "<" + type.GetOpenApiSubType().GetOpenApiFullName() + ">";
            }

            if (type.IsGenericType)
            {
                string name = type.GetOpenApiGenericRootName() + "<";
                var subtypes = type.GetGenericArguments();
                for (int i = 0; i < subtypes.Count() - 1; i++)
                {
                    name += subtypes[i].GetOpenApiFullName();
                    name += ", ";
                }
                name += subtypes[subtypes.Count() - 1].GetOpenApiFullName();

                return name += ">";
            }

            return type.Name;
        }
            
        /// <summary>
        /// Gets the root name of the given generic type.
        /// </summary>
        /// <param name="type"><see cref="Type"/> instance.</param>
        /// <returns>Returns the root name of the given generic type.</returns>
        private static string GetOpenApiGenericRootName(this Type type)
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }

            var name = type.Name.Split(new[] { "`" }, StringSplitOptions.RemoveEmptyEntries).First();

            return name;
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

            if (type.IsGenericTypeOf(typeof(IEnumerable<>)))
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