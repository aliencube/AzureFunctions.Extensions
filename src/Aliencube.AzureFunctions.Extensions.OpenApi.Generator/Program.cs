using System;
using System.Reflection;
using System.Xml.Linq;

using Aliencube.AzureFunctions.Extensions.OpenApi.Configurations;

using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi;

using Moq;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var projectPath = @"C:\dev\ac\AzureFunctions.Extensions\src\Aliencube.AzureFunctions.FunctionAppV3\";
            var projectName = @"Aliencube.AzureFunctions.FunctionAppV3.csproj";
            var projectFile = $"{projectPath}{projectName}";
            var projectCompiledPath = @"bin\Debug\netcoreapp3.1\";
            var projectDll = @"bin\Aliencube.AzureFunctions.FunctionAppV3.dll";
            var dllPath = $"{projectPath}{projectCompiledPath}{projectDll}";
            var hostJsonPath = $"{projectPath}{projectCompiledPath}host.json";
            var appSettingsPath = $"{projectPath}{projectCompiledPath}local.settings.json";


            var xml = XDocument.Load(projectFile)
                               .Root;

            var assembly = Assembly.LoadFrom(dllPath);
            var req = new Mock<HttpRequest>();
            req.SetupGet(p => p.Scheme).Returns("http");
            req.SetupGet(p => p.Host).Returns(new HostString("localhost", 7071));

            var filter = new RouteConstraintFilter();
            var helper = new DocumentHelper(filter);
            var document = new Document(helper);

            var swagger = document.InitialiseDocument()
                                  .AddMetadata(null)
                                  .AddServer(req.Object, null)
                                  .Build(assembly)
                                  .RenderAsync(OpenApiSpecVersion.OpenApi2_0, OpenApiFormat.Json)
                                  .Result;

            Console.WriteLine(swagger);
        }
    }
}
