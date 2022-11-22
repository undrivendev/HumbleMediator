# HumbleMediator
[![Build Status](https://dev.azure.com/undrivendev/HumbleMediator/_apis/build/status/undrivendev.HumbleMediator?branchName=main)](https://dev.azure.com/undrivendev/HumbleMediator/_build/latest?definitionId=3&branchName=main)
[![Azure DevOps tests](https://img.shields.io/azure-devops/tests/undrivendev/HumbleMediator/3/main)](https://dev.azure.com/undrivendev/HumbleMediator/_build/latest?definitionId=3&branchName=main)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=undrivendev_HumbleMediator&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=undrivendev_HumbleMediator)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=undrivendev_HumbleMediator&metric=coverage)](https://sonarcloud.io/summary/new_code?id=undrivendev_HumbleMediator)

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
## Cross-cutting concerns
It's possible to implement cross-cutting concerns by leveraging the Decorator pattern on the `IMediator` interface.

TODO: more examples
## Samples
A working example, including an implementation of cross-cutting concerns, can be found in my other project [here](https://github.com/undrivendev/template-webapi-aspnet/blob/main/src/WebApiTemplate.Api/Program.cs#L60).
