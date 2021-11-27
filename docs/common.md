# Aliencube.AzureFunctions.Extensions.Common #

This extension provides constants and extension methods frequently used while developing Azure Functions apps.


## Constants ##

The following constants represent the string respectively.


### `ContentTypes` ###

It specifies the content type.

* `ContentTypes.PlainText`: `text/plain`
* `ContentTypes.TextHtml`: `text/html`
* `ContentTypes.ApplicationJson`: `application/json`
* `ContentTypes.TextVndYaml`: `text/vnd.yaml`


### `HttpVerbs` ###

It specifies the HTTP method.

* `HttpVerbs.GET`: `GET`
* `HttpVerbs.POST`: `POST`
* `HttpVerbs.PUT`: `PUT`
* `HttpVerbs.PATCH`: `PATCH`
* `HttpVerbs.DELETE`: `DELETE`


## Enums ##

### `SourceFrom` ###

It specifies whether the HTTP request comes from.

* `Header`: Request source comes from the request header.
* `Query`: Request source comes from the request querystring.
* `Body`: Request source comes from the request body.


## Extension Methods ##

### `HttpRequestExtensions.To<T>(SourceFrom source)` ###

It extracts relevant data from the given source of the HTTP request.

```csharp
public async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, HttpVerbs.GET)] HttpRequest req)
{
    var headers = await req.To<IHeaderDictionary>(SourceFrom.Header)
                           .ConfigureAwait(false);

    var queries = await req.To<IQueryCollection>(SourceFrom.Query)
                           .ConfigureAwait(false);

    var payload = await req.To<MyClass>(SourceFrom.Body)
                           .ConfigureAwait(false);
    ...
}
```


### `OpenApiFormatExtensions.GetContentType()` ###

It returns the content type based on the OpenAPI document format.

```csharp
var format = OpenApiFormat.Json;
var contentType = format.GetContentType();
```


### `PayloadExtensions.ToJson<T>()` ###

It serialises the given payload to JSON string.

```csharp
var payload = new MyClass() { Message = "hello world" };
var serialised = payload.ToJson();
```


### `PayloadExtensions.FromJson<T>()` ###

It deserialises the JSON string value to given type.

```csharp
var payload = "{ \"message\": \"hello world\" }"
var deserialised = payload.FromJson<MyClass>(payload);
```
