# Aliencube.AzureFunctions.Extensions.DependencyInjection #

[![Build status](https://ci.appveyor.com/api/projects/status/6ex8if2l1ffdahfq/branch/dev?svg=true)](https://ci.appveyor.com/project/justinyoo/azurefunctions-extensions/branch/dev) [![](https://img.shields.io/nuget/dt/Aliencube.AzureFunctions.Extensions.DependencyInjection.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.DependencyInjection/) [![](https://img.shields.io/nuget/v/Aliencube.AzureFunctions.Extensions.DependencyInjection.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.DependencyInjection/)


This enables Azure Functions to utilise an IoC container offered by ASP.NET Core. The more details around the dependency injections on Azure Functions can be found on this [blog post](https://devkimchi.com/2018/04/07/dependency-injections-on-azure-functions-v2/).

> This extension uses the `property injection` approach. If the `methodd injection` approach is preferred, visit [https://github.com/rikvandenberg/AzureFunctions.Extensions.DependencyInjection](https://github.com/rikvandenberg/AzureFunctions.Extensions.DependencyInjection).


## `ContainerBuilder` ##

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

## `FunctionFactory` ##

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
