using System;
using System.IO;

#if NET461
using System.Net.Http;
#endif

using System.Reflection;
using System.Text;

using Aliencube.AzureFunctions.Extensions.OpenApi.Configurations;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;
using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;

using Cocona;

#if !NET461
using Microsoft.AspNetCore.Http;
#endif

using Moq;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.CLI
{
    /// <summary>
    /// This represents the console app entity.
    /// </summary>
    public class Program
    {
#if NET461
        private const string Target = "net461";
#elif NETCOREAPP2_1
        private const string Target = "netcoreapp2.1";
#elif NETCOREAPP3_1
        private const string Target = "netcoreapp3.1";
#endif

        private static readonly char directorySeparator = System.IO.Path.DirectorySeparatorChar;

        /// <summary>
        /// Invokes the console app.
        /// </summary>
        /// <param name="args">List of arguments.</param>
        public static void Main(string[] args)
        {
            CoconaLiteApp.Run<Program>(args);
        }

        /// <summary>
        /// Generates the Open API document.
        /// </summary>
        /// <param name="project">Project path.</param>
        /// <param name="configuration">Copile configuration.</param>
        /// <param name="version">Open API version.</param>
        /// <param name="format">Open API output format.</param>
        /// <param name="output">Output path.</param>
        /// <param name="console">Value indicating whether to render the document on console or not.</param>
        public void Generate(
            [Option('p')] string project = ".",
            [Option('c')] string configuration = "Debug",
            [Option('v')] OpenApiVersionType version = OpenApiVersionType.V2,
            [Option('f')] OpenApiFormatType format = OpenApiFormatType.Json,
            [Option('o')] string output = "output",
            bool console = false)
        {
            //var projectPath = @"C:\dev\ac\AzureFunctions.Extensions\src\Aliencube.AzureFunctions.FunctionAppV3\";
            //var projectName = @"Aliencube.AzureFunctions.FunctionAppV3.csproj";
            //var projectFile = $"{projectPath}{projectName}";
            //var projectCompiledPath = @"bin\Debug\netcoreapp3.1\";
            //var projectDll = @"bin\Aliencube.AzureFunctions.FunctionAppV3.dll";
            //var dllPath = $"{projectPath}{projectCompiledPath}{projectDll}";
            //var hostJsonPath = $"{projectPath}{projectCompiledPath}host.json";
            //var appSettingsPath = $"{projectPath}{projectCompiledPath}local.settings.json";


            //var xml = XDocument.Load(projectFile)
            //                   .Root;

            var pi = new ProjectInfo(project, configuration, Target);

            var assembly = Assembly.LoadFrom(pi.CompiledDllPath);
#if NET461
            var req = new Mock<HttpRequestMessage>();
            req.SetupGet(p => p.RequestUri).Returns(new Uri("http://localhost:7071"));
#else
            var req = new Mock<HttpRequest>();
            req.SetupGet(p => p.Scheme).Returns("http");
            req.SetupGet(p => p.Host).Returns(new HostString("localhost", 7071));
#endif

            var filter = new RouteConstraintFilter();
            var helper = new DocumentHelper(filter);
            var document = new Document(helper);

            var swagger = document.InitialiseDocument()
                                  .AddMetadata(pi.OpenApiInfo)
                                  .AddServer(req.Object, pi.HostJsonHttpSettings.RoutePrefix)
                                  .Build(assembly)
                                  .RenderAsync(version.ToOpenApiSpecVersion(), format.ToOpenApiFormat())
                                  .Result;

            if (console)
            {
                Console.WriteLine(swagger);
            }

            var outputpath =
#if NET461
                Path.IsPathRooted(output)
#else
                Path.IsPathFullyQualified(output)
#endif
                ? output
                : $"{pi.CompiledPath}{directorySeparator}{output}";

            if (!Directory.Exists(outputpath))
            {
                Directory.CreateDirectory(outputpath);
            }

            File.WriteAllText($"{outputpath}{directorySeparator}swagger.{format.ToDisplayName()}", swagger, Encoding.UTF8);
        }
    }
}
