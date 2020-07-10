using System.Collections.Generic;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Fakes
{
    /// <summary>
    /// This represents the fake model entity.
    /// </summary>
    public class FakeModelWithList
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public FakeSubModel Parent { get; set; }
        public List<FakeSubModel> Items { get; set; }
    }
}
