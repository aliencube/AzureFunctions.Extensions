﻿namespace Aliencube.AzureFunctions.Tests.Fakes
{
    /// <summary>
    /// This represents the fake model entity.
    /// </summary>
    public class FakeModelWithCircularRefSub
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public FakeModelWithCircularRef Circle { get; set; }
    }
}
