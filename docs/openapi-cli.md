# Aliencube.AzureFunctions.Extensions.OpenApi.CLI #

![Build and Test](https://github.com/aliencube/AzureFunctions.Extensions/workflows/Build%20and%20Test/badge.svg) [![](https://img.shields.io/static/v1?label=tag&message=cli-*&color=brightgreen)](https://github.com/aliencube/AzureFunctions.Extensions/releases) [![](https://img.shields.io/static/v1?label=tag&message=cli-*&color=brightgreen)](https://github.com/aliencube/AzureFunctions.Extensions/releases)

This generates Open API document through command-line without having to run the Azure Functions instance. The more details around this CLI can be found on this [blog post](https://devkimchi.com/2020/07/08/generating-open-api-doc-for-azure-functions-in-command-line/).

> **NOTE**: This CLI supports both [Open API 2.0 (aka Swagger)](https://github.com/OAI/OpenAPI-Specification/blob/master/versions/2.0.md) and [Open API 3.0.1](https://github.com/OAI/OpenAPI-Specification/blob/master/versions/3.0.1.md) spec.


## Acknowledgement ##

* In order to read JSON configuration on Azure Functions 1.x, these extensions have copied the source code of [Microsoft.Extensions.Configuration.Json](https://github.com/aspnet/Extensions/tree/master/src/Configuration/Config.Json) to make use of [Newtonsoft.Json 9.0.1](https://www.nuget.org/packages/Newtonsoft.Json/9.0.1) under the [MIT License](http://opensource.org/licenses/MIT).


## Getting Started ##

### Download CLI ###

The CLI is available for download at [GitHub](https://github.com/aliencube/AzureFunctions.Extensions/releases). It's always tagged with `cli-<version>`. Download the latest version of CLI.

* For Azure Functions v1
  * Windows only: `azfuncopenapi-v<version>-net461-win-x64.zip`
* For Azure Functions v2 or later
  * Linux: `azfuncopenapi-v<version>-netcoreapp3.1-linux-x64.zip`
  * MacOS: `azfuncopenapi-v<version>-netcoreapp3.1-osx-x64.zip`
  * Windows: `azfuncopenapi-v<version>-netcoreapp3.1-win-x64.zip`


### Generating Open API Document ###

Once you have an Azure Functions instance with [Azure Functions Open API extension](openapi.md) enabled, then you are ready to run this CLI.

For Windows:

```powershell
# PowerShell Console
azfuncopenapi `
    --project <PROJECT_PATH> `
    --configuration Debug `
    --target netcoreapp2.1 `
    --version v2 `
    --format json `
    --output output `
    --console false
```

For Linux/MacOS

```bash
# Bash
./azfuncopenapi \
    --project <PROJECT_PATH> \
    --configuration Debug \
    --target netcoreapp2.1 \
    --version v2 \
    --format json \
    --output output \
    --console false
```

Here are options:

* `--project|-p`: Project path. It can be a fully qualified project path including `.csproj` or project directory. Default is the current directory.
* `--configuration|-c`: Configuration value. It can be either `Debug`, `Release` or something else. Default is `Debug`.
* `--target|-t`: Target framework. It should be `net4x` for Azure Functions v1, `netcoreapp2.x` for Azure Functions v2, and `netcoreapp3.x` for Azure Functions v3. Default is `netcoreapp2.1`.
* `--version|-v`: Open API spec version. It should be either `v2` or `v3`. Default is `v2`.
* `--format|-f`: Open API document format. It should be either `json` or `yaml`. Default is `json`.
* `--output|-o`: Output directory for the generated Open API document. It can be a fully qualified directory path or relative path from `<PROJECT_ROOT>/bin/<CONFIGURATION>/<TARGET_FRAMEWORK>`. Default is `output`.
* `--console`: Value indicating whether to display the generated document to console or not. Default is `false`.


## Roadmap ##

* Distribution through a npm package.
* Project boilerplate generation, if an Open API doc is provided.

