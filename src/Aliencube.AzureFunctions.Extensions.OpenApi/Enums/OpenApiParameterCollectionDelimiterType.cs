using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Enums
{
    /// <summary>
    /// This specifies the parameter collection delimiter.
    /// </summary>
    public enum OpenApiParameterCollectionDelimiterType
    {
        /// <summary>
        /// Identifies "comma".
        /// </summary>
        [Display("comma")]
        Comma = 0,

        /// <summary>
        /// Identifies "space".
        /// </summary>
        [Display("space")]
        Space = 1,

        /// <summary>
        /// Identifies "pipe".
        /// </summary>
        [Display("pipe")]
        Pipe = 2
    }
}
