using System;

namespace Aliencube.AzureFunctions.Tests.Fakes
{
    public interface IFakeInheritedInterface : IFakeBaseInterface
    {
        DateTime Birthdate { get; set; }
        double Age { get; set; }
    }
}