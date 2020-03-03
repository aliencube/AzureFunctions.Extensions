﻿namespace Aliencube.AzureFunctions.Tests.Fakes
{
    /// <summary>
    /// This represents the fake model entity.
    /// </summary>
    public class FakeModel
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string FakeProperty { get; set; }
        public int? NullableInt { get; set; }
        public FakeSubModel SubProperty { get; set; }
        public FakeEnum EnumProperty { get; set; }

    }
}
