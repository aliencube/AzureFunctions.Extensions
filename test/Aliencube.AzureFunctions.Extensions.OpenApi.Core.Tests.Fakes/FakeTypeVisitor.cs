using System;

using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Visitors;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Core.Tests.Fakes
{
    public class FakeTypeVisitor : TypeVisitor
    {
        public FakeTypeVisitor(VisitorCollection visitorCollection)
            : base(visitorCollection)
        {
        }

        public bool IsTypeReferential(Type type)
        {
            return this.IsReferential(type);
        }
    }
}
