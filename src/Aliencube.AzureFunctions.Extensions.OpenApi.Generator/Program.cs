using System;
using System.Linq;

using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var project = new Project(@"Y:\Documents\devs\ac\AzureFunctions.Extensions\src\Aliencube.AzureFunctions.FunctionAppV3\Aliencube.AzureFunctions.FunctionAppV3.csproj");
            var pg = project.Properties.OfType<ProjectPropertyGroupElement>().First();
            var pgps = pg.Properties;
        }
    }
}
