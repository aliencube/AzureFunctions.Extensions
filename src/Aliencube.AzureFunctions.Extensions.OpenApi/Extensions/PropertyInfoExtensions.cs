using System.Reflection;

using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Extensions
{
    /// <summary>
    /// This represents the extension entity for <see cref="PropertyInfo"/> class.
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Gets the name from <see cref="JsonPropertyAttribute"/> instance.
        /// </summary>
        /// <param name="element"><see cref="PropertyInfo"/> instance.</param>
        /// <returns>Returns the name from <see cref="JsonPropertyAttribute"/> instance.</returns>
        public static string GetJsonPropertyName(this PropertyInfo element)
        {
            if (element.ExistsCustomAttribute<JsonPropertyAttribute>())
            {
                var name = element.GetCustomAttribute<JsonPropertyAttribute>().PropertyName;

                return name;

            }

            return element.Name;
        }
    }
}