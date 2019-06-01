using System;
using System.Collections.Generic;

using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Aliencube.AzureFunctions.FunctionAppCommon.Models
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
        /// Gets or sets the <see cref="JObject"/> instance.
        /// </summary>
        public JObject JsonObject { get; set; } = new JObject();

        /// <summary>
        /// Gets or sets the list objects 1.
        /// </summary>
        public List<int> Items1 { get; set; } = new List<int>();

        /// <summary>
        /// Gets or sets the list of objects 2.
        /// </summary>
        public int[] Items2 { get; set; } = new List<int>().ToArray();
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
