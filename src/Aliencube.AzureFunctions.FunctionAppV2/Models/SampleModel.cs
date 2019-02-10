using System;
using System.Collections.Generic;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;

using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.FunctionAppV2.Models
{
    /// <summary>
    /// This represents the model entity for sample request.
    /// </summary>
    public class SampleRequestModel
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public string Id { get; set; }
    }

    /// <summary>
    /// This represents the model entity for sample response.
    /// </summary>
    public class SampleResponseModel
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Advanced)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonIgnore]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SubSampleResponseModel"/> instance.
        /// </summary>
        public SubSampleResponseModel Sub { get; set; }

        /// <summary>
        /// Gets or sets the dictionary object.
        /// </summary>
        public Dictionary<string, string> Collection { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets the list object.
        /// </summary>
        public List<int> Items { get; set; } = new List<int>();
    }

    /// <summary>
    /// This represents the sub model entity for sample response
    /// </summary>
    public class SubSampleResponseModel
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public int Value { get; set; }
    }
}
