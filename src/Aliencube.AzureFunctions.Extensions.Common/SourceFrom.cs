namespace Aliencube.AzureFunctions.Extensions.Common
{
    /// <summary>
    /// This specifies the source of the request to parse.
    /// </summary>
    public enum SourceFrom
    {
        /// <summary>
        /// Identifies none.
        /// </summary>
        None = 0,

        /// <summary>
        /// Identifies the request header.
        /// </summary>
        Header = 1,

        /// <summary>
        /// Identifies the request query.
        /// </summary>
        Query = 2,

        /// <summary>
        /// Identifies the request body.
        /// </summary>
        Body = 3
    }
}
