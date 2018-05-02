# AzureFunctions.Extensions #

This provides some useful extensions for Azure Functions.


## Aliencube.AzureFunctions.Extensions.DependencyInjection ##

This enables Azure Functions to utilise an IoC container offered by ASP.NET Core. The more details around the dependency injections on Azure Functions can be found on this [blog post](https://devkimchi.com/2018/04/07/dependency-injections-on-azure-functions-v2/).

> This extension uses the `property injection` approach. If the `methodd injection` approach is preferred, visit [https://github.com/rikvandenberg/AzureFunctions.Extensions.DependencyInjection](https://github.com/rikvandenberg/AzureFunctions.Extensions.DependencyInjection).


### `ContainerBuilder` ###

`ContainerBuilder` builds `IServiceProvider` so that each Function can directly use the IoC container within the method. In order to use the `ContainerBuilder` into the Function, the `IServiceProvider` property should be defined as a `static` property first like:

```csharp
public static class SampleHttpTrigger
{
    public static IServiceProvider Container = new ContainerBuilder
                                                   .RegisterModule<AppModule>()
                                                   .Build();

    [FunctionName("SampleHttpTrigger")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
        ILogger log)
    {
        var function = Container.GetService<IFunction>();
        var result = await function.InvokeAsync<HttpRequest, IActionResult>(req)
                                   .ConfigureAwait(false);

        return result;
    }
}
```

### `FunctionFactory` ###

Instead of directly using the `ContainerBuilder`, the `FunctionFactory` class might be more useful. In order to use the `FunctionFactory` into the Function, the `IFunctionFactory` property should be defined as a `static` property like:

```csharp
public static class SampleHttpTrigger
{
    public static IFunctionFactory Factory = new FunctionFactory<AppModule>();

    [FunctionName("SampleHttpTrigger")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
        ILogger log)
    {
        var result = await Factory.Create<ISampleHttpFunction, ILogger>(log)
                                  .InvokeAsync<HttpRequest, IActionResult>(req)
                                  .ConfigureAwait(false);
        return result;
    }
}
```


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
