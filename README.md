# HumbleMediator
HumbleMediator is a simple library (~50 lines of code) containing the minimum amount of abstractions/boilerplate code to implement a functioning Mediator pattern implementation for CQRS.
It is specifically designed to sit on top of a Dependency Injection Container, so if you're not using one in your project (probably you should) this is not for you. 

## Like it? Give a star! :star:
If you like this project, you learned something from it or you are using it in your applications, please press the star button. Thanks!

## Installing via NuGet
```
Install-Package HumbleMediator
```

## Usage
After including the library in your project, you need to provide an implementation for the `IContainer` interface, which the default `Mediator` class implementation depends upon. I decided not to include an implementation for this because it depends on the DI Container of your choice.
An example of an adapter-like implementation forwarding the service resolution to a container implementing the standard Microsoft `IServiceProvider` interface (which many third-party DI containers implement, including the default ASP.NET Core one) could be like this:

```
public class ContainerServiceProviderWrapper : IContainer
{
    private readonly IServiceProvider _container;

    public ContainerServiceProviderWrapper(IServiceProvider container)
    {
        _container = container;
    }

    public TService Resolve<TService>() where TService : notnull
        => _container.GetRequiredService<TService>();
}
```

Then you're able to register the `IContainer` interface with the previous `ContainerServiceProviderWrapper` implementation and the `IMediator` interface with the `Mediator` implementation. That's it.

## Example
A working example can be found in my other project [here](https://github.com/undrivendev/template-webapi-aspnet/blob/main/src/WebApiTemplate.Api/Program.cs#L60).
