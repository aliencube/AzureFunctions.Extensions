> As the [Azure Functions OpenAPI Extension](https://github.com/Azure/azure-functions-openapi-extension/) package was [officially announced]((https://techcommunity.microsoft.com/t5/apps-on-azure/create-and-publish-openapi-enabled-azure-functions-with-visual/ba-p/2381067?WT.mc_id=github-0000-juyoo)) during the [//Build](https://mybuild.microsoft.com/sessions/5ac55e8d-82e5-4b9f-b9bc-d51187761b42?WT.mc_id=github-0000-juyoo) event in May 2021, this repository has now been archived.
> 
> If you have any questions around this extension, please pile up an issue to [here](https://github.com/Azure/azure-functions-openapi-extension/issues).


# AzureFunctions.Extensions #

This provides some useful extensions for Azure Functions.


## Getting Started ![Build and Test](https://github.com/aliencube/AzureFunctions.Extensions/workflows/Build%20and%20Test/badge.svg) ##

| Package | Status | Version |
| --- | --- | --- |
| [Aliencube.AzureFunctions.Extensions.Configuration.AppSettings](./docs/app-settings.md) | [![](https://img.shields.io/nuget/dt/Aliencube.AzureFunctions.Extensions.Configuration.AppSettings.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.Configuration.AppSettings/) | [![](https://img.shields.io/nuget/v/Aliencube.AzureFunctions.Extensions.Configuration.AppSettings.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.Configuration.AppSettings/) |
| [Aliencube.AzureFunctions.Extensions.Configuration.Json](./docs/configuration-json.md) | [![](https://img.shields.io/nuget/dt/Aliencube.AzureFunctions.Extensions.Configuration.Json.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.Configuration.Json/) | [![](https://img.shields.io/nuget/v/Aliencube.AzureFunctions.Extensions.Configuration.Json.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.Configuration.Json/) |
| [Aliencube.AzureFunctions.Extensions.DependencyInjection](./docs/dependency-injection.md) | [![](https://img.shields.io/nuget/dt/Aliencube.AzureFunctions.Extensions.DependencyInjection.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.DependencyInjection/) | [![](https://img.shields.io/nuget/v/Aliencube.AzureFunctions.Extensions.DependencyInjection.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.DependencyInjection/) |
| [Aliencube.AzureFunctions.Extensions.OpenApi.Core](./docs/openapi-core.md) | [![](https://img.shields.io/nuget/dt/Aliencube.AzureFunctions.Extensions.OpenApi.Core.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.OpenApi.Core/) | [![](https://img.shields.io/nuget/v/Aliencube.AzureFunctions.Extensions.OpenApi.Core.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.OpenApi.Core/) |
| [Aliencube.AzureFunctions.Extensions.OpenApi](./docs/openapi.md) | [![](https://img.shields.io/nuget/dt/Aliencube.AzureFunctions.Extensions.OpenApi.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.OpenApi/) | [![](https://img.shields.io/nuget/v/Aliencube.AzureFunctions.Extensions.OpenApi.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.OpenApi/) |
| [Aliencube.AzureFunctions.Extensions.OpenApi.CLI](./docs/openapi-cli.md) | [![](https://img.shields.io/static/v1?label=tag&message=cli-*&color=brightgreen)](https://github.com/aliencube/AzureFunctions.Extensions/releases) | [![](https://img.shields.io/static/v1?label=tag&message=cli-*&color=brightgreen)](https://github.com/aliencube/AzureFunctions.Extensions/releases) |


## Acknowledgement ##

* In order to read JSON configuration on Azure Functions 1.x, these extensions have copied the source code of [Microsoft.Extensions.Configuration.Json](https://github.com/aspnet/Extensions/tree/master/src/Configuration/Config.Json) to make use of [Newtonsoft.Json 9.0.1](https://www.nuget.org/packages/Newtonsoft.Json/9.0.1) under the [MIT License](http://opensource.org/licenses/MIT).
* [Swagger UI](https://github.com/swagger-api/swagger-ui) version used for this library is [3.20.5](https://github.com/swagger-api/swagger-ui/releases/tag/v3.20.5) under the [Apache 2.0 license](https://opensource.org/licenses/Apache-2.0).


## Contribution ##

Your contributions are always welcome! All your work should be done in your forked repository. Once you finish your work with corresponding tests, please send us a pull request onto our `dev` branch for review.


## License ##

**AzureFunctions.Extensions** is released under [MIT License](http://opensource.org/licenses/MIT)

> The MIT License (MIT)
>
> Copyright (c) 2018 [aliencube.org](http://aliencube.org)
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
