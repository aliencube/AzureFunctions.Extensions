using System.Collections.Generic;
using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.FunctionApp.Models
{
    /// <summary>
    /// This represents the model entity for pet of Swagger Pet Store.
    /// </summary>
    public class Pet
    {
        /// <summary>
        /// Gets or sets the pet ID.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonRequired]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of photo URLs.
        /// </summary>
        [JsonRequired]
        public List<string> PhotoUrls { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the list of tags.
        /// </summary>
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the Tag1 <see cref="Tag"/> value.
        /// </summary>
        public Tag Tag1 { get; set; }

        /// <summary>
        /// Gets or sets the Tag2 <see cref="Tag"/> value.
        /// </summary>
        public Tag Tag2 { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="PetStatus"/> value.
        /// </summary>
        public PetStatus? Status { get; set; }
    }
}
