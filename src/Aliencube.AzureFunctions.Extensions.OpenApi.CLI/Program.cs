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
        private static readonly char directorySeparator = Path.DirectorySeparatorChar;

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
            [Option('p', Description = "Project path. Default is current directory")] string project = ".",
            [Option('c', Description = "Configuration. Default is 'Debug'")] string configuration = "Debug",
            [Option('t', Description = "Target framework. Default is 'netcoreapp2.1'")] string target = "netcoreapp2.1",
            [Option('v', Description = "Open API spec version. Value can be either 'v2' or 'v3'. Default is 'v2'")] OpenApiVersionType version = OpenApiVersionType.V2,
            [Option('f', Description = "Open API output format. Value can be either 'json' or 'yaml'. Default is 'yaml'")] OpenApiFormatType format = OpenApiFormatType.Json,
            [Option('o', Description = "Generated Open API output location. Default is 'output'")] string output = "output",
            bool console = false)
        {
            var pi = default(ProjectInfo);
            try
            {
                pi = new ProjectInfo(project, configuration, target);
            }
            catch
            {
                return;
            }

            var assembly = Assembly.LoadFrom(pi.CompiledDllPath);
#if NET461
            var req = new HttpRequestMessage();
            req.RequestUri = new Uri("http://localhost:7071");
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
#if NET461
                                  .AddServer(req, pi.HostJsonHttpSettings.RoutePrefix)
#else
                                  .AddServer(req.Object, pi.HostJsonHttpSettings.RoutePrefix)
#endif
                                  .Build(assembly)
                                  .RenderAsync(version.ToOpenApiSpecVersion(), format.ToOpenApiFormat())
                                  .Result;
#if NET461
            req.Dispose();
#endif
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
