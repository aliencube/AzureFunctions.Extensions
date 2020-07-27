using System;

using Newtonsoft.Json;

namespace Aliencube.AzureFunctions.FunctionApp.Models
{
    public class DummyDictionaryResponseModel
    {
        public Guid? Id { get; set; }

        [JsonRequired]
        public string JsonRequiredValue { get; set; }
    }
}
