# HumbleMediator
HumbleMediator is a simple library (~50 lines of code) containing the minimum amount of abstractions/boilerplate code to implement a functioning Mediator pattern implementation for CQRS.

It is designed to leverage the functionality provided by a Dependency Injection container, so if you're not using one in your project, this is not for you.

## Like it? Give a star! :star:
If you like this project, you learned something from it or you are using it in your applications, please press the star button. Thanks!

## Installation
```
dotnet add package HumbleMediator
```

## Configure
Register the `IMediator` interface with the Dependency Injection container of your choice by creating an instance of the `Mediator` class and pass a `Func<Type, object?>` as a constructor argument.

This delegate should point to the service resolution method of the Dependency Injection container itself.

Some examples:

### ASP.NET Core
```
services.AddSingleton<IMediator>(sp => new Mediator(sp.GetService));
```

### SimpleInjector
```
container.Register<IMediator>(() => new Mediator(container.GetInstance), Lifestyle.Singleton);
```
### Generic IServiceProvider
```
container.Register<IMediator>(() => new Mediator((container as IServiceProvider).GetService));
```
TODO: more examples
## Cross-cutting concerns
It's possible to implement cross-cutting concerns by leveraging the Decorator pattern on the `IMediator` interface.

TODO: more examples
## Samples
A working example, including an implementation of cross-cutting concerns, can be found in my other project [here](https://github.com/undrivendev/template-webapi-aspnet/blob/main/src/WebApiTemplate.Api/Program.cs#L60).
