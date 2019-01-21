using System.Collections.Generic;

using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.FunctionAppV1.Models
{
    public class SampleModel
    {
        public string Name { get; set; }
        [JsonIgnore]
        public string Description { get; set; }
        public SampleSubModel Sub { get; set; }
        public Dictionary<string, string> Dict { get; set; } = new Dictionary<string, string>();
        public List<int> Items { get; set; } = new List<int>();
    }

    public class SampleSubModel
    {
        public int Value { get; set; }
    }
}
