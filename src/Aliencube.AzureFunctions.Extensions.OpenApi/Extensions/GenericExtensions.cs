using System;
using System.Collections.Generic;
using System.Linq;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Extensions
{
    /// <summary>
    /// This represents the extension entity for generic.
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Checks whether the given instance is <c>null</c> or default.
        /// </summary>
        /// <typeparam name="T">Type of instance.</typeparam>
        /// <param name="instance">Instance to check.</param>
        /// <returns><c>true</c>, if the original instance is <c>null</c> or empty; otherwise returns <c>false</c>.</returns>
        public static bool IsNullOrDefault<T>(this T instance)
        {
            return instance == null || instance.Equals(default(T));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the given instance is <c>null</c> or default.
        /// </summary>
        /// <typeparam name="T">Type of instance.</typeparam>
        /// <param name="instance">Instance to check.</param>
        /// <returns>The original instance, if the instance is NOT <c>null</c>; otherwise throws an <see cref="ArgumentNullException"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="instance"/> is <see langword="null"/></exception>
        public static T ThrowIfNullOrDefault<T>(this T instance)
        {
            if (instance.IsNullOrDefault())
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return instance;
        }

        /// <summary>
        /// Checks whether the given list of items is empty.
        /// </summary>
        /// <typeparam name="T">Type of instance.</typeparam>
        /// <param name="items">List of instances.</param>
        /// <returns><c>true</c>, if the given list of items is empty; otherwise returns <c>false</c>.</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            items.ThrowIfNullOrDefault();

            return !items.Any();
        }

        public static void AddRange<T, S>(this Dictionary<T, S> source, Dictionary<T, S> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("Collection is null");
            }

            foreach (var item in collection)
            {
                if (!source.ContainsKey(item.Key))
                {
                    source.Add(item.Key, item.Value);
                }
            }
        }
    }
}
