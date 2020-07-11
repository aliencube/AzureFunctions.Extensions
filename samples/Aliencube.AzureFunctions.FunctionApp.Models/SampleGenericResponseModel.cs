using System.Collections.Generic;

namespace Aliencube.AzureFunctions.FunctionApp.Models
{
    /// <summary>
    /// This represents the generic model entity.
    /// </summary>
    /// <typeparam name="T">Type of item.</typeparam>
    public class SampleGenericResponseModel<T>
    {
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the list of items.
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Gets or sets the collection of items.
        /// </summary>
        public Dictionary<string, T> Collection { get; set; }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        public T Item { get; set; }
    }
}