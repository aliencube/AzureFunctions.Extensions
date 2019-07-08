# Aliencube.AzureFunctions.Extensions.OpenApi #

[![Build status](https://ci.appveyor.com/api/projects/status/6ex8if2l1ffdahfq/branch/dev?svg=true)](https://ci.appveyor.com/project/justinyoo/azurefunctions-extensions/branch/dev) [![](https://img.shields.io/nuget/dt/Aliencube.AzureFunctions.Extensions.OpenApi.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.OpenApi/) [![](https://img.shields.io/nuget/v/Aliencube.AzureFunctions.Extensions.OpenApi.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.OpenApi/)

This enables Azure Functions to render Open API document and Swagger UI. The more details around the Swagger UI on Azure Functions can be found on this [blog post](https://devkimchi.com/2019/02/02/introducing-swagger-ui-on-azure-functions/).

> **NOTE**: This extension supports both [Open API 2.0 (aka Swagger)](https://github.com/OAI/OpenAPI-Specification/blob/master/versions/2.0.md) and [Open API 3.0.1](https://github.com/OAI/OpenAPI-Specification/blob/master/versions/3.0.1.md) spec.


## Acknowledgement ##

* In order to read JSON configuration on Azure Functions 1.x, these extensions have copied the source code of [Microsoft.Extensions.Configuration.Json](https://github.com/aspnet/Extensions/tree/master/src/Configuration/Config.Json) to make use of [Newtonsoft.Json 9.0.1](https://www.nuget.org/packages/Newtonsoft.Json/9.0.1) under the [MIT License](http://opensource.org/licenses/MIT).
* [Swagger UI](https://github.com/swagger-api/swagger-ui) version used for this library is [3.20.5](https://github.com/swagger-api/swagger-ui/releases/tag/v3.20.5) under the [Apache 2.0 license](https://opensource.org/licenses/Apache-2.0).


## Getting Started ##

### Rendering Open API Document ###

In order to include an HTTP endpoint in the Open API document, use attribute classes (decorators) like:

```csharp
[FunctionName(nameof(PostSample))]
[OpenApiOperation("add", "sample")]
[OpenApiRequestBody("application/json", typeof(SampleRequestModel))]
[OpenApiResponseBody(HttpStatusCode.OK, "application/json", typeof(SampleResponseModel))]
public static async Task<IActionResult> PostSample(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "samples")] HttpRequest req,
    ILogger log)
{
    ...
}
```

Then, define another HTTP endpoint to render Swagger document:

```csharp
[FunctionName(nameof(RenderSwaggerDocument))]
[OpenApiIgnore]
public static async Task<IActionResult> RenderSwaggerDocument(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "swagger.json")] HttpRequest req,
    ILogger log)
{
    var settings = new AppSettings();
    var filter = new RouteConstraintFilter();
    var helper = new DocumentHelper(filter);
    var document = new Document(helper);
    var result = await document.InitialiseDocument()
                               .AddMetadata(settings.OpenApiInfo)
                               .AddServer(req, settings.HttpSettings.RoutePrefix)
                               .Build(Assembly.GetExecutingAssembly(), new CamelCaseNamingStrategy())
                               .RenderAsync(OpenApiSpecVersion.OpenApi2_0, OpenApiFormat.Json)
                               .ConfigureAwait(false);
    var response = new ContentResult()
                       {
                           Content = result,
                           ContentType = "application/json",
                           StatusCode = (int)HttpStatusCode.OK
                       };

    return response;
}
```

> **NOTE**: In order to render payload definitions in `camelCasing`, add `new CamelCaseNamingStrategy()` as an optional argument to the `Build()` method. If this is omitted, payload will be rendered as defined in the payload definitions.


### Rendering Swagger UI ###

In order to render Swagger UI, define another HTTP endpoint for it:

```csharp
[FunctionName(nameof(RenderSwaggerUI))]
[OpenApiIgnore]
public static async Task<IActionResult> RenderSwaggerUI(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "swagger/ui")] HttpRequest req,
    ILogger log)
{
    var settings = new AppSettings();
    var ui = new SwaggerUI();
    var result = await ui.AddMetadata(settings.OpenApiInfo)
                         .AddServer(req, settings.HttpSettings.RoutePrefix)
                         .BuildAsync()
                         .RenderAsync("swagger.json", settings.SwaggerAuthKey)
                         .ConfigureAwait(false);
    var response = new ContentResult()
                       {
                           Content = result,
                           ContentType = "text/html",
                           StatusCode = (int)HttpStatusCode.OK
                       };

    return response;
}
```


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
* `OpenApi__ApiKey`: API Key of the endpoint that renders the Open API document.

> **NOTE**: In order to deploy Azure Functions v1 to Azure, the `AzureWebJobsScriptRoot` **MUST** be specified in the app settings section; otherwise it will throw an error that can't find `host.json`. Local debugging is fine, though. For more details, please visit [this page](https://docs.microsoft.com/bs-latn-ba/azure/azure-functions/functions-app-settings#azurewebjobsscriptroot).


## Decorators ##

In order to render Open API document, this uses attribute classes (decorators).

> **NOTE**: Not all Open API specs have been implemented.


### `OpenApiIgnoreAttribute` ###

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


### `OpenApiOperationAttribute` ###

This decorator implements a part of [Operation object](https://github.com/OAI/OpenAPI-Specification/blob/master/versions/3.0.1.md#operationObject) spec.

```csharp
[FunctionName(nameof(GetSample))]
[OpenApiOperation(operationId: "list", tags: new[] { "sample" })]
...
public static async Task<IActionResult> GetSample(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "samples")] HttpRequest req,
    ILogger log)
{
    ...
}
```

* `OperationId`: is the ID of the operation. If this is omitted, a combination of function name and verb is considered as the operation ID. eg) `Get_GetSample`
* `Tags`: are the list of tags of operation.
* `Summary`: is the summary of the operation.
* `Description`: is the description of the operation.
* `Visibility`: indicates how the operation is visible in Azure Logic Apps &ndash; `important`, `advanced` or `internal`. Default value is `undefined`.


### `OpenApiParameterAttribute` ###

This decorator implements the [Parameter object](https://github.com/OAI/OpenAPI-Specification/blob/master/versions/3.0.1.md#parameterObject) spec.

```csharp
[FunctionName(nameof(GetSample))]
[OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string))]
...
public static async Task<IActionResult> GetSample(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "samples")] HttpRequest req,
    ILogger log)
{
    ...
}
```

* `Name`: is the name of the parameter.
* `Summary`: is the summary of the parameter.
* `Description`: is the description of the parameter.
* `Type`: defines the parameter type. Default value is `typeof(string)`.
* `In`: identifies where the parameter is located &ndash; `header`, `path`, `query` or `cookie`. Default value is `path`.
* `Required`: indicates whether the parameter is required or not. Default value is `false`.
* `Visibility`: indicates how the parameter is visible in Azure Logic Apps &ndash; `important`, `advanced` or `internal`. Default value is `undefined`.


### `OpenApiRequestBodyAttribute` ###

This decorator implements the [Request Body object](https://github.com/OAI/OpenAPI-Specification/blob/master/versions/3.0.1.md#requestBodyObject) spec.

```csharp
[FunctionName(nameof(PostSample))]
[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SampleRequestModel))]
...
public static async Task<IActionResult> PostSample(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "samples")] HttpRequest req,
    ILogger log)
{
    ...
}
```

* `ContentType`: defines the content type of the request body payload. eg) `application/json` or `text/xml`
* `BodyType`: defines the type of the request payload.
* `Description`: is the description of the request body payload.


### `OpenApiResponseBodyAttribute` ###

This decorator implements the [Response object](https://github.com/OAI/OpenAPI-Specification/blob/master/versions/3.0.1.md#responseObject) spec.

```csharp
[FunctionName(nameof(PostSample))]
[OpenApiResponseBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SampleResponseModel))]
...
public static async Task<IActionResult> PostSample(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "samples")] HttpRequest req,
    ILogger log)
{
    ...
}
```

* `StatusCode`: defines the HTTP status code. eg) `HttpStatusCode.OK`
* `ContentType`: defines the content type of the response body payload. eg) `application/json` or `text/xml`
* `BodyType`: defines the type of the response payload.
* `Description`: is the description of the response body payload.
* `Summary`: is the summary of the response body payload.


## Supported Json.NET Decorators ##

Those attribute classes from [Json.NET](https://www.newtonsoft.com/json) are supported for payload definitions.


### `JsonIgnore` ###

Properties decorated with the `JsonIgnore` attribute class will not be included in the response.


### `JsonProperty` ###

Properties decorated with `JsonProperty` attribute class will use `JsonProperty.Name` value instead of their property names.


### `JsonConverter` ###

Enums types decorated with `[JsonConverter(typeof(StringEnumConverter))]` will appear in the document with their string names (names mangled based on default property naming standard).
