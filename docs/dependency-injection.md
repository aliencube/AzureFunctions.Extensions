# Aliencube.AzureFunctions.Extensions.DependencyInjection #

![Build and Test](https://github.com/aliencube/AzureFunctions.Extensions/workflows/Build%20and%20Test/badge.svg) [![](https://img.shields.io/nuget/dt/Aliencube.AzureFunctions.Extensions.DependencyInjection.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.DependencyInjection/) [![](https://img.shields.io/nuget/v/Aliencube.AzureFunctions.Extensions.DependencyInjection.svg)](https://www.nuget.org/packages/Aliencube.AzureFunctions.Extensions.DependencyInjection/)

This enables Azure Functions to utilise an IoC container offered by ASP.NET Core.


## Constructor Injection ##

This is only applicable to Azure Functions V2, as V2 runtime now take out the `static` modifier. The more details around the **constructor injection** approach on Azure Functions can be found on this [blog post](https://devkimchi.com/2019/02/22/performing-constructor-injections-on-azure-functions-v2/).


### `StartUp` ###

In order to use dependency injections, all dependencies should be registered through the `StartUp` class inheriting the `FunctionsStartup` class.

```csharp
[assembly: FunctionsStartup(typeof(Aliencube.AzureFunctions.FunctionAppV2.StartUp))]
namespace Aliencube.AzureFunctions.FunctionAppV2
{
    public class AppSettings : AppSettingsBase
    {
        public AppSettings()
            : base()
        {
        }
    }

    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<AppSettings>();

            builder.Services.AddTransient<ISampleHttpFunction, SampleHttpFunction>();
            builder.Services.AddTransient<ISampleTimerFunction, SampleTimerFunction>();

            builder.Services.AddTransient<IMyDependency, MyDependency>();
        }
    }
}
```

### Trigger ###

Any Azure Function trigger does not require the `static` modifier any longer. Because of this, it is possible to use the **constructor injection** approach like below:

```csharp
public class SampleHttpTrigger : TriggerBase<ILogger>
{
    private readonly ISampleHttpFunction _function;

    public SampleHttpTrigger(ISampleHttpFunction function)
    {
        this._function = function ?? throw new ArgumentNullException(nameof(function));
    }

    [FunctionName(nameof(GetSample))]
    public async Task<IActionResult> GetSample(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "samples")] HttpRequest req,
        ILogger log)
    {
        var result = await this._function
                               .AddLogger(log)
                               .InvokeAsync<HttpRequest, IActionResult>(req)
                               .ConfigureAwait(false);
        return result;
    }
```


## Property Injection ##

For Azure Function V1, the most efficient way for dependency injection would be the **property injection** approach. The more details around the property injections on Azure Functions can be found on this [blog post](https://devkimchi.com/2018/04/07/dependency-injections-on-azure-functions-v2/).

> This extension supports the `property injection` approach. However, if the `methodd injection` approach is preferred, visit [https://github.com/rikvandenberg/AzureFunctions.Extensions.DependencyInjection](https://github.com/rikvandenberg/AzureFunctions.Extensions.DependencyInjection).


### `StartUp` ###

In order to use dependency injection, all dependencies should be registered beforehand. The `Module` class needs to be inherited then all dependencies are registered within the `Load(IServiceCollection services)` method.

```csharp
public class AppSettings : AppSettingsBase
{
    public AppSettings()
        : base()
    {
    }
}

public class StartUp : Module
{
    public override void Load(IServiceCollection services)
    {
        services.AddSingleton<AppSettings>();

        services.AddTransient<ISampleHttpFunction, SampleHttpFunction>();
        services.AddTransient<ISampleTimerFunction, SampleTimerFunction>();

        services.AddTransient<IMyDependency, MyDependency>();
    }
}
```


### `ContainerBuilder` ###

`ContainerBuilder` builds `IServiceProvider` so that each Function can directly use the IoC container within the method. In order to use the `ContainerBuilder` into the Function, the `IServiceProvider` property should be defined as a `static` property first like:

```csharp
public static class SampleHttpTrigger
{
    public static IServiceProvider Container = new ContainerBuilder
                                                   .RegisterModule<StartUp>()
                                                   .Build();

    [FunctionName("SampleHttpTrigger")]
    public static async Task<HttpResponseMessage> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req,
        ILogger log)
    {
        var function = Container.GetService<IFunction>();
        var result = await function.InvokeAsync<HttpRequestMessage, HttpResponseMessage>(req)
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
    public static IFunctionFactory Factory = new FunctionFactory<StartUp>();

    [FunctionName("SampleHttpTrigger")]
    public static async Task<HttpResponseMessage> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req,
        ILogger log)
    {
        var result = await Factory.Create<ISampleHttpFunction, ILogger>(log)
                                  .InvokeAsync<HttpRequestMessage, HttpResponseMessage>(req)
                                  .ConfigureAwait(false);
        return result;
    }
}
```
