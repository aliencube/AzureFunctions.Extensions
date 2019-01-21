# Aliencube.AzureFunctions.Extensions.OpenApi #

[![Build status](https://ci.appveyor.com/api/projects/status/6ex8if2l1ffdahfq/branch/dev?svg=true)](https://ci.appveyor.com/project/justinyoo/azurefunctions-extensions/branch/dev) [![](https://img.shields.io/nuget/dt/Aliencube.AzureFunctions.Extensions.OpenApi.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.OpenApi/) [![](https://img.shields.io/nuget/v/Aliencube.AzureFunctions.Extensions.OpenApi.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.OpenApi/)


This enables Azure Functions to render Open API definition and Swagger UI. The more details around the Swagger UI on Azure Functions can be found on this [blog post](https://devkimchi.com/tba/).


## App Settings ##

On either your `local.settings.json` or App Settings on Azure Functions instance, those details should be set up to render Open API metadata:

* `OpenApi__Info__Version`: **REQUIRED** Version of Open API document. This is not the version of Open API spec. eg. 1.0.0
* `OpenApi__Info__Title`: **REQUIRED** Title of Open API document. eg. Open API Sample on Azure Functions
* `OpenApi__Info__Description`: Description of Open API document. eg. A sample API that runs on Azure Functions either 1.x or 2.x using Open API specification.
* `OpenApi__Info__TermsOfService`: Terms of service URL. eg. https://github.com/aliencube/AzureFunctions.Extensions
* `OpenApi__Info__Contact__Name`: Name of contact. eg. Aliencube Community
* `OpenApi__Info__Contact__Email`: Email address for the contact. eg. no-reply@aliencube.org
* `OpenApi__Info__Contact__Url`: Contact URL. eg. https://github.com/aliencube/AzureFunctions.Extensions/issues
* `OpenApi__Info__License__Name`: **REQUIRED** License name. eg. MIT
* `OpenApi__Info__License__Url`: License URL. eg. http://opensource.org/licenses/MIT


## `OpenApiIgnoreAttribute` ##

If there is any HTTP trigger that you want to exclude from the Open API document, use this decorator. Typically this is used for the endpoints that render Open API document and Swagger UI.

```csharp
[FunctionName(nameof(RenderSwaggerDocument))]
[OpenApiIgnore] // This HTTP endpoint is excluded from the Open API document.
public static async Task<IActionResult> RenderSwaggerDocument(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "swagger.{extension}")] HttpRequest req,
    string extension,
    ILogger log)
{
    ...
}
```


## `OpenApiOperationAttribute` ##

This decorator implements a part of [Operation object](https://github.com/OAI/OpenAPI-Specification/blob/master/versions/3.0.2.md#operationObject) spec.

```csharp
[FunctionName(nameof(GetSample))]
[OpenApiOperation("list", "sample")] // This defines the operation ID and tags.
...
public static async Task<IActionResult> GetSample(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "samples")] HttpRequest req,
    ILogger log)
{
    ...
}
```

If the operation ID is omitted, a combination of function name and verb is considered as the operation ID.
