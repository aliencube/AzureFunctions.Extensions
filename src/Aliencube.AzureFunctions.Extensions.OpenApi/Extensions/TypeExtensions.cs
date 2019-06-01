using System;
using System.Collections.Generic;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Extensions
{
    /// <summary>
    ///  This represents the extension entity for type.
    /// </summary>
    public static class TypeExtensions
    {
        public static bool IsOpenApiArray(this Type type)
        {
            if (type == null) { return false; }

            if (type.BaseType == typeof(Array)) { return true; }
            return type.IsGenericType(typeof(IList<>))
                || type.IsGenericType(typeof(List<>));
        }

        public static string ToOpenApiDescription(this Type type)
        {
            return type.IsOpenApiArray() ?
                $"Array of {type.ToOpenAPISubType()}" :
               type.Name;
        }

        public static string ToOpenAPISubType(this Type type)
        {
            if (type.BaseType == typeof(Array))
            {
                return type.GetElementType().Name;
            }
            if (type.IsGenericType(typeof(IList<>))
               || type.IsGenericType(typeof(List<>)))
            {
                return type.GetGenericArguments()[0].Name;
            }
            return null;
        }

        private static bool IsGenericType(this Type t1, Type t2) => t1.IsGenericType && t2.GetGenericTypeDefinition() == t2;
    }
}
