using System;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Attributes
{
    /// <summary>
    /// This represents the attribute entity for HTTP triggers to be excluded for rendering.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class OpenApiIgnoreAttribute : Attribute
    {
    }
}
