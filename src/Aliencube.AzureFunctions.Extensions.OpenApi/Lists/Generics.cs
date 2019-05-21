using System;
using System.Collections.Generic;
using System.Linq;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Lists
{
    internal static class Generics
    {
        internal static bool IsList(Type type)
        {
            if (type == null) { return false; }

            var interfaceTest = new Predicate<Type>(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IList<>));

            return interfaceTest(type) || type.GetInterfaces().Any(i => interfaceTest(i));
        }
    }
}
