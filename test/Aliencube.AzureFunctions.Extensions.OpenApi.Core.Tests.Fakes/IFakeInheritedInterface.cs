using System;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Core.Tests.Fakes
{
    public interface IFakeInheritedInterface : IFakeBaseInterface
    {
        DateTime Birthdate { get; set; }
        double Age { get; set; }
    }
}
