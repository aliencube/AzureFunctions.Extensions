using System;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Tests.Fakes
{
    public interface IFakeInheritedInterface : IFakeBaseInterface
    {
        DateTime Birthdate { get; set; }
        double Age { get; set; }
    }
}